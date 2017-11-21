using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace xadrez_console {
    class Tela {
        public static void imprimirTabuleiro(Tabuleiro tab) {
            for(int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for(int j = 0; j < tab.colunas; j++) {
                        imprimirPecaColorida(tab.peca(i, j));              
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }
        // sobrecarga no metodo imprimirTabuleiro para mostrar as possiveis jogadas na classe Program.cs
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] possiveisMovimentos) {
            // alterando a cor do fundo do console, marcando as possiveis jogadas
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++) {
                    Console.BackgroundColor = (possiveisMovimentos[i, j] == true) ? fundoAlterado : fundoOriginal; 
                    imprimirPecaColorida(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = fundoOriginal;
        }

        public static void imprimirPecaColorida(Peca peca) {
            if (peca == null) {
                Console.Write("- ");
            }
            else {
                if (peca.cor == Cor.Branco) {
                    ConsoleColor aux1 = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca);
                    Console.ForegroundColor = aux1;
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez() {
            string posicao = Console.ReadLine();
            char coluna = posicao[0];
            int linha = int.Parse(posicao[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

    }
}
