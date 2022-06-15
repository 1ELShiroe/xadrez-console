
using tabuleiro;

namespace xadrez
{
    class PositionXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PositionXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }
        public Position toPosition()
        {
            return new Position(8 - linha, coluna - 'a');
        }
        public override string ToString()
        {
            return " " + coluna + linha;
        }
    }
}
