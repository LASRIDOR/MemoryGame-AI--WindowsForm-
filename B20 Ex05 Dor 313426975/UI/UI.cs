using System;
using System.Collections.Generic;

namespace B20_Ex05
{
    internal class UI
    {
        private static readonly int sr_PageWidth = 50;
        internal static readonly int sr_SpaceForSingleCubeCols = 4;
        internal static readonly int sr_SpaceForSingleCubeRows = 2;
        private static readonly int sr_SpacesBetweenCoordinatesAndBoardAndEdges = 3;
        private static readonly int sr_LogRows = 6;
        private static readonly int sr_LogCols = 6;
        private static readonly int sr_PhyRows = (sr_LogRows * sr_SpaceForSingleCubeRows) + sr_SpacesBetweenCoordinatesAndBoardAndEdges;
        private static readonly int sr_PhyCols = (sr_LogCols * sr_SpaceForSingleCubeCols) + sr_SpacesBetweenCoordinatesAndBoardAndEdges;
        private static readonly int sr_MaxNumOfIconNeeded = sr_LogRows * sr_LogCols / 2;
        private static readonly char sr_SignOfPlaceForGameIcon = 'S';
        private static readonly char[,] sr_PresentationBoard;
        internal static readonly List<char> sr_RowSymbol;
        internal static readonly List<char> sr_ColSymbol;
        private static readonly List<char> sr_IconSymbolStorage;

        static UI()
        {
            sr_PresentationBoard = new char[sr_PhyRows, sr_PhyCols];
            sr_RowSymbol = new List<char>(sr_LogRows);
            sr_ColSymbol = new List<char>(sr_LogCols);
            sr_IconSymbolStorage = new List<char>(sr_MaxNumOfIconNeeded + 1);
            makeRowColSymbol();
            makeRandSymbolListOfIconAccordingToSizeOfBoard();
            makePresentationBoard();
        }

        private static void makeRowColSymbol()
        {
            char startOfAlphabet = 'A';
            char startOfNumbers = '1';

            for (int i = 0; i < 6; i++)
            {
                sr_ColSymbol.Add(startOfAlphabet);
                sr_RowSymbol.Add(startOfNumbers);
                startOfAlphabet++;
                startOfNumbers++;
            }
        }

        private static void makePresentationBoard()
        {
            List<char>.Enumerator rowListEnumerator = sr_RowSymbol.GetEnumerator();
            List<char>.Enumerator colListEnumerator = sr_ColSymbol.GetEnumerator();

            rowListEnumerator.MoveNext();
            colListEnumerator.MoveNext();

            for (int i = 0; i < sr_PhyRows; i++)
            {
                for (int j = 0; j < sr_PhyCols; j++)
                {
                    if (i == 0 && j % sr_SpaceForSingleCubeCols == 0 && j > 1)
                    {
                        sr_PresentationBoard[i, j] = colListEnumerator.Current;
                        colListEnumerator.MoveNext();
                    }
                    else if (i % sr_SpaceForSingleCubeRows == 1 && i > 1 && j == 0)
                    {
                        sr_PresentationBoard[i, j] = rowListEnumerator.Current;
                        rowListEnumerator.MoveNext();
                    }
                    else if (i % sr_SpaceForSingleCubeRows == 0 && j > 1 && i > 0)
                    {
                        sr_PresentationBoard[i, j] = '=';
                    }
                    else if (i % sr_SpaceForSingleCubeRows == 1 && i > 1 && j % sr_SpaceForSingleCubeCols == 2)
                    {
                        sr_PresentationBoard[i, j] = '|';
                    }
                    else if (i % sr_SpaceForSingleCubeRows == 1 && j % sr_SpaceForSingleCubeCols == 0 && i > 1 && j > 1)
                    {
                        sr_PresentationBoard[i, j] = sr_SignOfPlaceForGameIcon;
                    }
                    else
                    {
                        sr_PresentationBoard[i, j] = ' ';
                    }
                }
            }
        }

        internal int representTheBoardWithMove(GameBoard io_Board, Coordinate io_MoveCoordinate)
        {
            int symbolMove = io_Board.ExposeSymbolAndTakeValue(io_MoveCoordinate);

            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(io_Board);

            return symbolMove;
        }

        internal void announceOnTheWinner(Player io_PlayerOne, Player io_PlayerTwo)
        {
            string theWinner;
            string playerScorePresentation = string.Format("   {0} score is : {1} | {2} score is : {3}  ", io_PlayerOne.NameOfPlayer, io_PlayerOne.Score, io_PlayerTwo.NameOfPlayer, io_PlayerTwo.Score);

            printSign(playerScorePresentation);

            if (io_PlayerOne.Score > io_PlayerTwo.Score)
            {
                theWinner = io_PlayerOne.NameOfPlayer;
            }
            else if (io_PlayerOne.Score < io_PlayerTwo.Score)
            {
                theWinner = io_PlayerTwo.NameOfPlayer;
            }
            else
            {
                theWinner = "Both Of You";
            }

            string winnerAnnouncment = string.Format("The Winner Is {0}", theWinner);

            printSign(winnerAnnouncment);
        }

        internal static bool askForAnotherGame(string i_PlayerOneName, string i_PlayerTwoName)
        {
            string anotherGameQuestion = string.Format("{0} And {1}{2}Do You Want To Play Another Game (Yes Or No)", i_PlayerOneName, i_PlayerTwoName, Environment.NewLine);
            string playersAnswer;
            bool v_PlayerOneAndTwoIsDesicion;
            bool v_validAnswer;

            System.Console.WriteLine(anotherGameQuestion);

            do
            {
                playersAnswer = System.Console.ReadLine();
                v_validAnswer = CheckInput.CheckValidAnswerForAnotherGameQuestion(playersAnswer);
            }
            while (v_validAnswer == false);

            v_PlayerOneAndTwoIsDesicion = playersAnswer == "Yes" || playersAnswer == "yes";

            if (v_PlayerOneAndTwoIsDesicion == true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
            }

            return v_PlayerOneAndTwoIsDesicion;
        }

        internal static void printChoseSizeOfBoardForPlayerOne(string i_PlayerOneName)
        {
            string msg =
                string.Format(
                    "{0} I need you to determine the size of the board{1}Max size is 6 and Min size is 4 and make sure you enter an even number of square{1}Example of Input: 6x4", i_PlayerOneName, Environment.NewLine);

            System.Console.WriteLine(msg);
        }

        internal void printBoard(GameBoard io_Board)
        {
            int numOfRowsInCurrBoard = (io_Board.NumOfRows * sr_SpaceForSingleCubeRows) + sr_SpacesBetweenCoordinatesAndBoardAndEdges;
            int numOfColsInCurrBoard = (io_Board.NumOfCols * sr_SpaceForSingleCubeCols) + sr_SpacesBetweenCoordinatesAndBoardAndEdges;

            for (int i = 0; i < numOfRowsInCurrBoard; i++)
            {
                for (int j = 0; j < numOfColsInCurrBoard; j++)
                {
                    if (sr_PresentationBoard[i, j] == sr_SignOfPlaceForGameIcon)
                    {
                        Coordinate gameBoardCoordinate = Coordinate.FromPresentationBoardCoordinateToGameBoardCoordinate(i, j);
                        printIconInCube(io_Board.GetIconInCoordinate(gameBoardCoordinate));
                    }
                    else
                    {
                        System.Console.Write(sr_PresentationBoard[i, j]);
                    }
                }

                System.Console.Write(Environment.NewLine);
            }
        }

        internal static void printIconInCube(int i_SymbolOfIcon)
        {
            System.Console.Write(sr_IconSymbolStorage[i_SymbolOfIcon]);
        }

        internal void printMakeAMove(string i_PlayerOneName, string i_PlayerTwoName, SystemManager.eTurn io_PlayingPlayer)
        {
            string playingPlayer;

            if (io_PlayingPlayer == SystemManager.eTurn.PlayerOne)
            {
                playingPlayer = i_PlayerOneName;
            }
            else
            {
                playingPlayer = i_PlayerTwoName;
            }

            string msg = string.Format("{0} Make a Move:{1}", playingPlayer, Environment.NewLine);

            System.Console.WriteLine(msg);
        }

        internal static void printComputerMakingAMove()
        {
            System.Console.Write("RoboMatch Making His Move");
            System.Threading.Thread.Sleep(400);
            System.Console.Write(".");
            System.Threading.Thread.Sleep(400);
            System.Console.Write(".");
            System.Threading.Thread.Sleep(400);
            System.Console.Write(".");
            System.Threading.Thread.Sleep(400);
            System.Console.Write(".");
            System.Threading.Thread.Sleep(400);
            System.Console.WriteLine(".");
            System.Threading.Thread.Sleep(400);
        }

        // this program chose randomaly symbol from ASCII table between '~' - '!' For the symbol for each card
        // and makes list of symbol for UI
        private static void makeRandSymbolListOfIconAccordingToSizeOfBoard()
        {
            List<char> listOfAllPossibleIcon;
            int numOfCharMatchForIcon = '~' - '!';
            int numOfIconNeeded = sr_MaxNumOfIconNeeded;
            Random random = new Random();

            listOfAllPossibleIcon = makeListOfAllPossibleCharacters(numOfCharMatchForIcon);

            // plus one for space icon in symbol 0 (board return 0 if icon is hidden)
            sr_IconSymbolStorage.Add(' ');

            for (int i = 0; i < numOfIconNeeded; i++)
            {
                // range in ASCII table with only symbol (more the max board size)
                int randomNumber = random.Next(0, listOfAllPossibleIcon.Count);
                sr_IconSymbolStorage.Add(listOfAllPossibleIcon[randomNumber]);
                listOfAllPossibleIcon.RemoveAt(randomNumber);
            }
        }

        private static List<char> makeListOfAllPossibleCharacters(int i_NumOfCharMatchForIcon)
        {
            // range in ASCII table with only symbol (more the max board size)
            List<char> allPossibleChar = new List<char>(i_NumOfCharMatchForIcon);
            char startOfPossibleCharForIcon = '!';

            for (int i = 0; i < i_NumOfCharMatchForIcon; i++)
            {
                allPossibleChar.Add(startOfPossibleCharForIcon);
                startOfPossibleCharForIcon++;
            }

            return allPossibleChar;
        }

        internal static void printSign(string i_Title)
        {
            string firstAndLastLineOfRectangle = new string('-', sr_PageWidth);
            string spacesWithPlaceToEdgesOfRectangle = new string(' ', sr_PageWidth - 2);

            string oneSideOfSpacesWithPlaceToEdgesOfRectangleAndTitle =
                new string(' ', (sr_PageWidth - 1 - i_Title.Length) / 2);

            string middleOfRectangle = string.Format("|{0}|", spacesWithPlaceToEdgesOfRectangle);

            string middleOfRectangleTitleLine =
                string.Format("|{0}{1}{0}|", oneSideOfSpacesWithPlaceToEdgesOfRectangleAndTitle, i_Title);

            System.Console.WriteLine(firstAndLastLineOfRectangle);
            System.Console.WriteLine(middleOfRectangle);
            System.Console.WriteLine(middleOfRectangleTitleLine);
            System.Console.WriteLine(middleOfRectangle);
            System.Console.WriteLine(firstAndLastLineOfRectangle);
        }

        internal static void printPlayerLogin()
        {
            System.Console.WriteLine("Please Enter Your Name: ");
            System.Console.Write(Environment.NewLine);
        }

        internal static void printChoosingOfCompetitionForPlayerOne(string i_NameOfPlayerOne)
        {
            string msg = string.Format("{0} Press 1 If You Want To Play Against The Computer(AI){1}press 0 If You Want To Play Against Another Player", i_NameOfPlayerOne, Environment.NewLine);

            System.Console.WriteLine(msg);
        }
    }
}