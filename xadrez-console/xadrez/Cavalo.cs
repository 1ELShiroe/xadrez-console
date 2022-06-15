using tabuleiro;

namespace xadrez
{

    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Position pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.color != color;
        }

        public override bool[,] moviesPossible()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Position pos = new Position(0, 0);

            pos.defineValues(position.linha - 1, position.coluna - 2);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha - 2, position.coluna - 1);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha - 2, position.coluna + 1);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha - 1, position.coluna + 2);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha + 1, position.coluna + 2);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha + 2, position.coluna + 1);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha + 2, position.coluna - 1);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.defineValues(position.linha + 1, position.coluna - 2);
            if (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }
    }
}