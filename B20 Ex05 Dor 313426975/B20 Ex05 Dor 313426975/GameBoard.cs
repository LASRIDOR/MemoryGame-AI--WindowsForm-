using System;
using System.Collections.Generic;
using System.Drawing;

namespace MemoryLogic
{
    public class GameBoard
    {
        private readonly int r_NumOfRows;
        private readonly int r_NumOfCols;
        private Cube[,] m_Board;

        public GameBoard(int i_Rows, int i_Cols)
        {
            r_NumOfCols = i_Cols;
            r_NumOfRows = i_Rows;
            m_Board = new Cube[r_NumOfRows, r_NumOfCols];
        }

        public Cube[,] Board
        {
            get { return m_Board; }
        }

        public int NumOfRows
        {
            get { return r_NumOfRows; }
        }

        public int NumOfCols
        {
            get { return r_NumOfCols; }
        }

        public struct Cube
        {
            private readonly int r_SymbolOfIcon;
            private bool v_IsHidden;
            private Color m_Color;

            public Cube(int i_Icon, bool i_IsHidden, Color i_Color)
            {
                r_SymbolOfIcon = i_Icon;
                v_IsHidden = i_IsHidden;
                m_Color = i_Color;
            }

            public Color Color
            {
                set { m_Color = value; }
                get { return m_Color; }
            }

            internal bool IsHidden
            {
                get { return v_IsHidden; }
                set { IsHidden = value; }
            }

            // hide icon if havent exposed yet with 0 int (in ui logical 0 represent space)
            public int SymbolOfIcon
            {
                get
                {
                    int result;

                    if (IsHidden == true)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = r_SymbolOfIcon;
                    }

                    return result;
                }
            }

            internal void ExposeCube()
            {
                v_IsHidden = false;
            }

            internal void HideCube()
            {
                v_IsHidden = true;
            }
        }

        public int ExposeSymbolAndTakeValue(Point i_MovePoint)
        {
            m_Board[i_MovePoint.Y, i_MovePoint.X].ExposeCube();

            return m_Board[i_MovePoint.Y, i_MovePoint.X].SymbolOfIcon;
        }

        public int GetIconInPoint(Point i_MovePoint)
        {
            return m_Board[i_MovePoint.Y, i_MovePoint.X].SymbolOfIcon;
        }

        public void HideIcon(Point i_MovePoint)
        {
            m_Board[i_MovePoint.Y, i_MovePoint.X].HideCube();
        }

        public bool checkIfGamehasFinished()
        {
            bool v_GameOver = true;

            foreach (Cube cube in m_Board)
            {
                if (cube.IsHidden == true)
                {
                    v_GameOver = false;

                    break;
                }
            }

            return v_GameOver;
        }

        public bool CheckIfAlreadyExposed(Point i_CurrMovePoint)
        {
            return (m_Board[i_CurrMovePoint.Y, i_CurrMovePoint.X].IsHidden == false);
        }

        public void PaintCubeInColor(Point i_FirstMovePoint, Point i_SecondMovePoint, Color i_Color)
        {
            m_Board[i_FirstMovePoint.Y, i_FirstMovePoint.X].Color = i_Color;
            m_Board[i_SecondMovePoint.Y, i_SecondMovePoint.X].Color = i_Color;
        }

        public void HideAllCardAndMixCardAgain()
        {
            foreach (Cube cube in m_Board)
            {
                cube.HideCube();
            }

            boardPreparartion();
        }

        // make a mix of int according to the size of the board and divide them randomaly between the cubes
        private void boardPreparartion()
        {
            int numOfIcons = r_NumOfCols * r_NumOfRows;
            int numOfInserts = numOfIcons / 2;

            List<int> listOfIcon = new List<int>(numOfIcons);

            for (int i = 1; i < numOfInserts + 1; i++)
            {
                listOfIcon.Add(i);
                listOfIcon.Add(i);
            }

            mixingCardBeforeStart(ref listOfIcon);
        }

        private void mixingCardBeforeStart(ref List<int> io_ListOfIcon)
        {
            Random random = new Random();

            for (int i = 0; i < r_NumOfRows; i++)
            {
                for (int j = 0; j < r_NumOfCols; j++)
                {
                    // 0 saved to space symbol
                    int randomNumber = random.Next(0, io_ListOfIcon.Count);
                    m_Board[i, j] = new Cube(io_ListOfIcon[randomNumber], true, Color.DarkGray);
                    io_ListOfIcon.RemoveAt(randomNumber);
                }
            }
        }
    }
}
