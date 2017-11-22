using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
    class Bispo : Peca{
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            //definindo as posições
            Posicao pos = new Posicao(0, 0);
            //posição diagonal acima esquerda
            pos.definirValores(posicao.linha - 1, posicao.coluna -1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1);
            }
            //posição diagonal abaixo esquerda
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }
            //posição diagonal acima direita
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }
            //posição diagonal abaixo direita
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }
            return mat;
        }

        public override string ToString() {
            return "B";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }


    }
}
