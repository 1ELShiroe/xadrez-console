using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Color color) : base(tab, color)
        {

        }
        public override string ToString()
        {
            return "T";
        }
        private bool moviewPermission(Position pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.color != color;
        }
        public override bool[,] moviesPossible()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Position pos = new Position(0, 0);

            //UP / ACIMA
            pos.defineValues(position.linha - 1, position.coluna);
            while(tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }
            //UPDOWN / ABAIXO
            pos.defineValues(position.linha + 1, position.coluna);
            while (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
            }
            //LEFT / DIREITA
            pos.defineValues(position.linha, position.coluna + 1);
            while (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }
            //RIGHT / ESQUERDA
            pos.defineValues(position.linha, position.coluna - 1);
            while (tab.PositionVal(pos) && moviewPermission(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).color != color)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }
            return mat;
            }
        }
}
