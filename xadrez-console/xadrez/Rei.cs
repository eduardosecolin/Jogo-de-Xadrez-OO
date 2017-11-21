using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
    class Rei : Peca{
        //construtor recebendo como parametro um tabuleiro e uma cor de peça referenciando a casse mãe = base
        public Rei(Tabuleiro tab, Cor cor): base(tab, cor) {
        }
        //metodo auxiliar para verificar se a peça pode se mover
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }
        //classe abstrada implementada, referente a classe mãe(super classe)
        public override bool[,] movimentosPossiveis() {                    
            bool[,] mat = new bool[tab.linhas, tab.colunas];               
            //definindo as posições
            Posicao pos = new Posicao(0, 0);
            //posição acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if(tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição acima diagonal direita
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição abaixo diagonal direita
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição abaixo diagonal esquerda
            pos.definirValores(posicao.linha +1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição esquerda
            pos.definirValores(posicao.linha , posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //posição acima diagonal esquerda
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }

        //imprimmir a letra referente a peça de xadrez, nesse caso R = de "Rei"
        public override string ToString() {
            return "R";
        }
    }
}
