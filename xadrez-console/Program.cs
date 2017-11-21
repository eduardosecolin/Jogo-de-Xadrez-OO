﻿using System;
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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("ORIGEM: ");                   
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    //mostrar o tabuleiro marcado com os possiveis movimentos da peça
                    bool[,] possiveisMovimentosMarcados = partida.tab.peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab, possiveisMovimentosMarcados);

                    Console.WriteLine();
                    Console.Write("DESTINO: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executaMovimento(origem, destino);
                }          
               
             }
             catch(TabuleiroException e) {
                 Console.WriteLine(e.Message);
             }
            Console.ReadKey();
        }
    }
}
