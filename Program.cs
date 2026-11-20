using System.Globalization;
using System.Text;
using DesafioFundamentos.Models;

Console.OutputEncoding = Encoding.UTF8;

decimal precoInicial = LerDecimal("Seja bem-vindo ao sistema de estacionamento!\nDigite o preço inicial:");
decimal precoPorHora = LerDecimal("Agora digite o preço por hora:");

var estacionamento = new Estacionamento(precoInicial, precoPorHora);

bool exibirMenu = true;

while (exibirMenu)
{
    if (!Console.IsOutputRedirected) Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine()?.Trim())
    {
        case "1":
            estacionamento.AdicionarVeiculo();
            break;

        case "2":
            estacionamento.RemoverVeiculo();
            break;

        case "3":
            estacionamento.ListarVeiculos();
            break;

        case "4":
        case null: // fim da entrada (Ctrl+Z / Ctrl+D)
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    if (exibirMenu)
    {
        Console.WriteLine("Pressione Enter para continuar");
        Console.ReadLine();
    }
}

Console.WriteLine("O programa se encerrou");

static decimal LerDecimal(string mensagem)
{
    Console.WriteLine(mensagem);
    while (true)
    {
        string entrada = (Console.ReadLine()
            ?? throw new InvalidOperationException("Entrada encerrada antes de informar os preços.")).Trim();

        // Aceita tanto vírgula (pt-BR) quanto ponto como separador decimal
        string normalizada = entrada.Replace(',', '.');
        if (decimal.TryParse(normalizada, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal valor))
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Digite um número maior ou igual a zero (ex.: 5 ou 2,50):");
    }
}
