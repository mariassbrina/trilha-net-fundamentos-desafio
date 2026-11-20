# DIO - Trilha .NET - Fundamentos

Desafio de projeto do módulo de Fundamentos da trilha .NET da [DIO](https://www.dio.me/), baseado no repositório
[digitalinnovationone/trilha-net-fundamentos-desafio](https://github.com/digitalinnovationone/trilha-net-fundamentos-desafio).

## O desafio

Construir um sistema de console para um estacionamento, capaz de:

- **Cadastrar** um veículo (pela placa);
- **Remover** um veículo, exibindo o valor cobrado pelo período;
- **Listar** os veículos estacionados.

O valor cobrado é calculado como:

```
valor total = preço inicial + (preço por hora × horas)
```

## Estrutura

```
DesafioFundamentos.csproj
Program.cs                 # leitura dos preços e menu interativo
Models/
  Estacionamento.cs        # regras de negócio (adicionar, remover, listar, calcular valor)
```

### Classe `Estacionamento`

| Membro | Descrição |
| --- | --- |
| `Estacionamento(decimal precoInicial, decimal precoPorHora)` | Cria o estacionamento com a tabela de preços (valores negativos não são aceitos). |
| `AdicionarVeiculo()` | Lê a placa, valida o formato e impede duplicidade. |
| `RemoverVeiculo()` | Lê a placa e as horas, calcula o valor e remove o veículo. |
| `ListarVeiculos()` | Exibe os veículos estacionados ou avisa que não há nenhum. |
| `CalcularValor(int horas)` | Retorna `precoInicial + precoPorHora * horas`. |

### Melhorias em relação ao template

- Validação de placa (padrão antigo `ABC1234`/`ABC-1234` e Mercosul `ABC1D23`), sem diferenciar maiúsculas/minúsculas;
- Bloqueio de placas duplicadas;
- Validação dos preços e das horas digitadas (aceita vírgula ou ponto como separador decimal);
- Valores exibidos em Real (`R$`).

## Como executar

Pré-requisito: [.NET SDK 10](https://dotnet.microsoft.com/download) (para usar outra versão, altere o
`TargetFramework` no `.csproj`, por exemplo para `net8.0`).

```bash
dotnet run
```

### Exemplo de uso

```
Seja bem-vindo ao sistema de estacionamento!
Digite o preço inicial:
5
Agora digite o preço por hora:
2,50
Digite a sua opção:
1 - Cadastrar veículo
...
1
Digite a placa do veículo para estacionar:
abc1d23
Veículo ABC1D23 estacionado com sucesso.
...
2
Digite a placa do veículo para remover:
ABC1D23
Digite a quantidade de horas que o veículo permaneceu estacionado:
3
O veículo ABC1D23 foi removido e o preço total foi de: R$ 12,50
```
