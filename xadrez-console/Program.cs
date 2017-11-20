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
                 Tabuleiro tab = new Tabuleiro(8, 8);
                 tab.colocarPeca(new Torre(tab, Cor.Preto), new Posicao(0, 0));
                 tab.colocarPeca(new Bispo(tab, Cor.Preto), new Posicao(1, 3));
                 tab.colocarPeca(new Rei(tab, Cor.Preto), new Posicao(7, 5));
                 tab.colocarPeca(new Peao(tab, Cor.Branco), new Posicao(5, 2));
                Tela.imprimirTabuleiro(tab);
             }
             catch(TabuleiroException e) {
                 Console.WriteLine(e.Message);
             }
            Console.ReadKey();
        }
    }
}
