using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console {
    class Tela {
        public static void imprimirTabuleiro(Tabuleiro tab) {
            for(int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for(int j = 0; j < tab.colunas; j++) {
                    if (tab.peca(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        imprimirPecaColorida(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void imprimirPecaColorida(Peca peca) {
            if(peca.cor == Cor.Branco) {
                ConsoleColor aux1 = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(peca);
                Console.ForegroundColor = aux1;
            }else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
