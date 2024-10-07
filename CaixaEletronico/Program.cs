using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        int opcao;
        double saldoInicial = 0, DinheiroNaConta = 0, transferencia;
        List<string> extrato = new List<string>();
        do
        {
            Console.WriteLine("\n--- Caixa Eletrônico ---");
            Console.WriteLine("1: Criar Conta");
            Console.WriteLine("2: Depositar");
            Console.WriteLine("3: Sacar");
            Console.WriteLine("4: Extrato");
            Console.WriteLine("5: Transferir");
            Console.WriteLine("6: Sair");
            Console.Write("Escolha uma opção: ");
            
            if (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 6)
            {
                Console.WriteLine("\nNúmero inválido! Tente novamente.");
                continue;
            }
        }while(opcao <= 7);

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
                        Console.Write("Digite o saldo inicial: ");
                        if (!double.TryParse(Console.ReadLine(), out saldoInicial))
                        {
                            Console.WriteLine("Valor inválido!");
                        }
                        else
                        {
                            DinheiroNaConta = saldoInicial;
                            extrato.Add($"Conta criada com saldo inicial de {saldoInicial:C}");
                            Console.WriteLine($"Conta criada com sucesso! Saldo inicial: {saldoInicial:C}");
                        }
                    }
                    break;
                
                case 2:
                    Console.Clear();
                    Console.WriteLine("Digite o valor para o depósito:");
                    if (double.TryParse(Console.ReadLine(), out double deposito) && deposito > 0)
                    {
                        DinheiroNaConta += deposito;
                        extrato.Add($"Depósito de {deposito:C}");
                        Console.WriteLine($"Depósito realizado com sucesso, saldo atual é de: {DinheiroNaConta:C}");
                    }
                    else
                    {
                        Console.WriteLine("Valor de depósito inválido!");
                    }
                    break;
                
                case 3:
                    Console.Clear();
                    Console.WriteLine("Digite o valor para o saque:");
                    if (double.TryParse(Console.ReadLine(), out double saque) && saque > 0 && saque <= DinheiroNaConta)
                    {
                        DinheiroNaConta -= saque;
                        extrato.Add($"Saque de {saque:C}");
                        Console.WriteLine($"Saque realizado com sucesso, saldo atual é de: {DinheiroNaConta:C}");
                    }
                    else
                    {
                        Console.WriteLine("Valor de saque inválido ou saldo insuficiente!");
                    }
                    break;

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
                    break;

                case 5:
                    Console.Clear();
                    Console.WriteLine("Digite o valor que será enviado:");
                    if (double.TryParse(Console.ReadLine(), out transferencia) && transferencia > 0 && transferencia <= DinheiroNaConta)
                    {
                        Console.Write("Digite o nome do destinatário: ");
                        string destinatario = Console.ReadLine();
                        Console.Write("Digite o nome da agência do destinatário: ");
                        string agenciaR = Console.ReadLine();

                        DinheiroNaConta -= transferencia;
                        extrato.Add($"Transferência de {transferencia:C} para {destinatario}");
                        Console.WriteLine($"Transferência realizada com sucesso, saldo atual é de: {DinheiroNaConta:C}");
                    }
                    else
                    {
                        Console.WriteLine("Transferência não realizada! Valor inválido ou saldo insuficiente.");
                    }
                    break;

                case 6:
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
    }
}