using System.Reflection.Metadata.Ecma335;

namespace GestaoFinancas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Armazenamento
            List<Conta> contas = new List<Conta>();
            List<Transacao> transacoes = new List<Transacao>();

            string opcao = "0";

            string menu = @"
1 - Cadastrar nova conta
2 - Cadastrar despesa
3 - Cadastrar receita
4.1 - Exibir o saldo total
4.2 - Exibir o saldo por conta
5.1 - Listar transações
5.2 - Listar receitas
5.3 - Listar despesas
6.1 - Listar transações por mês
6.2 - Listar receitas por mês
6.3 - Listar despesas por mês
0 - Sair";

            do
            {
                Console.WriteLine(menu);
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":

                        //  cadastrar conta
                        Console.WriteLine("Digite o nome da nova conta: ");
                        string nomeConta = Console.ReadLine();
                        Conta novaConta = new Conta(nomeConta);
                        contas.Add(novaConta);

                        Console.WriteLine($"A conta {nomeConta} foi realizada!");

                        break;

                    case "2":
                        //despesa
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma conta foi cadastrada");
                            break;
                        }
                        Console.WriteLine("Escolha a conta que deseja:");
                        for (int iCount = 0; iCount < contas.Count; iCount++)
                        {
                            Console.WriteLine($"{iCount} -  {contas[iCount].Nome} (Saldo da conta : R${contas[iCount].SaldoConta})");
                        }

                        int indiceConta;
                        if (!int.TryParse(Console.ReadLine(), out indiceConta) || indiceConta < 0 || indiceConta >= contas.Count)
                        {
                            Console.WriteLine("Esse indice é inválido");
                            break;
                        }

                        Conta contaSelecionada = contas[indiceConta];

                        Console.WriteLine("Descrição da despesa:");
                        string descricaoDespesa = Console.ReadLine();

                        Console.WriteLine("Valor da despesa:");
                        decimal valorDespesa;
                        if (!decimal.TryParse(Console.ReadLine(), out valorDespesa) || valorDespesa <= 0)
                        {
                            Console.WriteLine("O valor é invalido");
                            break;
                        }

                        Console.WriteLine("Data da despesa :");
                        DateTime dataDespesa;
                        if (!DateTime.TryParse(Console.ReadLine(), out dataDespesa))
                        {
                            Console.WriteLine("Essa data está invalida");
                            break;
                        }

                        DateTime despesaAgora = DateTime.Now;
                        if (dataDespesa.Month < despesaAgora.Month && dataDespesa.Year <= despesaAgora.Year)
                        {
                            Console.WriteLine("Não foi possível cadastrar pois já passou esse mês");
                            break;
                        }

                        if (contaSelecionada.SaldoConta < valorDespesa)
                        {
                            Console.WriteLine("Saldo insuficiente na conta");
                            break;
                        }

                        Transacao novaDespesa = new Transacao(
                            contaSelecionada,
                            descricaoDespesa,
                            valorDespesa,
                            dataDespesa,
                            TipoTransacao.Despesa
                            );


                        transacoes.Add(novaDespesa);
                        contaSelecionada.SaldoConta -= valorDespesa;

                        Console.WriteLine("Despesa registrada!");
                        break;

                    case "3":
                        // Receita
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma conta cadastrada.");
                            break;
                        }

                        Console.WriteLine("Escolha a conta:");
                        for (int iCount = 0; iCount < contas.Count; iCount++)
                        {
                            Console.WriteLine($"{iCount} - {contas[iCount].Nome} (Saldo: R$ {contas[iCount].SaldoConta})");
                        }

                        int indiceContaReceita;
                        if (!int.TryParse(Console.ReadLine(), out indiceContaReceita) || indiceContaReceita < 0)
                        {
                            Console.WriteLine("Indice Invalido");
                        }

                        Conta contaReceita = contas[indiceContaReceita];

                        Console.WriteLine("Descrição da Receita:");
                        string descricaoReceita = Console.ReadLine();

                        Console.WriteLine("Valor da Receita:");
                        decimal valorReceita;
                        if (!decimal.TryParse(Console.ReadLine(), out valorReceita) || valorReceita <= 0)
                        {
                            Console.WriteLine("Valor é inválido");
                            break;
                        }

                        Console.WriteLine("Data da receita:");
                        DateTime dataReceita;
                        if (!DateTime.TryParse(Console.ReadLine(), out dataReceita))
                        {
                            Console.WriteLine("A data é inválida");
                            break;
                        }

                        DateTime receitaAgora = DateTime.Now;

                        if (dataReceita.Month < receitaAgora.Month && dataReceita.Year <= receitaAgora.Year)
                        {
                            Console.WriteLine("Não foi possível cadastrar pois já passou esse mês");
                            break;
                        }

                        Transacao novaReceita = new Transacao(
                            contaReceita,
                            descricaoReceita,
                            valorReceita,
                            dataReceita,
                            TipoTransacao.Receita
                            );

                        transacoes.Add(novaReceita);
                        contaReceita.SaldoConta += valorReceita;

                        Console.WriteLine("Receita registrada!");

                        break;

                    case "4.1":
                        // Exibir total
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma conta cadastrada.");
                            break;
                        }

                        decimal saldoTotal = 0;

                        foreach (Conta conta in contas)
                        {
                            saldoTotal += conta.SaldoConta;
                        }

                        Console.WriteLine($"Saldo total : {saldoTotal:F2}");
                        break;

                    case "4.2":
                        // Exibir saldo
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma conta cadastrada");
                            break;
                        }

                        Console.WriteLine("Saldo(s) por conta:");
                        foreach (Conta conta in contas)
                        {
                            Console.WriteLine($"{conta.Nome}: R$ {conta.SaldoConta:F2}");
                        }
                        break;

                    case "5.1":
                        //  todas transações
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma transação foi encontrada.");
                            break;
                        }
                        Console.WriteLine("Lista de Transações:");
                        foreach (Transacao transacao in transacoes)
                        {
                            Console.WriteLine($"");
                        }

                        break;

                    case "5.2":
                        // Listar receita
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma receita encontrada");
                        }

                        Console.WriteLine("Lista de Receitas:");
                        foreach (Transacao transacao in transacoes)
                        {
                            if (transacao.Tipo == TipoTransacao.Receita)
                            {
                                Console.WriteLine(transacao);
                            }
                        }

                        break;

                    case "5.3":
                        // Listar apenas despesas
                        if (transacoes.Count == 0)
                        {
                            Console.WriteLine("Nenhuma despesa encontrada");
                            break;
                        }
                        Console.WriteLine("Lista de despesas:");
                        foreach (Transacao transacao in transacoes)
                        {
                            if (transacao.Tipo == TipoTransacao.Despesa)
                            {
                                Console.WriteLine(transacao);
                            }
                        }
                        break;

                    case "6.1":
                        // Listar transações por mês
                        Console.WriteLine("Digite o mes e ano: (MM/yyyy)");
                        DateTime mesAno;
                        if (!DateTime.TryParseExact(Console.ReadLine(), "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out mesAno))
                        {
                            Console.WriteLine("Formato inválido, use MM/yyyy");
                            break;
                        }
                        List<Transacao> transacoesMes = new List<Transacao>();

                        foreach (Transacao transacao
                            in transacoes)
                        {
                            if (transacao.Data.Month == mesAno.Month && transacao.Data.Year == mesAno.Year)
                            {
                                transacoesMes.Add(transacao);
                            }
                        }


                        if (transacoesMes.Count == 0)
                        {
                            Console.WriteLine($"Nenhuma transação encontrada para essa data {mesAno:mm/yyyy}");
                            break;
                        }

                        foreach (Transacao transacao in transacoesMes)
                        {
                            string tipo = transacao.Tipo == TipoTransacao.Receita ? "Receita" : "Despesa";
                            Console.WriteLine($"{transacao.Data:dd/MM/yyyy} | " +
                                $"{transacao.Conta.Nome} | " +
                                $"{transacao.Descricao} | " +
                                $"{tipo} | " +
                                $"R$ {(transacao.Tipo == TipoTransacao.Receita ? "+" : "-")}{transacao.Valor:F2}");
                        }
                        break;

                    case "6.2":
                        // Listar receitas por mês
                        Console.WriteLine("Digite o mês e ano (mm/yyyy)");
                        string receitasMes = Console.ReadLine();

                        DateTime mesAnoReceitas;
                        if (!DateTime.TryParseExact(receitasMes, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out mesAnoReceitas))
                        {
                            Console.WriteLine("Formato incorreto! mm/YYYY (exemplo : 02/2024)");
                            break;
                        }

                        List<Transacao> receitasporMes = new List<Transacao>();
                        foreach (Transacao transacao in transacoes)
                        {
                            if (transacao.Tipo == TipoTransacao.Receita &&
                                transacao.Data.Month == mesAnoReceitas.Month &&
                                transacao.Data.Year == mesAnoReceitas.Year)
                            {
                                receitasporMes.Add(transacao);
                            }
                        }

                        for (int Icount = 0; Icount < receitasporMes.Count - 1; Icount++)
                        {
                            for (int ReceitaCount = Icount + 1; ReceitaCount < receitasporMes.Count; ReceitaCount++)
                            {
                                Transacao receitaTemporaria = receitasporMes[Icount];
                                receitasporMes[Icount] = receitasporMes[ReceitaCount];
                                receitasporMes[ReceitaCount] = receitaTemporaria;
                            }
                        } 

                        if (receitasporMes.Count == 0)
                        {
                            Console.WriteLine($"Nenhuma receita encontrada para {mesAnoReceitas:MM/yyyy}");
                            break;
                        }

                        Console.WriteLine($"Receitas de {mesAnoReceitas:MM/yyyy}");
                        Console.WriteLine("Data    -  Conta       -Descrição        Valor");
                        Console.WriteLine("------------------------------------------------");

                        foreach (Transacao transacao in receitasporMes)
                        {
                            Console.WriteLine($"{transacao.Data:dd/MM/yyyy}  {transacao.Conta.Nome,-12}" +
                                $"{transacao.Descricao,-23}    +R$ {transacao.Valor:F2}");
                        }

                        break;

                    case "6.3":
                        // Despesas por mês
                        Console.WriteLine("Digite o mês e ano (mm/yyyy)");
                        string mesAnoDespesas = Console.ReadLine();

                        DateTime mesEanoDespesa;
                        if (!DateTime.TryParseExact(mesAnoDespesas, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out mesEanoDespesa))
                        {
                            Console.WriteLine("Formato incorreto. utilize MM/yyyy exemplo(01/2025)");
                            break;
                        }

                        List<Transacao> despesasMes = new List<Transacao>();
                        foreach(Transacao transacao in transacoes)
                        {
                            if (transacao.Tipo == TipoTransacao.Despesa &&
                                transacao.Data.Month == mesEanoDespesa.Month &&
                                transacao.Data.Year == mesEanoDespesa.Year)
                            {
                                despesasMes.Add(transacao);
                            }
                        }
                        for (int iCount = 0; iCount < despesasMes.Count - 1; iCount++)
                        {
                            for (int despesaCount = iCount + 1; despesaCount < despesasMes.Count; despesaCount++)
                            {
                                if (despesasMes[iCount].Data > despesasMes[despesaCount].Data)
                                {
                                    Transacao despesaTemporaria = despesasMes[iCount];
                                    despesasMes[iCount] = despesasMes[despesaCount];
                                    despesasMes[despesaCount] = despesaTemporaria; 
                                }
                            }
                        }


                        if (despesasMes.Count ==0)
                        {
                            Console.WriteLine($"Nenhuma despesa encontrada para {mesEanoDespesa:MM/yyyy}");
                            break;
                        }

                        Console.WriteLine($"Despesas de {mesEanoDespesa:MM/yyyy} :");
                        Console.WriteLine("Data      Conta      Descrição      Valor") ;
                        Console.WriteLine("-------------------------------------------") ;

                        foreach (Transacao transacao in despesasMes)
                        {
                            Console.WriteLine($"{transacao.Data:dd/MM/yyyy}  {transacao.Conta.Nome,-12} " + $"{transacao.Descricao,-23}  R$ {transacao.Valor:F2}");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Saindo do programa :)");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != "0");
        }
    }
}
