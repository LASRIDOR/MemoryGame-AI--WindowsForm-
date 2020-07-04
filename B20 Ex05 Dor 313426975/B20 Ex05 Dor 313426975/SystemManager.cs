using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryLogic
{
    public class SystemManager
    {
        private UI m_Ui = new UI();
        private Player m_PlayerOne;
        private Player m_PlayerTwo;

        public enum eTurn
        {
            PlayerOne = 1,
            PlayerTwo = 2
        }

        public enum eMoveNum
        {
            FirstMove = 1,
            SecondMove = 2
        }

        private Player PlayerOne
        {
            get { return m_PlayerOne; }
            set { m_PlayerOne = value; }
        }

        private Player PlayerTwo
        {
            get { return m_PlayerTwo; }
            set { m_PlayerTwo = value; }
        }

        public void PlayMatchGame()
        {
            m_PlayerOne = playerOneLogin();
            m_PlayerTwo = playerTwoLogin(m_PlayerOne.NameOfPlayer);
            UI.printSign("Welcome To Dori's World");
            bool v_WantToPlayAnotherGame;

            do
            {
                GameBoard board = makeGameBoard();

                m_PlayerOne.NewGame(null, null);
                m_PlayerTwo.NewGame(board.NumOfRows, board.NumOfCols);
                m_Ui.printBoard(board);
                gameRoutineAndKeepScore(board);
                m_Ui.announceOnTheWinner(m_PlayerOne, m_PlayerTwo);
                v_WantToPlayAnotherGame = UI.askForAnotherGame(m_PlayerOne.NameOfPlayer, m_PlayerTwo.NameOfPlayer);
            }
            while (v_WantToPlayAnotherGame == true);
        }

        private Player playerOneLogin()
        {
            string nameOfPlayerOne;

            UI.printSign("Player One Login");
            UI.printPlayerLogin();
            nameOfPlayerOne = System.Console.ReadLine();
            exitIfQ(nameOfPlayerOne);
            Ex02.ConsoleUtils.Screen.Clear();

            return new Player(nameOfPlayerOne, false);
        }

        private Player playerTwoLogin(string io_NameOfPlayerOne)
        {
            string nameOfPlayerTwo = null;
            bool v_WantToPlayVsCompter;

            UI.printSign("Player Two Login");
            v_WantToPlayVsCompter = playerOneChoosingCompetition(io_NameOfPlayerOne);

            if (v_WantToPlayVsCompter == false)
            {
                UI.printPlayerLogin();
                nameOfPlayerTwo = System.Console.ReadLine();
                exitIfQ(nameOfPlayerTwo);
            }

            Ex02.ConsoleUtils.Screen.Clear();

            return new Player(nameOfPlayerTwo, v_WantToPlayVsCompter);
        }

        private bool playerOneChoosingCompetition(string i_NameOfPlayerOne)
        {
            string playerChoice;
            bool v_ValidInput;

            do
            {
                UI.printChoosingOfCompetitionForPlayerOne(i_NameOfPlayerOne);
                playerChoice = System.Console.ReadLine();
                exitIfQ(playerChoice);
                v_ValidInput = CheckInput.IsValidPlayerOneEnemyChoice(playerChoice);
            }
            while (v_ValidInput == false);

            return playerChoice == "1";
        }

        private GameBoard makeGameBoard()
        {
            string sizeOfBoard;
            string[] seperator = { "x" };
            string[] seperateSizeOfBoard;
            int rowOfBoard;
            int colOfBoard;
            bool v_SizeIsValid;

            UI.printSign("Choosing Board Size");

            do
            {
                UI.printChoseSizeOfBoardForPlayerOne(m_PlayerOne.NameOfPlayer);
                sizeOfBoard = System.Console.ReadLine();
                exitIfQ(sizeOfBoard);
                v_SizeIsValid = CheckInput.IsValidBoardSize(sizeOfBoard);
            }
            while (v_SizeIsValid == false);

            seperateSizeOfBoard = sizeOfBoard.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
            rowOfBoard = int.Parse(seperateSizeOfBoard[0]);
            colOfBoard = int.Parse(seperateSizeOfBoard[1]);
            Ex02.ConsoleUtils.Screen.Clear();

            return new GameBoard(rowOfBoard, colOfBoard);
        }

        private void gameRoutineAndKeepScore(GameBoard io_Board)
        {
            eTurn playerTurn = eTurn.PlayerOne;

            while (io_Board.checkIfGamehasFinished() == false)
            {
                playerMakeMoveHisTurn(io_Board, playerTurn);
                switchTurn(ref playerTurn);
            }
        }

        private void playerMakeMoveHisTurn(GameBoard io_Board, eTurn i_PlayingPlayer)
        {
            eMoveNum moveNum = eMoveNum.FirstMove;
            Coordinate firstMoveCoordinate =
                askPlayingPlayerForMoveCheckMoveAndMakeCoordinate(io_Board, i_PlayingPlayer, moveNum, null);
            int symbolOfFirstMove;

            symbolOfFirstMove = m_Ui.representTheBoardWithMove(io_Board, firstMoveCoordinate);

            moveNum = eMoveNum.SecondMove;
            Coordinate secondMoveCoordinate = askPlayingPlayerForMoveCheckMoveAndMakeCoordinate(io_Board, i_PlayingPlayer, moveNum, symbolOfFirstMove);

            int symbolOfSecondMove;

            symbolOfSecondMove = m_Ui.representTheBoardWithMove(io_Board, secondMoveCoordinate);

            if (symbolOfSecondMove != symbolOfFirstMove)
            {
                System.Threading.Thread.Sleep(2000);
                cancelLastPlayingPlayerPlay(io_Board, firstMoveCoordinate, secondMoveCoordinate);
            }
            else
            {
                if (i_PlayingPlayer == eTurn.PlayerOne)
                {
                    m_PlayerOne.GivePlayerOnePoint();
                }
                else
                {
                    m_PlayerTwo.GivePlayerOnePoint();
                }
            }

            if (m_PlayerTwo.IsAi() == true)
            {
                m_PlayerTwo.AiBrain.SetCardRevealedFromLastMove(firstMoveCoordinate, symbolOfFirstMove, secondMoveCoordinate, symbolOfSecondMove);
            }
        }

        private Coordinate askPlayingPlayerForMoveCheckMoveAndMakeCoordinate(GameBoard io_Board, eTurn io_PlayingPlayer, eMoveNum i_MoveNum, int? i_SymbolOfFirstMoveCardRevealed)
        {
            Coordinate moveCoordinate;
            bool v_AlreadyExposed;

            do
            {
                m_Ui.printMakeAMove(m_PlayerOne.NameOfPlayer, m_PlayerTwo.NameOfPlayer, io_PlayingPlayer);
                moveCoordinate = askPlayingPlayerForMoveAndMakeCoordinate(io_Board, io_PlayingPlayer, i_MoveNum, i_SymbolOfFirstMoveCardRevealed);
                v_AlreadyExposed = CheckInput.IssueErrorMessageExposedCube(CheckIfCoordinateisExposed(io_Board, moveCoordinate));
            }
            while (v_AlreadyExposed == true);

            return moveCoordinate;
        }

        private bool CheckIfCoordinateisExposed(GameBoard io_Board, Coordinate io_CurrMoveCoordinate)
        {
            return io_Board.CheckIfAlreadyExposed(io_CurrMoveCoordinate);
        }

        private Coordinate askPlayingPlayerForMoveAndMakeCoordinate(GameBoard io_Board, eTurn io_PlayingPlayer, eMoveNum io_MoveNum, int? i_SymbolOfFirstMoveCardRevealed)
        {
            Coordinate moveCoordinate;

            if (io_PlayingPlayer == eTurn.PlayerOne)
            {
                moveCoordinate = getMoveFromHumanPlayerAndMakeCoordinate(io_Board);
            }
            else
            {
                if (m_PlayerTwo.IsAi() == true)
                {
                    UI.printComputerMakingAMove();

                    if (io_MoveNum == eMoveNum.FirstMove)
                    {
                        moveCoordinate = m_PlayerTwo.AiBrain.MakingFirstMove();
                    }
                    else
                    {
                        moveCoordinate = m_PlayerTwo.AiBrain.MakingSecondMove(i_SymbolOfFirstMoveCardRevealed.Value);
                    }

                    System.Console.Write(UI.sr_ColSymbol[moveCoordinate.Col]);
                    System.Console.WriteLine(UI.sr_RowSymbol[moveCoordinate.Row]);
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    moveCoordinate = getMoveFromHumanPlayerAndMakeCoordinate(io_Board);
                }
            }

            return moveCoordinate;
        }

        private Coordinate getMoveFromHumanPlayerAndMakeCoordinate(GameBoard io_Board)
        {
            string moveInputFromPlayer;
            bool v_MoveIsValid;

            do
            {
                moveInputFromPlayer = System.Console.ReadLine();
                exitIfQ(moveInputFromPlayer);
                v_MoveIsValid = CheckInput.IsValidMove(moveInputFromPlayer, io_Board.NumOfRows, io_Board.NumOfCols);
            }
            while (v_MoveIsValid == false);

            return new Coordinate(moveInputFromPlayer);
        }

        private void cancelLastPlayingPlayerPlay(GameBoard io_Board, Coordinate i_FirstMoveCoordinate, Coordinate i_SecondMoveCoordinate)
        {
            io_Board.HideIcon(i_FirstMoveCoordinate);
            io_Board.HideIcon(i_SecondMoveCoordinate);
            Ex02.ConsoleUtils.Screen.Clear();
            m_Ui.printBoard(io_Board);
        }

        private void switchTurn(ref eTurn o_PlayerCurrentTurn)
        {
            if (o_PlayerCurrentTurn == eTurn.PlayerOne)
            {
                o_PlayerCurrentTurn = eTurn.PlayerTwo;
            }
            else
            {
                o_PlayerCurrentTurn = eTurn.PlayerOne;
            }
        }

        private void exitIfQ(string io_Move)
        {
            if (io_Move == "Q")
            {
                Environment.Exit(1);
            }
        }
    }
}
