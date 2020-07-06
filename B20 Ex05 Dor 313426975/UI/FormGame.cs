using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    public partial class FormGame : Form
    {
        private readonly SystemManager m_LogicManager;
        private Label playerTwo = new Label();
        private Label playerOne = new Label();
        private Button[,] buttonBoard;
        //private Button currentButtonToMove;
        private static readonly List<char> sr_IconSymbolStorage = makeRandSymbolListOfIconAccordingToSizeOfBoard();

        public FormGame(SystemManager i_System)
        {
            m_LogicManager = i_System;
            InitializeComponent();
            showBoardFromLogic();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void InitializeComponent()
        {
            Label playerTwo = new Label();
            Label playerOne = new Label();
            this.SuspendLayout();
            playerOne.AutoSize = true;
            playerOne.Text = m_LogicManager.PlayerOne.NameOfPlayer + ": " + m_LogicManager.PlayerOne.Score;
            playerOne.Top = 12;
            playerOne.Left = 12;
            playerOne.Name = "playerOne";
            playerTwo.AutoSize = true;
            playerTwo.Top = 12;
            playerTwo.Left = ClientSize.Width;
            playerTwo.Name = "playerTwo";
            playerTwo.Text = m_LogicManager.PlayerTwo.NameOfPlayer + ": " + m_LogicManager.PlayerTwo.Score;
            Controls.Add(this.playerOne);
            Controls.Add(this.playerTwo);
            ClientSize = new Size((m_LogicManager.Board.NumOfCols * m_LogicManager.Board.NumOfRows) + 100, (m_LogicManager.Board.NumOfCols * m_LogicManager.Board.NumOfRows) + 150);
            AutoSize = true;
            int xStartValue = playerOne.Left;
            int yStartValue = playerOne.Height + 12;
            int xCurrentValue;
            int yCurrentValue;

            for (int row = 0; row < m_LogicManager.Board.NumOfRows; row++)
            {
                for (int col = 0; col < m_LogicManager.Board.NumOfCols; col++)
                {
                    buttonBoard[row, col] = new Button();
                    buttonBoard[row, col].Size = new Size(80, 80);
                    xCurrentValue = xStartValue + (col * 80);
                    yCurrentValue = yStartValue + (row * 80);
                    buttonBoard[row, col].Location = new Point(xCurrentValue, yCurrentValue);
                    buttonBoard[row, col].BackColor = Color.DarkGray;
                    buttonBoard[row, col].Enabled = true;

                    this.Controls.Add(buttonBoard[row, col]);
                    buttonBoard[row, col].Click += new EventHandler(buttonBoard_Click);
                }
            }

            ResumeLayout(false);
            PerformLayout();
            //
            // FormGame
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1991, 1185);
            this.MaximizeBox = false;
            this.Name = "FormGame";
            this.Text = "Memory Game";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.ResumeLayout(false);

        }

        private void AiMoves()
        {
            m_LogicManager.AITurn();
            System.Threading.Thread.Sleep(2000);
            showBoardFromLogic();
            m_LogicManager.AITurn();
            System.Threading.Thread.Sleep(2000);
            showBoardFromLogic();
            m_LogicManager.checkMatchLastMoveAndKeepGameRoutine();
            KeepScore();
            CheckGameOver();
        }

        private void showBoardFromLogic()
        {
            for (int row = 0; row < m_LogicManager.GameBoard.NumOfRows; row++)
            {
                for (int col = 0; col < m_LogicManager.GameBoard.NumOfCols; col++)
                {
                    if (buttonBoard[row, col].Enabled == true)
                    {
                        buttonBoard[row, col].Text = sr_IconSymbolStorage[m_LogicManager.GameBoard.Board[row, col].SymbolOfIcon].ToString();
                    }
                }
            }
        }

        private void CheckGameOver()
        {
            if (m_LogicManager.checkGameOver() == true)
            {

            }
        }

        private void buttonBoard_Click(object sender, EventArgs e)
        {
            Button currentButtonToMove = sender as Button;

            if (m_LogicManager.CheckIfPointisExposed(currentButtonToMove.Location))
            {
                return;
            }

            //currentButtonToMove.Location // point
            m_LogicManager.CheckIfPointisExposed(movePoint);

            KeepScore();
            showBoardFromLogic();
        }

        private void KeepScore()
        {
            playerOne.Text = m_LogicManager.PlayerOne.NameOfPlayer + ": " + m_LogicManager.PlayerOne.Score;
            playerTwo.Text = m_LogicManager.PlayerTwo.NameOfPlayer + ": " + m_LogicManager.PlayerTwo.Score;
        }

        // this program chose randomaly symbol from ASCII table between '~' - '!' For the symbol for each card
        // and makes list of symbol for UI
        private static List<char> makeRandSymbolListOfIconAccordingToSizeOfBoard()
        {
            List<char> iconTempStorage = new List<char>();
            List<char> listOfAllPossibleIcon;
            int numOfCharMatchForIcon = '~' - '!';
            int numOfIconNeeded = 36;
            Random random = new Random();

            listOfAllPossibleIcon = makeListOfAllPossibleCharacters(numOfCharMatchForIcon);

            // plus one for space icon in symbol 0 (board return 0 if icon is hidden)
            iconTempStorage.Add(' ');

            for (int i = 0; i < numOfIconNeeded; i++)
            {
                // range in ASCII table with only symbol (more the max board size)
                int randomNumber = random.Next(0, listOfAllPossibleIcon.Count);
                iconTempStorage.Add(listOfAllPossibleIcon[randomNumber]);
                listOfAllPossibleIcon.RemoveAt(randomNumber);
            }

            return iconTempStorage;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Player2NameLabel_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanelCards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }
    }
}
