namespace tabuleiro
{
    class Position
    {
        public int linha { get; set; }
        public int coluna { get; set; }

        public void defineValues(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }
        public Position(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }
    }
}
