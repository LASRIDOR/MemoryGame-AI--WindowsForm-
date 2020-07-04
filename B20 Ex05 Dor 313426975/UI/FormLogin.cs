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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void WindowsFormUI_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (SecondPlayerNameTextBox.Enabled == true)
            {
                SecondPlayerNameTextBox.Text = string.Empty;
            }
            else
            {
                SecondPlayerNameTextBox.Text = "-computer-";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void AgainstFriend_click(object sender, EventArgs e)
        {
            if (SecondPlayerNameTextBox.Enabled == false)
            {
                this.AgainstAFriendButtom.Text = "Against a Friend";
                this.SecondPlayerNameTextBox.Enabled = true;
            }
            else
            {
                this.AgainstAFriendButtom.Text = "Against a Computer";
                this.SecondPlayerNameTextBox.Enabled = false;
            }
        }

        private void BoardSizeButtom_Click(object sender, EventArgs e)
        {
            if (BoardSizeButtom.Text[2] == '6')
            {
                if (BoardSizeButtom.Text[0] == '6')
                {
                    BoardSizeButtom.Text = "4x4";
                }
                else
                {
                    BoardSizeButtom.Text = string.Format("{0}x{1}", BoardSizeButtom.Text[0]+1, BoardSizeButtom.Text[2]);
                }
            }
            else
            {
                BoardSizeButtom.Text = string.Format("{0}x{1}", BoardSizeButtom.Text[0], BoardSizeButtom.Text[2] + 1);
            }
        }

        private void StartButtom_Click(object sender, EventArgs e)
        {
            if (this.FirstPlayerNameTextBox == null)
            {
                MessageBox.Show("Please add a name for First Player");
            }
            else if (this.SecondPlayerNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please add a name for Second Player");
            }
            else
            {
                //set game
            }
        }

        public MemoryLogic.SystemManager SetGame()
        {
            MemoryLogic.SystemManager gameManager = new SystemManager();
            bool IsPlayerTwoAi = !(this.SecondPlayerNameLabel.Enabled);

            gameManager.PlayerOne = new Player(this.FirstPlayerNameTextBox.Text, false);

            if (IsPlayerTwoAi == true)
            {
                gameManager.PlayerTwo = new Player(null, true);
            }
            else
            {
                gameManager.PlayerTwo = new Player(this.SecondPlayerNameTextBox.Text, false);
            }

            gameManager.Board = new GameBoard(this.BoardSizeButtom.Text[0] - '0', this.BoardSizeButtom.Text[2] - '0');

            return gameManager;
        }
    }
}
