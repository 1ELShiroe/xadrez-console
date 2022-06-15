using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private Partida partida;
        public Rei(Tabuleiro tab, Color color, Partida partida) : base(tab, color)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }
        private bool moviewPermission(Position pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.color != color;
        }
        private bool testeTorreROQUE(Position pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.color == color && p.movies == 0;
        }
        public override bool[,] moviesPossible()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Position pos = new Position(0, 0);

            //UP / ACIMA
            pos.defineValues(position.linha - 1, position.coluna);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //NE
            pos.defineValues(position.linha - 1, position.coluna + 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Left / Direita
            pos.defineValues(position.linha, position.coluna + 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //SE
            pos.defineValues(position.linha + 1, position.coluna + 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //UPDOWN / Abaixo
            pos.defineValues(position.linha + 1, position.coluna);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //RIGHT / Esquerda
            pos.defineValues(position.linha, position.coluna - 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //SO
            pos.defineValues(position.linha + 1, position.coluna - 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //NO
            pos.defineValues(position.linha - 1, position.coluna - 1);
            if (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Jogada especial - ROQUE
            if(movies == 0 && !partida.xeque)
            {
                // Roque pequeno.
                Position PosTP = new Position(position.linha, position.coluna + 3);
                if (testeTorreROQUE(PosTP))
                {
                    Position p1 = new Position(position.linha, position.coluna + 1);
                    Position p2 = new Position(position.linha, position.coluna + 2);
                    if(tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[position.linha, position.coluna + 2] = true;
                    }
                }
                // Roque Grande.
                Position PosTG = new Position(position.linha, position.coluna - 4);
                if (testeTorreROQUE(PosTG))
                {
                    Position p1 = new Position(position.linha, position.coluna - 1);
                    Position p2 = new Position(position.linha, position.coluna - 2);
                    Position p3 = new Position(position.linha, position.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null )
                    {
                        mat[position.linha, position.coluna - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}