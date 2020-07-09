using System;
using System.Collections.Generic;
using System.Drawing;

namespace MemoryLogic
{
    public class Player
    {
        private readonly string r_NameOfPlayer;
        private readonly Ai r_AiBrain;
        private int m_Score;
        private readonly Color r_Color;

        public Ai AiBrain
        {
            get { return r_AiBrain; }
        }

        public Player(string i_NameOfPlayer, bool isAi, Color i_Color)
        {
            if (isAi == true)
            {
                r_NameOfPlayer = "-computer-";
                r_AiBrain = new Ai();
            }
            else
            {
                r_NameOfPlayer = i_NameOfPlayer;
                r_AiBrain = null;
            }

            r_Color = i_Color;
        }

        public Color Color
        {
            get { return r_Color; }
        }

        public string NameOfPlayer
        {
            get { return r_NameOfPlayer; }
        }

        public int Score
        {
            get { return m_Score; }
        }

        public bool IsAi()
        {
            if (r_AiBrain == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public class Ai
        {
            // Ai Explanation:
            // my Ai is based on game theory and has specific strategy for each move
            // XMove - says that i will play with x UnKnown Card in this move and (2-X) moves with known card
            private enum ePlayingStrategy
            {
                ZeroMove = 0,
                OneMove = 1,
                TwoMove = 2,
                ThreeMove = 3
            }

            private Dictionary<int, Point> m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch;
            private int m_PairsOnTable;
            private int m_NumOfRows;
            private int m_NumOfCols;
            private ePlayingStrategy m_MyNextKindMove;
            private List<Point> m_ListPointForSureWinsMove;
            private List<Point> m_ListPointExposedCard;

            public Point MakingFirstMove()
            {
                Point firstMove;

                if (m_MyNextKindMove == ePlayingStrategy.TwoMove || m_MyNextKindMove == ePlayingStrategy.OneMove)
                {
                    firstMove = getCardForCurrMoveOnKnowingDemand(false);
                }
                else if (m_MyNextKindMove == ePlayingStrategy.ZeroMove)
                {
                    firstMove = getCardForCurrMoveOnKnowingDemand(true);
                }
                else
                {
                    firstMove = m_ListPointForSureWinsMove[0];
                    m_ListPointForSureWinsMove.RemoveAt(0);
                }

                return firstMove;
            }

            public Point MakingSecondMove(int i_SymbolOfFirstMoveCardRevealed)
            {
                Point secondMove;

                if (m_MyNextKindMove == ePlayingStrategy.TwoMove || m_MyNextKindMove == ePlayingStrategy.OneMove)
                {
                    if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsKey(i_SymbolOfFirstMoveCardRevealed) == true)
                    {
                        secondMove = m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch[i_SymbolOfFirstMoveCardRevealed];
                        m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Remove(i_SymbolOfFirstMoveCardRevealed);
                    }
                    else
                    {
                        if (m_MyNextKindMove == ePlayingStrategy.TwoMove)
                        {
                            secondMove = getCardForCurrMoveOnKnowingDemand(false);
                        }
                        else
                        {
                            secondMove = getCardForCurrMoveOnKnowingDemand(true);
                        }
                    }
                }
                else if(m_MyNextKindMove == ePlayingStrategy.ZeroMove)
                {
                    secondMove = getCardForCurrMoveOnKnowingDemand(true);
                }
                else
                {
                    secondMove = m_ListPointForSureWinsMove[0];
                    m_ListPointForSureWinsMove.RemoveAt(0);
                }

                return secondMove;
            }

            // make a lottery between cards on knowing demand example: if i_NeedKnownCard == true then i want a known card
            // and make i randomaly chose them from the dictionary of knowing card
            // and always make sure that they havent exposed yet
            // (No need for checking because ui will ask me again in case of exposing card but just to make sure)
            private Point getCardForCurrMoveOnKnowingDemand(bool i_NeedKnownCard)
            {
                Point move;
                bool v_FoundMove = false;
                Random rand = new Random();

                do
                {
                    int randRowPoint = rand.Next(0, m_NumOfRows);
                    int randColPoint = rand.Next(0, m_NumOfCols);

                    move = new Point(randColPoint, randRowPoint);

                    if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsValue(move) == i_NeedKnownCard)
                    {
                        if (m_ListPointExposedCard.Contains(move) == false)
                        {
                            v_FoundMove = true;
                        }
                    }
                }
                while (v_FoundMove == false);

                return move;
            }

            // get last play from ui and in case of match play add And Remove If Exist from Sure win list And Dictionary
            // in case of match from dictionary of knowing card (differend cards of Point) this program will add them to sure win list
            // if this is a new reveal of card i will add this to dictionary of knowing card and calculate next move (Math of game theory)
            public void SetCardRevealedFromLastMove(Point i_FirstMovePoint, int i_FirstMoveSymbol, Point i_SecondMovePoint, int i_SecondMoveSymbol)
            {
                if (i_FirstMoveSymbol == i_SecondMoveSymbol)
                {
                    addAndRemoveIfExistMatchCard(i_FirstMovePoint, i_SecondMovePoint, i_FirstMoveSymbol);
                    calculateNextMove();
                }
                else
                {
                    if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsKey(i_FirstMoveSymbol) == true)
                    {
                        if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsValue(i_FirstMovePoint) == false)
                        {
                            addToSureWinListKnownCard(i_FirstMoveSymbol, i_FirstMovePoint);
                        }
                    }
                    else
                    {
                        m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Add(i_FirstMoveSymbol, i_FirstMovePoint);
                        calculateNextMove();
                    }

                    if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsKey(i_SecondMoveSymbol) == true)
                    {
                        if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.ContainsValue(i_SecondMovePoint) == false)
                        {
                            addToSureWinListKnownCard(i_SecondMoveSymbol, i_SecondMovePoint);
                        }
                    }
                    else
                    {
                        m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Add(i_SecondMoveSymbol, i_SecondMovePoint);
                        calculateNextMove();
                    }
                }
            }

            private void addAndRemoveIfExistMatchCard(Point i_FirstMovePoint, Point i_SecondMovePoint, int i_SymbolOfMoves)
            {
                m_ListPointExposedCard.Add(i_FirstMovePoint);
                m_ListPointExposedCard.Add(i_SecondMovePoint);
                m_ListPointForSureWinsMove.Remove(i_FirstMovePoint);
                m_ListPointForSureWinsMove.Remove(i_SecondMovePoint);
                m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Remove(i_SymbolOfMoves);
            }

            private void addToSureWinListKnownCard(int i_MoveSymbol, Point i_MovePoint)
            {
                m_ListPointForSureWinsMove.Add(m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch[i_MoveSymbol]);
                m_ListPointForSureWinsMove.Add(i_MovePoint);
                m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Remove(i_MoveSymbol);
                m_MyNextKindMove = ePlayingStrategy.ThreeMove;
            }

            // Math of game theory for next move for matching game
            private void calculateNextMove()
            {
                if (m_ListPointForSureWinsMove.Count > 0)
                {
                    m_MyNextKindMove = ePlayingStrategy.ThreeMove;
                }
                else
                {
                    if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count == 0 ||
                         (m_PairsOnTable + m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count) % 2 == 1)
                    {
                        m_MyNextKindMove = ePlayingStrategy.TwoMove;
                    }
                    else if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count >= 1 &&
                         (m_PairsOnTable + m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count) % 2 == 0)
                    {
                        m_MyNextKindMove = ePlayingStrategy.OneMove;
                    }
                    else if (m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count >=
                             2 * (m_PairsOnTable + 1) / 3 &&
                             (m_PairsOnTable + m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count) % 2 == 1)
                    {
                        m_MyNextKindMove = ePlayingStrategy.ZeroMove;
                    }

                    if (m_PairsOnTable == 6 && m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch.Count == 1)
                    {
                        m_MyNextKindMove = ePlayingStrategy.OneMove;
                    }
                }
            }

            public void ResetMermory(int i_NumOfRow, int i_NumOfCol)
            {
                m_DictionaryMemoryOfUnrevealedKnownCardWithoutMatch = new Dictionary<int, Point>(i_NumOfRow * i_NumOfCol);
                m_ListPointForSureWinsMove = new List<Point>(i_NumOfRow * i_NumOfCol);
                m_ListPointExposedCard = new List<Point>(i_NumOfRow * i_NumOfCol);
                m_PairsOnTable = i_NumOfRow * i_NumOfCol;
                m_MyNextKindMove = ePlayingStrategy.TwoMove;
                m_NumOfRows = i_NumOfRow;
                m_NumOfCols = i_NumOfCol;
            }
        }

        public void ResetGame(int? i_NumOfRow, int? i_NumOfCol)
        {
            if (r_AiBrain != null)
            {
                AiBrain.ResetMermory(i_NumOfRow.Value, i_NumOfCol.Value);
            }

            m_Score = 0;
        }

        public void GivePlayerOnePoint()
        {
            m_Score++;
        }
    }
}
