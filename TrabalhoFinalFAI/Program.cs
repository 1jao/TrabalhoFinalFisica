using System;
using System.Collections.Generic;

class CircuitoEletrico
{
    static void Main()
    {
        string tipoAssociacao;
        do
        {
            Console.WriteLine("Escolha o tipo de associação de resistores (serie/paralelo):");
            tipoAssociacao = Console.ReadLine().ToLower();

            if (tipoAssociacao != "serie" && tipoAssociacao != "paralelo")
            {
                Console.WriteLine("Tipo de associação inválido. Tente novamente.");
            }
        } while (tipoAssociacao != "serie" && tipoAssociacao != "paralelo");

        Console.WriteLine("Informe a quantidade de geradores (em série ou paralelo) e a voltagem de cada um:");
        int numGeradores = int.Parse(Console.ReadLine());
        List<double> geradores = new List<double>();
        for (int i = 0; i < numGeradores; i++)
        {
            Console.WriteLine($"Voltagem do gerador {i + 1}:");
            geradores.Add(double.Parse(Console.ReadLine()));
        }

        Console.WriteLine("Informe a quantidade de resistores e o valor de cada um (em ohms):");
        int numResistores = int.Parse(Console.ReadLine());
        List<double> resistores = new List<double>();
        for (int i = 0; i < numResistores; i++)
        {
            Console.WriteLine($"Resistência do resistor {i + 1}:");
            resistores.Add(double.Parse(Console.ReadLine()));
        }

        double tensaoTotal = 0;
        geradores.ForEach(v => tensaoTotal += v);

        double resistenciaTotal = 0;
        if (tipoAssociacao == "serie")
        {
            resistores.ForEach(r => resistenciaTotal += r);
        }
        else if (tipoAssociacao == "paralelo")
        {
            resistores.ForEach(r => resistenciaTotal += 1 / r);
            resistenciaTotal = 1 / resistenciaTotal;
        }

        double correnteTotal = tensaoTotal / resistenciaTotal;
        if (correnteTotal > 10)
        {
            Console.WriteLine("A corrente elétrica total excede o limite de 10 A.");
            return;
        }

        Console.WriteLine($"Corrente elétrica total: {correnteTotal} A");

        if (tipoAssociacao == "serie")
        {
            Console.WriteLine("Tensões nos terminais dos resistores:");
            resistores.ForEach(r => Console.WriteLine($"Tensão no resistor {r} ohms: {correnteTotal * r} V"));
        }
        else if (tipoAssociacao == "paralelo")
        {
            Console.WriteLine("Correntes nos resistores:");
            resistores.ForEach(r => Console.WriteLine($"Corrente no resistor {r} ohms: {tensaoTotal / r} A"));
        }

        double potenciaTotal = tensaoTotal * correnteTotal;
        Console.WriteLine($"Potência total fornecida pelo(s) gerador(es): {potenciaTotal} W");

        Console.WriteLine("Potências consumidas pelos resistores:");
        if (tipoAssociacao == "serie")
        {
            resistores.ForEach(r => Console.WriteLine($"Potência no resistor {r} ohms: {(correnteTotal * correnteTotal) * r} W"));
        }
        else if (tipoAssociacao == "paralelo")
        {
            resistores.ForEach(r => Console.WriteLine($"Potência no resistor {r} ohms: {(tensaoTotal * tensaoTotal) / r} W"));
        }
    }
}
