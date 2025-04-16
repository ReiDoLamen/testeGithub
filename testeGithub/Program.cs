using System;
using System.Collections.Generic;
using System.Linq;

namespace PomarInterativo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Suporte a acentuação e emojis

            Console.WriteLine("🌳 Bem-vindo ao Simulador de Pomar de Maçãs!\n");

            Console.Write("Dê um nome para o seu pomar: ");
            string nome = Console.ReadLine();

            Pomar pomar = CriarNovoPomar(nome);

            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine($"📋 Menu Principal - {pomar.Nome}");
                Console.WriteLine("1 - Ver relatório geral do pomar");
                Console.WriteLine("2 - Ver relatório de uma árvore específica");
                Console.WriteLine("3 - Simular nova safra");
                Console.WriteLine("0 - Sair do sistema e encerrar o programa");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        pomar.ExibirRelatorioGeral();
                        break;
                    case "2":
                        pomar.ExibirRelatorioArvore();
                        break;
                    case "3":
                        pomar = CriarNovoPomar(pomar.Nome);
                        Console.WriteLine("🌱 Nova safra simulada com sucesso!");
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("❌ Opção inválida.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\n👋 Obrigado por usar o simulador!");
        }

        static Pomar CriarNovoPomar(string nome)
        {
            Console.Clear();
            Console.Write($"Quantas árvores deseja plantar no pomar \"{nome}\"? ");
            int quantidade;
            while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
            {
                Console.Write("Digite um número válido maior que zero: ");
            }

            Pomar novoPomar = new Pomar(nome);
            novoPomar.PlantarArvores(quantidade);
            return novoPomar;
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

        private void GerarMacas()
        {
            int qtd = rand.Next(20, 51);
            for (int i = 0; i < qtd; i++)
            {
                double peso = rand.NextDouble() * (300 - 100) + 100; // entre 100g e 300g
                bool estaBoa = rand.NextDouble() > 0.2; // 80% chance de ser boa
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
            Arvores.Clear();
            for (int i = 0; i < quantidade; i++)
            {
                Arvores.Add(new Arvore());
            }
        }

        public void ExibirRelatorioGeral()
        {
            int totalMacas = 0, totalBoas = 0, totalRuins = 0;
            double pesoTotal = 0;

            for (int i = 0; i < Arvores.Count; i++)
            {
                var arvore = Arvores[i];
                totalMacas += arvore.TotalMacas;
                totalBoas += arvore.MacasBoas;
                totalRuins += arvore.MacasRuins;
                pesoTotal += arvore.PesoTotal;
            }

            Console.WriteLine($"📊 Relatório Geral do {Nome}");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"🌲 Árvores: {Arvores.Count}");
            Console.WriteLine($"🍎 Total de maçãs: {totalMacas}");
            Console.WriteLine($"✅ Maçãs boas: {totalBoas} ({(double)totalBoas / totalMacas * 100:F1}%)");
            Console.WriteLine($"❌ Maçãs ruins: {totalRuins} ({(double)totalRuins / totalMacas * 100:F1}%)");
            Console.WriteLine($"⚖️ Peso total: {(pesoTotal / 1000):F2} kg");
            Console.WriteLine(new string('-', 40));
        }

        public void ExibirRelatorioArvore()
        {
            Console.Write($"Digite o número da árvore (1 a {Arvores.Count}): ");
            int numero;
            if (!int.TryParse(Console.ReadLine(), out numero) || numero < 1 || numero > Arvores.Count)
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            var arvore = Arvores[numero - 1];
            Console.WriteLine($"\n🌳 Relatório da Árvore {numero}");
            Console.WriteLine($"🍏 Maçãs totais: {arvore.TotalMacas}");
            Console.WriteLine($"✅ Boas: {arvore.MacasBoas}");
            Console.WriteLine($"❌ Ruins: {arvore.MacasRuins}");
            Console.WriteLine($"⚖️ Peso total: {arvore.PesoTotal:F2} g");
        }
    }
}
