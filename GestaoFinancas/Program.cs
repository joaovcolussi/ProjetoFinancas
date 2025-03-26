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
                        if(contas.Count == 0)
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
                        if (!int.TryParse(Console.ReadLine(),out indiceConta) || indiceConta <0 || indiceConta >= contas.Count)
                        {
                            Console.WriteLine("Esse indice é inválido");
                            break;
                        }

                        Conta contaSelecionada = contas[indiceConta];

                        Console.WriteLine("Descrição da despesa:");
                        string descricaoDespesa = Console.ReadLine();

                        Console.WriteLine("Valor da despesa:");
                        decimal valorDespesa;
                        if(!decimal.TryParse(Console.ReadLine(),out valorDespesa) || valorDespesa <=0)
                        {
                            Console.WriteLine("O valor é invalido");
                            break ;
                        }

                        Console.WriteLine("Data da despesa :");
                        DateTime dataDespesa;
                        if (!DateTime.TryParse(Console.ReadLine(), out dataDespesa))
                            {
                            Console.WriteLine("Essa data está invalida");
                            break ;
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
                        for(int iCount = 0;iCount < contas.Count; iCount++)
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
                            break ;
                        }

                        Console.WriteLine("Data da receita:");
                        DateTime dataReceita;
                        if(!DateTime.TryParse(Console.ReadLine(), out dataReceita))
                        {
                            Console.WriteLine("A data é inválida");
                            break;
                        }

                        DateTime receitaAgora = DateTime.Now;

                        if(dataReceita.Month < receitaAgora.Month && dataReceita.Year <= receitaAgora.Year)
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
                        if (contas.Count ==0)
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
                        if (contas.Count ==0)
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
                        // Listar apenas receitas
                        break;

                    case "5.3":
                        // Listar apenas despesas
                        break;

                    case "6.1":
                        // Listar transações por mês
                        break;

                    case "6.2":
                        // Listar receitas por mês
                        break;

                    case "6.3":
                        // Listar despesas por mês
                        break;

                    case "0":
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != "0");
        }
    }
}
