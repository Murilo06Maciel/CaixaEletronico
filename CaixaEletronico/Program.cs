using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        int opcao;
        double saldoContaCorrente = 0, saldoPoupanca = 0, transferencia;
        const double limiteSaque = 50000;
        const double taxaTransferencia = 0.0005;
        List<string> extrato = new List<string>();
        string senha = string.Empty;

        returngeral:
        do
        {
            Console.WriteLine("\n--- Caixa Eletrônico ---");
            Console.WriteLine("1: Criar Conta");
            Console.WriteLine("2: Depositar na Conta Corrente");
            Console.WriteLine("3: Sacar da Conta Corrente");
            Console.WriteLine("4: Extrato");
            Console.WriteLine("5: Transferir");
            Console.WriteLine("6: Poupança");
            Console.WriteLine("7: Certificado de Depósito Bancário (CDB)");
            Console.WriteLine("8: Sair");
            Console.Write("Escolha uma opção: ");
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("\nNúmero inválido! Tente novamente.");
            }
        } while (opcao < 1 || opcao > 8);

        switch (opcao)
        {
            case 1:
                Console.Clear();
                Console.Write("Digite o nome do titular da conta: ");
                string nome = Console.ReadLine();
                Console.Write("Digite o nome da agência: ");
                string agencia = Console.ReadLine();
                Console.Write("Digite o número da conta: ");
                if (!int.TryParse(Console.ReadLine(), out int numero))
                {
                    Console.WriteLine("\nNúmero inválido!");
                }
                else
                {
                    Console.Write("Digite sua senha (mínimo 8 caracteres): ");
                    senha = Console.ReadLine();
                    if (senha.Length < 8)
                    {
                        Console.WriteLine("Senha muito curta!");
                        break;
                    }
                    Console.Write("Digite o saldo inicial da Conta Corrente: ");
                    if (!double.TryParse(Console.ReadLine(), out saldoContaCorrente))
                    {
                        Console.WriteLine("Valor inválido!");
                    }
                    else
                    {
                        extrato.Add($"Conta criada com saldo inicial de {saldoContaCorrente:C} na Conta Corrente");
                        Console.WriteLine($"Conta criada com sucesso! Saldo inicial: {saldoContaCorrente:C}");
                    }
                }
                goto returngeral;

            case 2:
                Console.Clear();
                Console.Write("Digite sua senha: ");
                string tentativaSenha = Console.ReadLine();
                if (tentativaSenha != senha)
                {
                    Console.WriteLine("Senha incorreta!");
                }
                else
                {
                    Console.WriteLine("Digite o valor para o depósito na Conta Corrente:");
                    if (double.TryParse(Console.ReadLine(), out double deposito) && deposito > 0)
                    {
                        saldoContaCorrente += deposito;
                        extrato.Add($"Depósito de {deposito:C} na Conta Corrente");
                        Console.WriteLine($"Depósito realizado com sucesso, saldo atual é de: {saldoContaCorrente:C}");
                    }
                    else
                    {
                        Console.WriteLine("Valor de depósito inválido!");
                    }
                }
                goto returngeral;

            case 3:
                Console.Clear();
                Console.WriteLine("Digite a senha para sacar da Conta Corrente:");
                string senhaDigitada = Console.ReadLine();
                if (senhaDigitada == senha)
                {
                    Console.WriteLine("Digite o valor para o saque:");
                    if (double.TryParse(Console.ReadLine(), out double saque) && saque > 0 && saque <= saldoContaCorrente && saque <= limiteSaque)
                    {
                        saldoContaCorrente -= saque;
                        extrato.Add($"Saque de {saque:C} da Conta Corrente");
                        Console.WriteLine($"Saque realizado com sucesso, saldo atual é de: {saldoContaCorrente:C}");
                    }
                    else
                    {
                        Console.WriteLine("Valor de saque inválido ou saldo insuficiente, ou limite de saque excedido!");
                    }
                }
                else
                {
                    Console.WriteLine("Senha incorreta!");
                }
                goto returngeral;

            case 4:
                Console.Clear();
                Console.WriteLine("\n---- Extrato ----");
                if (extrato.Count == 0)
                {
                    Console.WriteLine("Nenhuma transação realizada.");
                }
                else
                {
                    foreach (var item in extrato)
                    {
                        Console.WriteLine(item);
                    }
                }
                goto returngeral;

            case 5:
                Console.Clear();
                Console.WriteLine("Digite o valor que será enviado:");
                if (double.TryParse(Console.ReadLine(), out transferencia) && transferencia > 0 && transferencia <= saldoContaCorrente)
                {
                    Console.Write("Digite o nome do destinatário: ");
                    string destinatario = Console.ReadLine();
                    Console.Write("Digite o nome da agência do destinatário: ");
                    string agenciaR = Console.ReadLine();

                    double valorComTaxa = transferencia * (1 + taxaTransferencia);
                    if (valorComTaxa <= saldoContaCorrente)
                    {
                        saldoContaCorrente -= valorComTaxa;
                        extrato.Add($"Transferência de {transferencia:C} para {destinatario} (Taxa: {transferencia * taxaTransferencia:C})");
                        Console.WriteLine($"Transferência realizada com sucesso, saldo atual é de: {saldoContaCorrente:C}");
                    }
                    else
                    {
                        Console.WriteLine("Transferência não realizada! Saldo insuficiente após aplicação da taxa.");
                    }
                }
                else
                {
                    Console.WriteLine("Transferência não realizada! Valor inválido ou saldo insuficiente.");
                }
                goto returngeral;

            case 6:
                Console.Clear();
                Console.WriteLine("Você escolheu a opção de Poupança.");
                Console.Write("Digite o valor que deseja depositar na Poupança: ");
                if (double.TryParse(Console.ReadLine(), out double poupancaValor) && poupancaValor > 0)
                {
                    saldoPoupanca += poupancaValor; 
                    Console.Write("Digite quantos meses você deseja manter na Poupança: ");
                    if (int.TryParse(Console.ReadLine(), out int meses) && meses > 0)
                    {
                        double taxaPoupanca = 0.0056; 
                        double rendimento = poupancaValor * Math.Pow(1 + taxaPoupanca, meses) - poupancaValor;
                        saldoPoupanca += rendimento;
                        extrato.Add($"Depósito na Poupança de {poupancaValor:C} com rendimento de {rendimento:C} após {meses} meses");
                        Console.WriteLine($"Depósito realizado com sucesso na Poupança. Saldo atual da Poupança é de: {saldoPoupanca:C}");
                    }
                    else
                    {
                        Console.WriteLine("Número de meses inválido!");
                    }
                }
                else
                {
                    Console.WriteLine("Valor inválido!");
                }
                goto returngeral;

            case 7:
                Console.Clear();
                Console.WriteLine("Você escolheu a opção de Certificado de Depósito Bancário (CDB).");
                Console.Write("Digite o valor que deseja investir no CDB: ");
                if (double.TryParse(Console.ReadLine(), out double cdbValor) && cdbValor > 0)
                {
                    double taxaCDB = 0.15; 
                    saldoContaCorrente += cdbValor + (cdbValor * taxaCDB);
                    extrato.Add($"Investimento em CDB de {cdbValor:C} com rendimento de {cdbValor * taxaCDB:C}");
                    Console.WriteLine($"Investimento realizado com sucesso em CDB. Saldo atual é de: {saldoContaCorrente:C}");
                }
                else
                {
                    Console.WriteLine("Valor inválido!");
                }
                goto returngeral;

            case 8:
                Console.Clear();
                Console.WriteLine("Saindo...");
                break;

            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
}
