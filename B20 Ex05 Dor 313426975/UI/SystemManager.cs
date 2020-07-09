using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MemoryLogic;

namespace UI
{
    public class SystemManager
    {
        private FormMatchGame m_FormGame;
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

        public SystemManager(Player i_PlayerOne, Player i_PlayerTwo, int i_Row, int i_Col)
        {
            m_PlayerOne = i_PlayerOne;
            m_PlayerTwo = i_PlayerTwo;
            m_GameBoard = new GameBoard(i_Row, i_Col);
        }

        public void PlayMatchGame()
        {
            bool v_WantToPlayAnotherGame = true;

            do
            {
                MakeNewGame();
                m_PlayerOne.ResetGame(null, null);
                m_PlayerTwo.ResetGame(m_GameBoard.NumOfRows, m_GameBoard.NumOfCols);
                m_PlayerTurn = eTurn.PlayerOne;
                gameRoutineAndKeepScore();
                m_FormGame.WinnerAnnouncment(m_PlayerOne, m_PlayerTwo);
                v_WantToPlayAnotherGame = m_FormGame.AskForAnotherGame();
                m_FormGame.Close();
            }
            while (v_WantToPlayAnotherGame == true);
        }

        private void MakeNewGame()
        {
            m_GameBoard.HideAllCardAndMixCardAgain();
            m_FormGame = new FormMatchGame(m_PlayerOne.NameOfPlayer, m_PlayerTwo.NameOfPlayer, m_GameBoard.NumOfRows, m_GameBoard.NumOfCols);
            m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
        }

        private void gameRoutineAndKeepScore()
        {
            while (m_GameBoard.checkIfGamehasFinished() == false)
            {
                playerMakeMoveHisTurn();
                switchTurn();
            }
        }

        private void playerMakeMoveHisTurn()
        {
            m_MoveNum = eMoveNum.FirstMove;
            m_FirstMovePoint = askPlayingPlayerForMoveCheckMoveAndMakeCoordinate();
            m_FirstMoveSymbol = m_GameBoard.ExposeSymbolAndTakeValue(m_FirstMovePoint);

            m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
            m_FormGame.WaitForPushButtom = false;
            m_FormGame.ShowDialog();
            m_FormGame.WaitForPushButtom = true;

            m_MoveNum = eMoveNum.SecondMove;
            m_SecondMovePoint = askPlayingPlayerForMoveCheckMoveAndMakeCoordinate();
            m_SecondMoveSymbol = m_GameBoard.ExposeSymbolAndTakeValue(m_SecondMovePoint);

            m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
            m_FormGame.WaitForPushButtom = false;
            m_FormGame.ShowDialog();

            if (m_SecondMoveSymbol != m_FirstMoveSymbol)
            {
                cancelLastPlayingPlayerPlay();
            }
            else
            {
                if (m_PlayerTurn == eTurn.PlayerOne)
                {
                    m_PlayerOne.GivePlayerOnePoint();
                    m_GameBoard.PaintCubeInColor(m_FirstMovePoint, m_SecondMovePoint, PlayerOne.Color);
                }
                else
                {
                    m_PlayerTwo.GivePlayerOnePoint();
                    m_GameBoard.PaintCubeInColor(m_FirstMovePoint, m_SecondMovePoint, m_PlayerTwo.Color);
                }

                m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
            }

            if (m_PlayerTwo.IsAi() == true)
            {
                m_PlayerTwo.AiBrain.SetCardRevealedFromLastMove(m_FirstMovePoint, m_FirstMoveSymbol, m_SecondMovePoint, m_SecondMoveSymbol);
            }
        }

        private Point askPlayingPlayerForMoveCheckMoveAndMakeCoordinate()
        {
            Point moveCoordinate;
            bool v_AlreadyExposed;

            do
            {
                moveCoordinate = askPlayingPlayerForMoveAndMakeCoordinate();
                v_AlreadyExposed = CheckIfPointisExposed(moveCoordinate);

                if (v_AlreadyExposed == true)
                {
                    m_FormGame.AlreadyExposedMessage();
                }
            }
            while (v_AlreadyExposed == true);

            return moveCoordinate;
        }

        private Point askPlayingPlayerForMoveAndMakeCoordinate()
        {
            Point moveCoordinate;

            if (m_PlayerTurn == eTurn.PlayerOne)
            {
                m_FormGame.WaitForPushButtom = true;
                moveCoordinate = getMoveFromHumanPlayerAndMakeCoordinate();
            }
            else
            {
                if (m_PlayerTwo.IsAi() == true)
                {
                    m_FormGame.WaitForPushButtom = false;
                    if (m_MoveNum == eMoveNum.FirstMove)
                    {
                        moveCoordinate = m_PlayerTwo.AiBrain.MakingFirstMove();
                    }
                    else
                    {
                        moveCoordinate = m_PlayerTwo.AiBrain.MakingSecondMove(m_FirstMoveSymbol);
                    }
                }
                else
                {
                    m_FormGame.WaitForPushButtom = true;
                    moveCoordinate = getMoveFromHumanPlayerAndMakeCoordinate();
                }

                m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
            }

            return moveCoordinate;
        }

        private Point getMoveFromHumanPlayerAndMakeCoordinate()
        {
            m_FormGame.ShowDialog();
            return m_FormGame.LastMove;
        }

        public bool CheckIfPointisExposed(Point io_CurrMovePoint)
        {
            return m_GameBoard.CheckIfAlreadyExposed(io_CurrMovePoint);
        }

        private void cancelLastPlayingPlayerPlay()
        {
            m_GameBoard.HideIcon(m_FirstMovePoint);
            m_GameBoard.HideIcon(m_SecondMovePoint);
            m_FormGame.showBoardFromLogic(m_GameBoard.Board, PlayerOne, PlayerTwo);
        }

        private void switchTurn()
        {
            if (m_PlayerTurn == eTurn.PlayerOne)
            {
                m_PlayerTurn = eTurn.PlayerTwo;
                m_FormGame.switchCurrentPlayerTo(m_PlayerTwo.NameOfPlayer);
            }
            else
            {
                m_PlayerTurn = eTurn.PlayerOne;
                m_FormGame.switchCurrentPlayerTo(m_PlayerOne.NameOfPlayer);
            }
        }
    }
}
