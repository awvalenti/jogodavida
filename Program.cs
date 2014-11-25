using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVidaConsole
{
    class Program
    {
        private int altura, largura;
        private bool[,] tabuleiro, copia;
        private Random random = new Random();

        public Program(int largura, int altura)
        {
            this.largura = largura;
            this.altura = altura;
            tabuleiro = new bool[altura, largura];
            copia = new bool[altura, largura];
        }

        public void Rodar()
        {
            Aleatorizar();
            for (int geracao = 0; ; ++geracao)
            {
                Desenhar();
                Gerar();
                Console.WriteLine("\nGeracao {0}", geracao);
                Console.SetCursorPosition(0, 0);
            }
        }

        private void Gerar()
        {
            for (int i = 0; i < altura; ++i)
            {
                for (int j = 0; j < largura; ++j)
                {
                    copia[i, j] = tabuleiro[i, j];
                    tabuleiro[i, j] = DeveFicarViva(i, j);
                }
            }
        }

        private bool DeveFicarViva(int i, int j)
        {
            bool estavaViva = copia[i, j];
            int vizinhosVivos = QuantidadeVizinhosQueEstavamVivos(i, j);
            return vizinhosVivos == 3 || estavaViva && vizinhosVivos == 2;
        }

        private int QuantidadeVizinhosQueEstavamVivos(int i, int j)
        {
            int cont = 0;
            for (int incI = -1; incI <= 1; ++incI)
            {
                for (int incJ = -1; incJ <= 1; ++incJ)
                {
                    //Mundo ciclico nas bordas
                    if ((incI != 0 || incJ != 0) && copia[(i + incI + altura) % altura, (j + incJ + largura) % largura]) ++cont;

                    //Mundo termina nas bordas
                    //if ((incI != 0 || incJ != 0) && DentroDosLimites(i + incI, altura) && DentroDosLimites(j + incJ, largura) && copia[i + incI, j + incJ]) ++cont;
                }
            }
            return cont;
        }

        private bool DentroDosLimites(int indice, int maximo)
        {
            return indice >= 0 && indice < maximo;
        }

        private void Desenhar()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < altura; ++i)
            {
                for (int j = 0; j < largura; ++j)
                {
                    sb.AppendFormat("{0} ", tabuleiro[i, j] ? 'O' : ' ');
                }
                sb.AppendLine();
            }
            var corAnterior = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(sb);
            Console.ForegroundColor = corAnterior;
        }

        private void Aleatorizar()
        {
            for (int i = 0; i < altura; ++i)
            {
                for (int j = 0; j < largura; ++j)
                {
                    copia[i, j] = tabuleiro[i, j] = random.Next(30) == 0;
                }
            }
        }

        static void Main(string[] args)
        {
            int largura = 215, altura = 150;
            new Program(largura, altura).Rodar();
        }
    }
}
