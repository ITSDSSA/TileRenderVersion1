namespace TileManagerNS
{
    public  class TileRef
    {
        private int sheetPosX;
        private int sheetPosY;
        private int tileMapValue;

        public int SheetPosX
        {
            get
            {
                return sheetPosX;
            }

            set
            {
                sheetPosX = value;
            }
        }

        public int SheetPosY
        {
            get
            {
                return sheetPosY;
            }

            set
            {
                sheetPosY = value;
            }
        }

        public int TileMapValue
        {
            get
            {
                return tileMapValue;
            }

            set
            {
                tileMapValue = value;
            }
        }

        public TileRef(int x, int y, int val)
        {
            SheetPosX = x;
            SheetPosY = y;
            TileMapValue = val;
        }
    }
}