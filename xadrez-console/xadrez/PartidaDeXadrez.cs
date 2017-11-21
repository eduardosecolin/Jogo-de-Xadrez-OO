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

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca capturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
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

        public void colocarPecas() {
            //JOGADOR 1
            tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('a', 1).toPosicao());
            //tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('h', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branco), new PosicaoXadrez('b', 1).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Branco), new PosicaoXadrez('g', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('c', 1).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('f', 1).toPosicao());
            //tab.colocarPeca(new Dama(tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('e', 1).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('a', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('b', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('c', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('d', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('e', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('f', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('g', 2).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('h', 2).toPosicao());
            // JOGADOR 2
            //tab.colocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('a', 8).toPosicao());
            //tab.colocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('h', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preto), new PosicaoXadrez('b', 8).toPosicao());
            //tab.colocarPeca(new Cavalo(tab, Cor.Preto), new PosicaoXadrez('g', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preto), new PosicaoXadrez('c', 8).toPosicao());
            //tab.colocarPeca(new Bispo(tab, Cor.Preto), new PosicaoXadrez('f', 8).toPosicao());
            //tab.colocarPeca(new Dama(tab, Cor.Preto), new PosicaoXadrez('d', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preto), new PosicaoXadrez('e', 8).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('a', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('b', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('c', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('d', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('e', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('f', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('g', 7).toPosicao());
            //tab.colocarPeca(new Peao(tab, Cor.Preto), new PosicaoXadrez('h', 7).toPosicao());
        }
    }
}
