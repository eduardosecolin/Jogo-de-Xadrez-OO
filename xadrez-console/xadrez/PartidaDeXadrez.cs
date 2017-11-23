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
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca capturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (capturada != null) {
                capturadas.Add(capturada);
            }
            // JOGADA ESPECIAL 'ROQUE-PEQUENO' IMPLEMENTANDO
            if(p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdMovimentos();
                tab.colocarPeca(torre, destinoTorre);
            }
            // JOGADA ESPECIAL 'ROQUE-GRANDE' IMPLEMENTANDO
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdMovimentos();
                tab.colocarPeca(torre, destinoTorre);
            }
            // JOGADA ESPECIAL EN PASSANT IMPLEMENTANDO
            if (p is Peao) {
                if (origem.coluna != destino.coluna && capturada == null) {
                    Posicao posPeao;
                    if (p.cor == Cor.Branco) {
                        posPeao = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else {
                        posPeao = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    capturada = tab.retirarPeca(posPeao);
                    capturadas.Add(capturada);
                }
            }
            return capturada;
        }
        //desfaz o movimento para que o jogador não fique em xeque
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if(pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // JOGADA ESPECIAL 'ROQUE-PEQUENO' DESFAZENDO
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdMovimentos();
                tab.colocarPeca(torre, origemTorre);
            }
            // JOGADA ESPECIAL 'ROQUE-GRANDE' DESFAZENDO
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdMovimentos();
                tab.colocarPeca(torre, origemTorre);
            }
            // JOGADA ESPECIAL EN PASSANT DESFAZENDO
            if(p is Peao) {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posPeao;
                    if(p.cor == Cor.Branco) {
                        posPeao = new Posicao(3, destino.coluna);
                    }else {
                        posPeao = new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posPeao);
                }
            }
        }

        //realiza as jogadas em turno
        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca capturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, capturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tab.peca(destino);

            // # JOGADA ESPECIAL PROMOÇÃO
            if(p is Peao) {
                if((p.cor == Cor.Branco && destino.linha == 0) || (p.cor == Cor.Preto && destino.linha == 7)) {
                    tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    Peca cavalo = new Cavalo(tab, p.cor);
                    Peca bispo = new Bispo(tab, p.cor);
                    Peca torre = new Torre(tab, p.cor);
                        Console.WriteLine("Escolha a peça que substituira o Peão: ");
                        Console.WriteLine("DAMA(rainha) = 1");
                        Console.WriteLine("CAVALO       = 2");
                        Console.WriteLine("BISPO        = 3");
                        Console.WriteLine("TORRE        = 4");
                        Console.Write("Sua Escolha: ");
                        int num = int.Parse(Console.ReadLine());
                        switch (num) {
                            case 1:
                                tab.colocarPeca(dama, destino);
                                pecas.Add(dama);
                                break;
                            case 2:
                                tab.colocarPeca(cavalo, destino);
                                pecas.Add(cavalo);
                                break;
                            case 3:
                                tab.colocarPeca(bispo, destino);
                                pecas.Add(bispo);
                                break;
                            case 4:
                                tab.colocarPeca(torre, destino);
                                pecas.Add(torre);
                                break;
                            default:
                                throw new TabuleiroException("Escolha incorreta!, PERDEU A PEÇA!!!");
                    }
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }else {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudarJogador();
            }
            // # JOGADA ESPECIAL EN PASSANT
            vulneravelEnPassant = (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) ? p : null;
                    
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
            if(tab.peca(origem).movimentoPossivel(destino) == false) {
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

        //descobrir uma peça adversaria
        private Cor adversaria(Cor cor) {
            if(cor == Cor.Branco) {
                return Cor.Preto;
            }else {
                return Cor.Branco;
            }
        }





        /************************* IMPLEMENTAÇÃO DO CHEQUE ******************************************************************/

        //metodo que retorna o "Rei" com sua determinada cor --uns dos metodos para verificar se o rei esta em xeque--
        private Peca rei(Cor cor) {
            foreach (Peca item in pecasEmJogo(cor)) {
                if(item is Rei) {
                    return item;
                }
            }
            return null;
        }
        //metodo que verifica se o "Rei esta em xeque
        public bool estaEmXeque(Cor cor) {
            Peca R = rei(cor);
            if(R == null) {
                throw new TabuleiroException("Não tem rei da cor" + cor + " no tabuleiro!");
            }
            foreach (Peca item in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = item.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        /*********************************** FIM *********************************************************************************/


        /************************* IMPLEMENTAÇÃO DO CHEQUE-MATE ******************************************************************/
        public bool testeXequeMate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca item in pecasEmJogo(cor)) {
                bool[,] mat = item.movimentosPossiveis();
                for(int i = 0; i < tab.linhas; i++) {
                    for(int j = 0; j < tab.colunas; j++) {
                        if(mat[i,j] == true) {
                            Posicao origem = item.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /*********************************** FIM *********************************************************************************/



        //metodos para colocar peças no tabuleiro e adicionar no conjunto(HashSet<>) pecas
        public void colocarNovasPecas(char linha, int coluna, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(linha, coluna).toPosicao());
            pecas.Add(peca);
        }

        public void colocarPecas() {
            //JOGADOR 1
            colocarNovasPecas('a', 1, new Torre(tab, Cor.Branco));
            colocarNovasPecas('h', 1, new Torre(tab, Cor.Branco));
            colocarNovasPecas('f', 1, new Bispo(tab, Cor.Branco));
            colocarNovasPecas('g', 1, new Cavalo(tab, Cor.Branco));
            colocarNovasPecas('c', 1, new Bispo(tab, Cor.Branco));
            colocarNovasPecas('b', 1, new Cavalo(tab, Cor.Branco));
            colocarNovasPecas('d', 1, new Dama(tab, Cor.Branco));
            colocarNovasPecas('e', 1, new Rei(tab, Cor.Branco, this));
            colocarNovasPecas('a', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('b', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('c', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('d', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('e', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('f', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('g', 2, new Peao(tab, Cor.Branco, this));
            colocarNovasPecas('h', 2, new Peao(tab, Cor.Branco, this));

            // JOGADOR 2
            colocarNovasPecas('a', 8, new Torre(tab, Cor.Preto));
            colocarNovasPecas('h', 8, new Torre(tab, Cor.Preto));
            colocarNovasPecas('f', 8, new Bispo(tab, Cor.Preto));
            colocarNovasPecas('g', 8, new Cavalo(tab, Cor.Preto));
            colocarNovasPecas('c', 8, new Bispo(tab, Cor.Preto));
            colocarNovasPecas('b', 8, new Cavalo(tab, Cor.Preto));
            colocarNovasPecas('d', 8, new Dama(tab, Cor.Preto));
            colocarNovasPecas('e', 8, new Rei(tab, Cor.Preto, this));
            colocarNovasPecas('a', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('b', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('c', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('d', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('e', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('f', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('g', 7, new Peao(tab, Cor.Preto, this));
            colocarNovasPecas('h', 7, new Peao(tab, Cor.Preto, this));
        }
    }
}
