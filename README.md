# Desafio Técnico - Target Sistemas

Resolução dos três exercícios do desafio em **C# / .NET 8**, cada um em um projeto de console separado dentro da mesma solution.

```
DesafioTarget.sln
└── src/
    ├── Desafio1.Comissoes   → comissão por vendedor a partir do JSON de vendas
    ├── Desafio2.Estoque     → lançamento de entradas e saídas de estoque
    └── Desafio3.Juros       → juros por atraso a partir de valor e vencimento
```

Em cada projeto, as regras de negócio ficam em `Services/`, os modelos em `Models/` e o `Program.cs` cuida apenas da entrada e saída no console.

## Como executar

Pré-requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet run --project src/Desafio1.Comissoes
dotnet run --project src/Desafio2.Estoque
dotnet run --project src/Desafio3.Juros
```

## 1. Comissões

Lê `Data/vendas.json` e aplica a regra em **cada venda**:

| Valor da venda      | Comissão |
|---------------------|----------|
| abaixo de R$ 100,00 | 0%       |
| abaixo de R$ 500,00 | 1%       |
| a partir de R$ 500,00 | 5%     |

Depois agrupa por vendedor e mostra quantidade de vendas, total vendido e total de comissão.

Resultado:

```
Vendedor            Vendas     Total vendido      Comissão
----------------------------------------------------------
João Silva              10      R$ 10.754,70     R$ 495,68
Maria Souza              9       R$ 9.874,30     R$ 465,95
Ana Lima                 9       R$ 8.763,95     R$ 404,98
Carlos Oliveira          8       R$ 7.928,35     R$ 379,37
----------------------------------------------------------
TOTAL                   36      R$ 37.321,30   R$ 1.745,98
```

## 2. Movimentação de estoque

Carrega os produtos de `Data/estoque.json` e abre um menu para listar produtos, lançar entradas, lançar saídas e consultar o histórico.

Cada movimentação tem:
- **ID único** sequencial, gerado automaticamente;
- **descrição** informada pelo usuário (ex.: "Compra de fornecedor", "Venda", "Devolução");
- tipo (entrada/saída), produto, quantidade e data.

Ao final de cada lançamento o programa mostra a **quantidade final em estoque** do produto movimentado.

Validações: produto inexistente, quantidade menor ou igual a zero, descrição vazia e saída maior que o saldo disponível.

> As movimentações ficam em memória durante a execução; o arquivo JSON original não é alterado.

## 3. Juros por atraso

Recebe um valor e uma data de vencimento e calcula os juros até a data de hoje, com taxa de **2,5% ao dia** (juros simples):

```
juros = valor × 2,5% × dias de atraso
```

Se a data de vencimento for hoje ou no futuro, não há juros. Exemplo com R$ 1.000,00 e 10 dias de atraso: juros de R$ 250,00 e valor atualizado de R$ 1.250,00.
