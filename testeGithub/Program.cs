using System;
using System.Collections.Generic;
using System.Linq;

namespace PomarDeMacas
{
    class Program
    {
        static void Main(string[] args)
        {
            Pomar meuPomar = new Pomar("Pomar Primavera");

            // Adiciona árvores com maçãs aleatórias
            meuPomar.PlantarArvores(10); // 10 árvores

            // Exibe relatório completoeee
            meuPomar.ExibirRelatorio();

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }

    class Maca
    {
        public double PesoGramas { get; set; }
        public bool EstaBoa { get; set; }

        public Maca(double peso, bool estaBoa)
        {
            PesoGramas = peso;
            EstaBoa = estaBoa;
        }
    }

    class Arvore
    {
        public List<Maca> Macas { get; set; }
        private static Random rand = new Random();

        public Arvore()
        {
            Macas = new List<Maca>();
            GerarMacas();
        }

        // Gera entre 20 e 50 maçãs por árvore
        private void GerarMacas()
        {
            int qtd = rand.Next(20, 51);
            for (int i = 0; i < qtd; i++)
            {
                double peso = rand.NextDouble() * (300 - 100) + 100; // entre 100g e 300g
                bool estaBoa = rand.NextDouble() > 0.2; // 80% de chance de ser boa
                Macas.Add(new Maca(peso, estaBoa));
            }
        }

        public int TotalMacas => Macas.Count;
        public int MacasBoas => Macas.Count(m => m.EstaBoa);
        public int MacasRuins => Macas.Count(m => !m.EstaBoa);
        public double PesoTotal => Macas.Sum(m => m.PesoGramas);
    }

    class Pomar
    {
        public string Nome { get; set; }
        public List<Arvore> Arvores { get; set; }

        public Pomar(string nome)
        {
            Nome = nome;
            Arvores = new List<Arvore>();
        }

        public void PlantarArvores(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                Arvores.Add(new Arvore());
            }
        }

        public void ExibirRelatorio()
        {
            Console.WriteLine($"Relatório do {Nome}");
            Console.WriteLine(new string('-', 40));

            int totalMacas = 0, totalBoas = 0, totalRuins = 0;
            double pesoTotal = 0;

            for (int i = 0; i < Arvores.Count; i++)
            {
                var arvore = Arvores[i];
                Console.WriteLine($"Árvore {i + 1}:");
                Console.WriteLine($"  Total de maçãs: {arvore.TotalMacas}");
                Console.WriteLine($"  Maçãs boas: {arvore.MacasBoas}");
                Console.WriteLine($"  Maçãs ruins: {arvore.MacasRuins}");
                Console.WriteLine($"  Peso total: {arvore.PesoTotal:F2} g\n");

                totalMacas += arvore.TotalMacas;
                totalBoas += arvore.MacasBoas;
                totalRuins += arvore.MacasRuins;
                pesoTotal += arvore.PesoTotal;
            }

            Console.WriteLine(new string('=', 40));
            Console.WriteLine("Resumo Geral do Pomar:");
            Console.WriteLine($"  Árvores: {Arvores.Count}");
            Console.WriteLine($"  Total de maçãs: {totalMacas}");
            Console.WriteLine($"  Maçãs boas: {totalBoas} ({(double)totalBoas / totalMacas * 100:F1}%)");
            Console.WriteLine($"  Maçãs ruins: {totalRuins} ({(double)totalRuins / totalMacas * 100:F1}%)");
            Console.WriteLine($"  Peso total de maçãs: {pesoTotal / 1000:F2} kg");
            Console.WriteLine(new string('=', 40));
        }
    }
}
