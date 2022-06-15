using tabuleiro;

namespace xadrez
{

    class Dama : Peca
    {

        public Dama(Tabuleiro tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "D";
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

            // esquerda
            pos.defineValues(position.linha, position.coluna - 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha, pos.coluna - 1);
            }

            // direita
            pos.defineValues(position.linha, position.coluna + 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha, pos.coluna + 1);
            }

            // acima
            pos.defineValues(position.linha - 1, position.coluna);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha - 1, pos.coluna);
            }

            // abaixo
            pos.defineValues(position.linha + 1, position.coluna);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha + 1, pos.coluna);
            }

            // NO
            pos.defineValues(position.linha - 1, position.coluna - 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha - 1, pos.coluna - 1);
            }

            // NE
            pos.defineValues(position.linha - 1, position.coluna + 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha - 1, pos.coluna + 1);
            }

            // SE
            pos.defineValues(position.linha + 1, position.coluna + 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha + 1, pos.coluna + 1);
            }

            // SO
            pos.defineValues(position.linha + 1, position.coluna - 1);
            while (tab.PositionVal(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.linha + 1, pos.coluna - 1);
            }

            return mat;
        }
    }
}