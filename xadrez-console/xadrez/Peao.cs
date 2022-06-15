using tabuleiro;

namespace xadrez
{

    class Peao : Peca
    {

        private Partida partida;

        public Peao(Tabuleiro tab, Color color, Partida partida) : base(tab, color)
        {
            this.partida = partida;
        }

        public Peao(Tabuleiro tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Position pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.color != color;
        }

        private bool livre(Position pos)
        {
            return tab.peca(pos) == null;
        }

        public override bool[,] moviesPossible()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Position pos = new Position(0, 0);

            if (color == Color.Branca)
            {
                pos.defineValues(position.linha - 1, position.coluna);
                if (tab.PositionVal(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 2, position.coluna);
                Position p2 = new Position(position.linha - 1, position.coluna);
                if (tab.PositionVal(p2) && livre(p2) && tab.PositionVal(pos) && livre(pos) && movies == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 1, position.coluna - 1);
                if (tab.PositionVal(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha - 1, position.coluna + 1);
                if (tab.PositionVal(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (position.linha == 3)
                {
                    Position esquerda = new Position(position.linha, position.coluna - 1);
                    if (tab.PositionVal(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Position direita = new Position(position.linha, position.coluna + 1);
                    if (tab.PositionVal(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else
            {
                pos.defineValues(position.linha + 1, position.coluna);
                if (tab.PositionVal(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 2, position.coluna);
                Position p2 = new Position(position.linha + 1, position.coluna);
                if (tab.PositionVal(p2) && livre(p2) && tab.PositionVal(pos) && livre(pos) && movies == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 1, position.coluna - 1);
                if (tab.PositionVal(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.defineValues(position.linha + 1, position.coluna + 1);
                if (tab.PositionVal(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #jogadaespecial en passant
                if (position.linha == 4)
                {
                    Position esquerda = new Position(position.linha, position.coluna - 1);
                    if (tab.PositionVal(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Position direita = new Position(position.linha, position.coluna + 1);
                    if (tab.PositionVal(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}