using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro {
    abstract class Peca {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimento { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor) {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qtdMovimento = 0;
        }
        public void incrementarQtdMovimentos() {
            qtdMovimento++;
        }
        public void decrementarQtdMovimentos() {
            qtdMovimento--;
        }
        // metodo que verifica se a peça esta bloqueada ou seja, verifica se a matriz do metodo abstrato
        // movimentosPossiveis() tem linhas ou colunas com retorno true
        public bool existeMovimentosPossiveis() {
            bool[,] mat = movimentosPossiveis();
            for(int i = 0; i < tab.linhas; i++) {
                for(int j = 0; j < tab.linhas; j++) {
                    if(mat[i,j] == true) {
                        return true;
                    }
                }
            }
            return false;
        }
        // metodo para verificar se pode mover para a posição passada como argumento
        public bool movimentoPossivel(Posicao pos) {
            return movimentosPossiveis()[pos.linha,pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
