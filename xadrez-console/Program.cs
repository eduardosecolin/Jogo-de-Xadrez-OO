using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
             try {
                PartidaDeXadrez partida = new PartidaDeXadrez();  
                while(partida.terminada == false) {
                    try {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab);
                        Console.WriteLine();
                        Console.WriteLine("TURNO: " + partida.turno);
                        Console.WriteLine("AGUARDANDO JOGADA: " + partida.jogadorAtual);

                        Console.WriteLine();
                        Console.Write("ORIGEM: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        //mostrar o tabuleiro marcado com os possiveis movimentos da peça
                        bool[,] possiveisMovimentosMarcados = partida.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, possiveisMovimentosMarcados);

                        Console.WriteLine();
                        Console.Write("DESTINO: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarposicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }catch(TabuleiroException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }          
               
             }
             catch(TabuleiroException e) {
                 Console.WriteLine(e.Message);
             }
            Console.ReadKey();
        }
    }
}
