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
        //imprimmir a letra referente a peça de xadrez, nesse caso R = de "Rei"
        public override string ToString() {
            return "R";
        }
    }
}
