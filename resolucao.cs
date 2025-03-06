using System;
using System.Linq;
using System.Text;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine("1) Resultado da soma:");
        int resultadoSoma = SomaIndice();
        Console.WriteLine($"Resposta: {resultadoSoma}");
        
        Console.WriteLine("\n2) Verificação na sequência de Fibonacci:");
        Console.Write("Digite um número: ");
        int num = int.Parse(Console.ReadLine());
        bool pertence = VerificarFibonacci(num);
        Console.WriteLine($"Resposta: {(pertence ? "O número pertence à sequência de Fibonacci." : "O número NÃO pertence à sequência de Fibonacci.")}");
        
        Console.WriteLine("\n3) Análise do faturamento:");
        var (menor, maior, diasAcima) = AnaliseFaturamento();
        Console.WriteLine($"Resposta: Menor faturamento: {menor}, Maior faturamento: {maior}, Dias acima da média: {diasAcima}");
        
        Console.WriteLine("\n4) Percentual de faturamento por estado:");
        PercentualFaturamento();
        
        Console.WriteLine("\n5) Inversão de string:");
        Console.Write("Digite uma string: ");
        string input = Console.ReadLine();
        string invertida = InverterString(input);
        Console.WriteLine($"Resposta: {invertida}");
    }

    static int SomaIndice()
    {
        int INDICE = 13, SOMA = 0, K = 0;
        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }
        return SOMA; // Resposta: 91
    }

    static bool VerificarFibonacci(int num)
    {
        int a = 0, b = 1, c = 0;
        while (c < num)
        {
            c = a + b;
            a = b;
            b = c;
        }
        return num == a || num == b;
    }

    static (double, double, int) AnaliseFaturamento()
    {
        string json = "{"faturamento": [2000, 3000, 2500, 0, 0, 4000, 5000, 6000, 0, 0, 7000, 8000, 0, 9000]}";
        var dados = JsonSerializer.Deserialize<FaturamentoMensal>(json);
        var faturamentos = dados.faturamento.Where(f => f > 0).ToList();
        
        double media = faturamentos.Average();
        int diasAcimaMedia = faturamentos.Count(f => f > media);
        
        return (faturamentos.Min(), faturamentos.Max(), diasAcimaMedia);
    }

    static void PercentualFaturamento()
    {
        var estados = new (string, double)[]
        {
            ("SP", 67836.43),
            ("RJ", 36678.66),
            ("MG", 29229.88),
            ("ES", 27165.48),
            ("Outros", 19849.53)
        };
        
        double total = estados.Sum(e => e.Item2);
        foreach (var (estado, valor) in estados)
        {
            Console.WriteLine($"{estado}: {valor / total * 100:F2}%");
        }
    }

    static string InverterString(string str)
    {
        char[] invertido = new char[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            invertido[i] = str[str.Length - 1 - i];
        }
        return new string(invertido);
    }
}

class FaturamentoMensal
{
    public double[] faturamento { get; set; }
}
