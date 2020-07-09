using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    public partial class FormMatchGame : Form
    {
        private static readonly List<char> sr_IconSymbolStorage = makeRandSymbolListOfIconAccordingToSizeOfBoard();
        private readonly int r_NumOfRows;
        private readonly int r_NumOfCols;
        private readonly Dictionary<int, Point> sr_LocationToPoint;
        private Button[,] buttonBoard;
        private Point m_LastMove;
        private bool v_WaitForPushButtom;

        public bool WaitForPushButtom
        {
            get { return v_WaitForPushButtom; }
            set { v_WaitForPushButtom = value; }
        }

        public Point LastMove
        {
            get { return m_LastMove; }
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

        public FormMatchGame(string i_NameOfPlayerOne, string i_NameOfPlayerTwo, int i_Row, int i_Col)
        {
            r_NumOfRows = i_Row;
            r_NumOfCols = i_Col;
            sr_LocationToPoint = new Dictionary<int, Point>(i_Row * i_Col);
            InitializeComponent(i_NameOfPlayerOne, i_NameOfPlayerTwo, i_Row, i_Col);
        }
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(string i_NameOfPlayerOne, string i_NameOfPlayerTwo, int i_Row, int i_Col)
        {
            this.LabelCurrentPlayer = new System.Windows.Forms.Label();
            this.LabelFirstName = new System.Windows.Forms.Label();
            this.LabelSecondPlayer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            string zeroPairs = ": 0 Pairs";
            ////
            //// LabelCurrentPlayer
            ////
            this.LabelCurrentPlayer.AutoSize = true;
            this.LabelCurrentPlayer.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.LabelCurrentPlayer.Location = new System.Drawing.Point(10, i_Row * 165 + 12);
            this.LabelCurrentPlayer.Name = "LabelCurrentPlayer";
            this.LabelCurrentPlayer.Size = new System.Drawing.Size(100, 37);
            this.LabelCurrentPlayer.TabIndex = 0;
            LabelCurrentPlayer.Text = string.Format("Current Player: {0}", i_NameOfPlayerOne);
            this.LabelCurrentPlayer.Click += new System.EventHandler(this.label1_Click);
            ////
            //// LabelFirstName
            ////
            this.LabelFirstName.AutoSize = true;
            this.LabelFirstName.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.LabelFirstName.Location = new System.Drawing.Point(10, LabelCurrentPlayer.Location.Y + 60);
            this.LabelFirstName.Name = "LabelFirstName";
            this.LabelFirstName.Size = new System.Drawing.Size(102, 37);
            this.LabelFirstName.TabIndex = 1;
            LabelFirstName.Text = string.Format("{0} {1}", i_NameOfPlayerOne, zeroPairs);
            this.LabelFirstName.Click += new System.EventHandler(this.LaberFirstName_Click);
            ////
            //// LabelSecondPlayer
            ////
            this.LabelSecondPlayer.AutoSize = true;
            this.LabelSecondPlayer.BackColor = System.Drawing.Color.Orchid;
            this.LabelSecondPlayer.ForeColor = System.Drawing.Color.Black;
            this.LabelSecondPlayer.Location = new System.Drawing.Point(10, LabelFirstName.Location.Y + 60);
            this.LabelSecondPlayer.Name = "LabelSecondPlayer";
            this.LabelSecondPlayer.Size = new System.Drawing.Size(100, 37);
            this.LabelSecondPlayer.TabIndex = 2;
            LabelSecondPlayer.Text = string.Format("{0} {1}", i_NameOfPlayerTwo, zeroPairs);
            ////
            //// Table of Board Of Buttom
            ////
            buttonBoard = new Button[i_Row, i_Col];
            int xStartValue = LabelCurrentPlayer.Left;
            int yStartValue = LabelCurrentPlayer.Height - 12;
            int xCurrentValue;
            int yCurrentValue;
            int tabIndex = 0;

            for (int row = 0; row < r_NumOfRows; row++)
            {
                for (int col = 0; col < r_NumOfCols; col++)
                {
                    buttonBoard[row, col] = new Button();
                    buttonBoard[row, col].Size = new Size(150, 150);
                    xCurrentValue = xStartValue + (col * 160);
                    yCurrentValue = yStartValue + (row * 160);
                    buttonBoard[row, col].Location = new Point(xCurrentValue, yCurrentValue);
                    buttonBoard[row, col].BackColor = Color.DarkGray;
                    buttonBoard[row, col].Enabled = true;
                    this.Controls.Add(buttonBoard[row, col]);
                    buttonBoard[row, col].Click += new EventHandler(buttonBoard_Click);
                    buttonBoard[row, col].TabIndex = tabIndex++;
                    sr_LocationToPoint.Add(buttonBoard[row, col].TabIndex, new Point(col, row));
                }
            }
            ////
            //// FormMatchGame
            ////
            Button cancelButtom = new Button();
            cancelButtom.Name = "CancelButtom";
            cancelButtom.Click += new EventHandler(FormMatchGame_DieHard);
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size((i_Col) * 162, (i_Row + 2) * 167);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Controls.Add(this.LabelSecondPlayer);
            this.Controls.Add(this.LabelFirstName);
            this.Controls.Add(this.LabelCurrentPlayer);
            this.Name = "FormMatchGame";
            this.Text = "Memory Game";
            this.Load += new System.EventHandler(this.FormMatchGame_Load);
            this.Shown += new System.EventHandler(this.FormMatchGame_Shown);
            this.CancelButton = cancelButtom;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public void showBoardFromLogic(GameBoard.Cube[,] i_Cube, Player io_PlayerOne, Player io_PlayerTwo) // think about give only point and change accordingly except than giving and changing all of it
        {
            for (int row = 0; row < r_NumOfRows; row++)
            {
                for (int col = 0; col < r_NumOfCols; col++)
                {
                    if (buttonBoard[row, col].Enabled == true)
                    {
                        buttonBoard[row, col].Text = sr_IconSymbolStorage[i_Cube[row, col].SymbolOfIcon].ToString();
                        buttonBoard[row, col].BackColor = i_Cube[row, col].Color;
                    }
                }
            }

            this.LabelFirstName.Text = string.Format("{0}: {1} Pairs", io_PlayerOne.NameOfPlayer, io_PlayerOne.Score);
            this.LabelSecondPlayer.Text = string.Format("{0}: {1} Pairs", io_PlayerTwo.NameOfPlayer, io_PlayerTwo.Score);
        }

        private void buttonBoard_Click(object sender, EventArgs e)
        {
            Button currentButtonToMove = sender as Button;
            m_LastMove = sr_LocationToPoint[currentButtonToMove.TabIndex];
            this.DialogResult = DialogResult.Cancel;
        }

        public void WinnerAnnouncment(Player i_PlayerOne, Player i_PlayerTwo)
        {
            if (i_PlayerOne.Score > i_PlayerTwo.Score)
            {
                MessageBox.Show(string.Format("{0} Congratulations You Are The Winner", i_PlayerOne.NameOfPlayer));
            }
            else if (i_PlayerOne.Score < i_PlayerTwo.Score)
            {
                MessageBox.Show(string.Format("{0} Congratulations You Are The Winner", i_PlayerTwo.NameOfPlayer));
            }
            else
            {
                MessageBox.Show("Its A Tie Congratulations Both Of You");
            }
        }

        public void AlreadyExposedMessage()
        {
            MessageBox.Show("Card Is Already Exposed And Cannot Be Selected Again");
        }

        public bool AskForAnotherGame()
        {
            DialogResult dialogResult = MessageBox.Show("Game Ended, Do you want to play again?", "Matching", MessageBoxButtons.YesNo);
            bool v_AnotherGame;

            if (dialogResult == DialogResult.Yes)
            {
                Close();
                v_AnotherGame = true;
            }
            else
            {
                Close();
                v_AnotherGame = false;
            }

            return v_AnotherGame;
        }

        public void switchCurrentPlayerTo(string i_NameOfPlayer)
        {
            this.LabelCurrentPlayer.Text = string.Format("Current Player: {0}", i_NameOfPlayer);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void FormMatchGame_DieHard(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void FormMatchGame_Load(object sender, EventArgs e)
        {
        }

        private void LaberFirstName_Click(object sender, EventArgs e)
        {
        }

        private void FormMatchGame_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FormMatchGame_Shown(object sender, EventArgs e)
        {
            if (v_WaitForPushButtom == false)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show("Card Was Flipped Watch The Result Press OK After Evaluation");
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
