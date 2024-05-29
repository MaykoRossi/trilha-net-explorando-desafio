using System;
using System.Collections.Generic;
using DesafioProjetoHospedagem.Models;

namespace DesafioProjetoHospedagem
{
    class Program
    {
        static void Main(string[] args)
        {
            var reserva = new Reserva();
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Cadastrar hóspedes");
                Console.WriteLine("2. Mostrar hóspedes cadastrados");
                Console.WriteLine("3. Calcular valor da estadia");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarHospedes(reserva);
                        break;

                    case "2":
                        MostrarHospedes(reserva);
                        break;

                    case "3":
                        CalcularValorEstadia(reserva);
                        break;

                    case "4":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void CadastrarHospedes(Reserva reserva)
{
    Console.WriteLine("Cadastro de Hóspedes:");

    while (true)
    {
        Console.WriteLine("Cadastro de novo hóspede:");

        Console.Write("Informe o nome completo do hóspede (ou 'sair' para finalizar): ");
        string nomeCompleto = Console.ReadLine();

        if (nomeCompleto.ToLower() == "sair")
            break;

        Console.WriteLine("Escolha a suíte:");
        Console.WriteLine("1. Comum (Capacidade: 2, Valor Diária: R$ 100)");
        Console.WriteLine("2. Premium (Capacidade: 4, Valor Diária: R$ 200)");
        Console.WriteLine("3. Presidencial (Capacidade: 6, Valor Diária: R$ 300)");
        Console.Write("Digite o número da opção desejada: ");
        string tipoSuite;
        switch (Console.ReadLine())
        {
            case "1":
                tipoSuite = "comum";
                break;
            case "2":
                tipoSuite = "premium";
                break;
            case "3":
                tipoSuite = "presidencial";
                break;
            default:
                Console.WriteLine("Opção inválida. Cadastro cancelado.");
                return;
        }

        Console.Write("Informe a quantidade de dias de reserva para este hóspede: ");
        int diasReservados = int.Parse(Console.ReadLine());

        try
        {
            // Cadastra a suíte e a quantidade de dias para este hóspede
            reserva.CadastrarSuite(Suite.CriarSuitePorTipo(tipoSuite));
            reserva.DiasReservados = diasReservados;

            // Separa o nome completo em nome e sobrenome
            string[] partesNome = nomeCompleto.Split(' ');
            string nome = partesNome[0];
            string sobrenome = partesNome.Length > 1 ? partesNome[1] : "";

            // Cadastra o hóspede
            reserva.CadastrarHospedes(new List<Pessoa> { new Pessoa(nome, sobrenome) });

            Console.WriteLine($"Hóspede cadastrado com sucesso na suíte {tipoSuite} por {diasReservados} dias!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar hóspede: {ex.Message}");
        }
    }
}

        static void MostrarHospedes(Reserva reserva)
{
    if (reserva.Hospedes.Count == 0)
    {
        Console.WriteLine("Nenhum hóspede cadastrado.");
        return;
    }

    Console.WriteLine("Hóspedes cadastrados:");
    for (int i = 0; i < reserva.Hospedes.Count; i++)
    {
        Console.WriteLine($"Hóspede {i + 1}:");
        Console.WriteLine($"  Nome: {reserva.Hospedes[i].NomeCompleto}");
        Console.WriteLine($"  Suíte: {reserva.Suite.TipoSuite.ToUpper()}");
        Console.WriteLine($"  Dias reservados: {reserva.DiasReservados}");
        Console.WriteLine();
    }
}

        static void CalcularValorEstadia(Reserva reserva)
        {
            if (reserva.Hospedes.Count == 0)
            {
                Console.WriteLine("Nenhum hóspede cadastrado.");
                return;
            }

            // Exibe a lista de hóspedes cadastrados
            MostrarHospedes(reserva);

            Console.Write("Selecione o número do hóspede para calcular o valor da estadia: ");
            int numeroHospede = int.Parse(Console.ReadLine()) - 1;

            // Verifica se o número do hóspede selecionado é válido
            if (numeroHospede < 0 || numeroHospede >= reserva.Hospedes.Count)
            {
                Console.WriteLine("Número de hóspede inválido.");
                return;
            }

            Console.Write($"Informe o número de dias reservados para {reserva.Hospedes[numeroHospede].NomeCompleto}: ");
            int diasReservados = int.Parse(Console.ReadLine());

            try
            {
                // Define os dias reservados para o hóspede selecionado
                reserva.DiasReservados = diasReservados;

                // Calcula o valor total da estadia para o hóspede selecionado
                decimal valor = reserva.CalcularValorDiaria();

                // Aplica desconto de 10% se a estadia ultrapassar 10 dias
                if (diasReservados >= 10)
                {
                    valor *= 0.9m; // 10% de desconto
                    Console.WriteLine("Desconto de 10% aplicado para estadias de 10 dias ou mais.");
                }

                Console.WriteLine($"O valor total da estadia para {reserva.Hospedes[numeroHospede].NomeCompleto} é: {valor:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao calcular o valor da estadia: {ex.Message}");
            }
        }
    }
}