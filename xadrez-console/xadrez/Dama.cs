using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
    class Dama : Peca{
        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        public override bool[,] movimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "D";
        }
    }
}
