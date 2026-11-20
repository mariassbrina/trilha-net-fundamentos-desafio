using System.Globalization;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        // Aceita placas no padrão antigo (ABC1234 / ABC-1234) e no padrão Mercosul (ABC1D23)
        private static readonly Regex FormatoPlaca =
            new(@"^[A-Z]{3}-?[0-9][A-Z0-9][0-9]{2}$", RegexOptions.Compiled);

        private readonly decimal precoInicial;
        private readonly decimal precoPorHora;
        private readonly List<string> veiculos = new();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            if (precoInicial < 0 || precoPorHora < 0)
                throw new ArgumentException("Os preços não podem ser negativos.");

            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public IReadOnlyList<string> Veiculos => veiculos;

        public static string NormalizarPlaca(string? placa) =>
            (placa ?? string.Empty).Trim().ToUpperInvariant().Replace("-", string.Empty);

        public static bool PlacaValida(string? placa) =>
            FormatoPlaca.IsMatch((placa ?? string.Empty).Trim().ToUpperInvariant());

        public bool VeiculoEstacionado(string placa) =>
            veiculos.Contains(NormalizarPlaca(placa));

        public decimal CalcularValor(int horas)
        {
            if (horas < 0)
                throw new ArgumentOutOfRangeException(nameof(horas), "A quantidade de horas não pode ser negativa.");

            return precoInicial + precoPorHora * horas;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = NormalizarPlaca(Console.ReadLine());

            if (!PlacaValida(placa))
            {
                Console.WriteLine("Placa inválida. Use o formato ABC1234 ou ABC1D23.");
                return;
            }

            if (veiculos.Contains(placa))
            {
                Console.WriteLine($"O veículo {placa} já está estacionado.");
                return;
            }

            veiculos.Add(placa);
            Console.WriteLine($"Veículo {placa} estacionado com sucesso.");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = NormalizarPlaca(Console.ReadLine());

            if (!VeiculoEstacionado(placa))
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
                return;
            }

            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
            if (!int.TryParse(Console.ReadLine(), out int horas) || horas < 0)
            {
                Console.WriteLine("Quantidade de horas inválida. Operação cancelada.");
                return;
            }

            decimal valorTotal = CalcularValor(horas);
            veiculos.Remove(placa);

            string valorFormatado = valorTotal.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
            Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {valorFormatado}");
        }

        public void ListarVeiculos()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("Não há veículos estacionados.");
                return;
            }

            Console.WriteLine($"Os veículos estacionados são ({veiculos.Count}):");
            for (int i = 0; i < veiculos.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {veiculos[i]}");
            }
        }
    }
}
