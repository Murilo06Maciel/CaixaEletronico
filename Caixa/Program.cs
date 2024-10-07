class Conta
{
    public string Nome { get; private set; }
    public string Numero { get; private set; }
    public double Saldo { get; private set; }

    public Conta(string nome, string numero, double saldo = 0.0)
    {
        Nome = nome;
        Numero = numero;
        Saldo = saldo;
    }

    public void Depositar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de depósito inválido.");
            return;
        }
        Saldo += valor;
        Console.WriteLine($"Depósito de R${valor:F2} realizado com sucesso.");
    }

    public void Sacar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de saque inválido.");
            return;
        }
        if (valor > Saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
            return;
        }
        Saldo -= valor;
        Console.WriteLine($"Saque de R${valor:F2} realizado com sucesso.");
    }

    public void Extrato()
    {
        Console.WriteLine($"{Nome} - Conta {Numero}: Saldo atual: R${Saldo:F2}");
    }

    public void Transferir(Conta contaDestino, double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de transferência inválido.");
            return;
        }
        if (valor > Saldo)
        {
            Console.WriteLine("Saldo insuficiente para transferência.");
            return;
        }
        Sacar(valor);
        contaDestino.Depositar(valor);
        Console.WriteLine($"Transferência de R${valor:F2} para a conta {contaDestino.Numero} realizada com sucesso.");
    }
}

class Program
{
    static List<Conta> contas = new List<Conta>();

    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n--- Caixa Eletrônico ---");
                Console.WriteLine("1: Criar Conta");
                Console.WriteLine("2: Depositar");
                Console.WriteLine("3: Sacar");
                Console.WriteLine("4: Extrato");
                Console.WriteLine("5: Transferir");
                Console.WriteLine("6: Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CriarConta();
                        break;

                    case "2":
                        RealizarOperacao("depositar");
                        break;

                    case "3":
                        RealizarOperacao("sacar");
                        break;

                    case "4":
                        ExibirExtrato();
                        break;

                    case "5":
                        Transferir();
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrada inválida. Por favor, insira números válidos onde necessário.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }

    static void CriarConta()
    {
        Console.Write("Digite o nome do titular da conta: ");
        string nome = Console.ReadLine();
        Console.Write("Digite o número da conta: ");
        string numero = Console.ReadLine();
        Console.Write("Digite o saldo inicial (opcional, deixe em branco para R$0,00): ");
        string saldoInicial = Console.ReadLine();

        double saldo = string.IsNullOrEmpty(saldoInicial) ? 0.0 : Convert.ToDouble(saldoInicial);
        Conta novaConta = new Conta(nome, numero, saldo);
        contas.Add(novaConta);

        Console.WriteLine("Conta criada com sucesso!");
    }

    static void RealizarOperacao(string operacao)
    {
        Conta conta = SelecionarConta();
        if (conta == null) return;

        Console.Write($"Digite o valor para {operacao}: ");
        double valor = Convert.ToDouble(Console.ReadLine());

        if (operacao == "depositar")
        {
            conta.Depositar(valor);
        }
        else if (operacao == "sacar")
        {
            conta.Sacar(valor);
        }
    }

    static void ExibirExtrato()
    {
        Conta conta = SelecionarConta();
        if (conta != null)
        {
            conta.Extrato();
        }
    }

    static void Transferir()
    {
        Console.WriteLine("Selecione a conta de origem:");
        Conta contaOrigem = SelecionarConta();
        if (contaOrigem == null) return;

        Console.WriteLine("Selecione a conta de destino:");
        Conta contaDestino = SelecionarConta();
        if (contaDestino == null) return;

        Console.Write("Digite o valor para transferir: ");
        double valor = Convert.ToDouble(Console.ReadLine());

        contaOrigem.Transferir(contaDestino, valor);
    }

    static Conta SelecionarConta()
    {
        if (contas.Count == 0)
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return null;
        }

        Console.WriteLine("\n--- Contas Disponíveis ---");
        for (int i = 0; i < contas.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {contas[i].Nome} - Conta {contas[i].Numero}");
        }

        Console.Write("Selecione o número da conta: ");
        int indiceConta = Convert.ToInt32(Console.ReadLine()) - 1;

        if (indiceConta >= 0 && indiceConta < contas.Count)
        {
            return contas[indiceConta];
        }
        else
        {
            Console.WriteLine("Número de conta inválido.");
            return null;
        }
    }
}

