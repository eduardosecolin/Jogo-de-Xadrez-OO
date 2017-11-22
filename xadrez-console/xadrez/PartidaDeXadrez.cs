using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;


// Essa Classe terá toda a mecânica do jogo

namespace xadrez {
    class PartidaDeXadrez {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca capturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (capturada != null) {
                capturadas.Add(capturada);
            }
        }
        //realiza as jogadas em turno
        public void realizaJogada(Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            turno++;
            mudarJogador();
        }
        //tratando os possiveis erros na escolha das peças e movimentos de origem - metodo que valida a posição de origem
        public void validarPosicaoDeOrigem(Posicao pos) {
            if(tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if(tab.peca(pos).existeMovimentosPossiveis() == false) {
                throw new TabuleiroException("Não há movimentos para a peça de origem escolhida!");
            }
        }
        public void validarposicaoDeDestino(Posicao origem, Posicao destino) {
            if(tab.peca(origem).podeMover(destino) == false) {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }
        //muda de jogador para dar a logica de jogadas em turno
        public void mudarJogador() {
            jogadorAtual = (jogadorAtual == Cor.Branco) ? Cor.Preto : Cor.Branco;
        }
        //metodo para separar as peças capturadas pela sua cor
        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in capturadas) {
                if(item.cor == cor) {
                    aux.Add(item);
                }
            }
            return aux;
        }
        //guardando no conjunto (HashSet<>) pecas as peças que estão em jogo no tabuleiro, separando das peças capturadas
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in pecas) {
                if (item.cor == cor) {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        //metodos para colocar peças no tabuleiro e adicionar no conjunto(HashSet<>) pecas
        public void colocarNovasPecas(char linha, int coluna, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            pecas.Add(peca);
        }

        public void colocarPecas() {
            //JOGADOR 1
            colocarNovasPecas('a', 1, new Torre(tab, Cor.Branco));
            //colocarNovasPecas('h', 1, new Torre(tab, Cor.Branco));
            //colocarNovasPecas('b', 1, new Bispo(tab, Cor.Branco));
            //colocarNovasPecas('g', 1, new Cavalo(tab, Cor.Branco));
            //colocarNovasPecas('c', 1, new Bispo(tab, Cor.Branco));
            //colocarNovasPecas('f', 1, new Cavalo(tab, Cor.Branco));
            //colocarNovasPecas('d', 1, new Dama(tab, Cor.Branco));
            colocarNovasPecas('e', 1, new Rei(tab, Cor.Branco));
            //colocarNovasPecas('a', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('b', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('c', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('d', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('e', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('f', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('g', 2, new Peao(tab, Cor.Branco));
            //colocarNovasPecas('h', 2, new Peao(tab, Cor.Branco));

            // JOGADOR 2
            colocarNovasPecas('a', 8, new Torre(tab, Cor.Preto));
            //colocarNovasPecas('h', 8, new Torre(tab, Cor.Preto));
            //colocarNovasPecas('b', 8, new Bispo(tab, Cor.Preto));
            //colocarNovasPecas('g', 8, new Cavalo(tab, Cor.Preto));
            //colocarNovasPecas('c', 8, new Bispo(tab, Cor.Preto));
            //colocarNovasPecas('f', 8, new Cavalo(tab, Cor.Preto));
            //colocarNovasPecas('d', 8, new Dama(tab, Cor.Preto));
            colocarNovasPecas('e', 8, new Rei(tab, Cor.Preto));
            //colocarNovasPecas('a', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('b', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('c', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('d', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('e', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('f', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('g', 7, new Peao(tab, Cor.Preto));
            //colocarNovasPecas('h', 7, new Peao(tab, Cor.Preto));
        }
    }
}
