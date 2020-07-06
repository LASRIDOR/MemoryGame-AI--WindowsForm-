using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MemoryLogic
{
    public class SystemManager
    {
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private GameBoard m_GameBoard;
        private eTurn m_PlayerTurn;
        private eMoveNum m_MoveNum;
        private Point m_FirstMovePoint;
        private Point m_SecondMovePoint;
        private int m_FirstMoveSymbol;
        private int m_SecondMoveSymbol;

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

        public Player PlayerOne
        {
            get { return m_PlayerOne; }
            set { m_PlayerOne = value; }
        }

        public Player PlayerTwo
        {
            get { return m_PlayerTwo; }
            set { m_PlayerTwo = value; }
        }

        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public eTurn PlayerTurn
        {
            get { return m_PlayerTurn; }
            set { m_PlayerTurn = value; }
        }

        public void PlayMatchGame()
        {
            bool v_WantToPlayAnotherGame;

           // do
           // {
                m_PlayerOne.NewGame(null, null);
                m_PlayerTwo.NewGame(m_Board.NumOfRows, m_Board.NumOfCols);
                //m_Ui.printBoard(board);
                gameRoutineAndKeepScore(m_Board);
                //m_Ui.announceOnTheWinner(m_PlayerOne, m_PlayerTwo);
                //v_WantToPlayAnotherGame = UI.askForAnotherGame(m_PlayerOne.NameOfPlayer, m_PlayerTwo.NameOfPlayer);
         //   }
           // while (v_WantToPlayAnotherGame == true);
        }
        /*
        private void gameRoutineAndKeepScore(GameBoard io_Board)
        {
            while (io_Board.checkIfGamehasFinished() == false)
            {
                playingPlayerMakeMoveHisTurn();

                if (m_MoveNum == eMoveNum.SecondMove)
                {
                    makeSecondMoveCheckForMatch();
                    switchTurn();
                    m_MoveNum = eMoveNum.FirstMove;
                }
                else
                {
                    makeFirstMove();
                    m_MoveNum = eMoveNum.SecondMove;
                }
            }
        }
        */
        public void checkMatchLastMoveAndKeepGameRoutine()
        {
            if (m_FirstMoveSymbol != m_SecondMoveSymbol)
            {
                System.Threading.Thread.Sleep(2000);
                cancelLastPlayingPlayerPlay();
            }
            else
            {
                if (m_PlayerTurn == eTurn.PlayerOne)
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
                m_PlayerTwo.AiBrain.SetCardRevealedFromLastMove(m_FirstMovePoint, m_FirstMoveSymbol, m_SecondMovePoint, m_SecondMoveSymbol);
            }

            GameRoutineSwitchTurn();
        }

        private void GameRoutineSwitchTurn()
        {
            switchTurn();
            m_MoveNum = eMoveNum.FirstMove;
        }

        private void makeFirstMove()
        {
            Point firstMovePoint = askPlayingPlayerForMoveCheckMoveAndMakePoint(moveNum, null);

            m_FirstMoveSymbol = m_Ui.representTheBoardWithMove(firstMovePoint);
        }

        private Point askPlayingPlayerForMoveCheckMoveAndMakePoint()
        {
            Point movePoint;
            bool v_AlreadyExposed;

            do
            {
                movePoint = askPlayingPlayerForMoveAndMakePoint(i_MoveNum, i_SymbolOfFirstMoveCardRevealed);
                v_AlreadyExposed = CheckIfPointisExposed(movePoint);
            }
            while (v_AlreadyExposed == true);

            return movePoint;
        }
        /*
        private Point askPlayingPlayerForMoveAndMakePoint(eMoveNum io_MoveNum, int? i_SymbolOfFirstMoveCardRevealed)
        {
            Point movePoint;

            if (m_PlayerTurn == eTurn.PlayerTwo)
            {
                if (PlayerTwo.IsAi() == true)
                {
                    if (io_MoveNum == SystemManager.eMoveNum.FirstMove)
                    {
                        movePoint = PlayerTwo.AiBrain.MakingFirstMove();
                    }
                    else
                    {
                        movePoint = PlayerTwo.AiBrain.MakingSecondMove(i_SymbolOfFirstMoveCardRevealed.Value);
                    }

                    System.Threading.Thread.Sleep(2000);
                }
            }

            return movePoint;
        }
        */

        public void AITurn()
        {

            if (m_MoveNum == eMoveNum.FirstMove)
            {
                m_FirstMovePoint = PlayerTwo.AiBrain.MakingFirstMove();
                m_FirstMoveSymbol = m_GameBoard.Board[m_FirstMovePoint.Y, m_FirstMovePoint.X].SymbolOfIcon;
                m_MoveNum = eMoveNum.SecondMove;
            }
            else
            {
                m_SecondMovePoint = PlayerTwo.AiBrain.MakingSecondMove(m_FirstMoveSymbol);
                m_SecondMoveSymbol = m_GameBoard.Board[m_SecondMovePoint.Y, m_SecondMovePoint.X].SymbolOfIcon;
            }
        }

        public bool CheckIfPointisExposed(Point io_CurrMovePoint)
        {
            return m_GameBoard.CheckIfAlreadyExposed(io_CurrMovePoint);
        }

        private void cancelLastPlayingPlayerPlay()
        {
            m_GameBoard.HideIcon(m_FirstMovePoint);
            m_GameBoard.HideIcon(m_SecondMovePoint);
        }

        private void switchTurn()
        {
            if (m_PlayerTurn == eTurn.PlayerOne)
            {
                m_PlayerTurn = eTurn.PlayerTwo;
            }
            else
            {
                m_PlayerTurn = eTurn.PlayerOne;
            }
        }

        public bool checkGameOver()
        {
            m_GameBoard.checkIfGamehasFinished();
        }
    }
}
