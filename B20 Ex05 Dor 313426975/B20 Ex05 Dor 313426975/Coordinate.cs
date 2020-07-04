namespace MemoryLogic
{
    public struct Coordinate
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public Coordinate(string move)
        {
            r_Col = move[0] - UI.sr_ColSymbol[0];
            r_Row = move[1] - UI.sr_RowSymbol[0];
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }

        // Presentation Board has Some Other Logical than Logic Game this Method makes coordinate for presentatiopn board with ui logical board
        internal static Coordinate FromPresentationBoardCoordinateToGameBoardCoordinate(int io_CurrRowCoordinateOfPresentationBoard, int io_CurrColCoordinateOfPresentationBoard)
        {
            return new Coordinate(io_CurrRowCoordinateOfPresentationBoard, io_CurrColCoordinateOfPresentationBoard);
        }

        // c'tor of UI presentation board make coordinate from logical of ui presentation board to logical of gameboard
        private Coordinate(int io_CurrRowCoordinateOfPresentationBoard, int io_CurrColCoordinateOfPresentationBoard)
        {
            r_Row = (io_CurrRowCoordinateOfPresentationBoard / UI.sr_SpaceForSingleCubeRows) - 1;
            r_Col = (io_CurrColCoordinateOfPresentationBoard / UI.sr_SpaceForSingleCubeCols) - 1;
        }
    }
}
