using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
    class Rei : Peca{

        private PartidaDeXadrez partida;
        //construtor recebendo como parametro um tabuleiro e uma cor de peça referenciando a casse mãe = base e uma partida de xadrez
        public Rei(Tabuleiro tab, Cor cor,PartidaDeXadrez partida): base(tab, cor) {
            this.partida = partida;
        }
        //metodo auxiliar para verificar se a peça pode se mover
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        //metodo para verificar se pode ser feito a jogada especial 'roque'
        public bool testeTorreParaRoque(Posicao pos) {
            Peca peca = tab.peca(pos);
            return peca != null && peca is Torre && peca.cor == cor && peca.qtdMovimento == 0;
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

            // # JOGADA ESPECIAL 'ROQUE'
            if (qtdMovimento == 0 && partida.xeque == false) {
                // roque pequeno
                Posicao posicaoTorre = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posicaoTorre)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                // roque grande
                Posicao posicaoTorre2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posicaoTorre2)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

        //imprimmir a letra referente a peça de xadrez, nesse caso R = de "Rei"
        public override string ToString() {
            return "R";
        }
    }
}
