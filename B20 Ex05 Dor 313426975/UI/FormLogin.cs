using System;
using System.Drawing;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    public partial class FormLogin : Form
    {
        private SystemManager m_ManagerPreperation;

        public SystemManager ManagerPreperation
        {
            get { return m_ManagerPreperation; }
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                return;
            }
            else
            {
                gamePreperationWDetails();
            }
        }

        private void AgainstFriend_click(object sender, EventArgs e)
        {
            if (SecondPlayerNameTextBox.Enabled == false)
            {
                this.AgainstAFriendButtom.Text = "Against a Computer";
                this.SecondPlayerNameTextBox.Enabled = true;
                SecondPlayerNameTextBox.Text = string.Empty;
            }
            else
            {
                this.AgainstAFriendButtom.Text = "Against a Friend";
                this.SecondPlayerNameTextBox.Enabled = false;
                SecondPlayerNameTextBox.Text = "-computer-";
            }
        }

        private void BoardSizeButtom_Click(object sender, EventArgs e)
        {
            char pRow = BoardSizeButtom.Text[0];
            char pCol = BoardSizeButtom.Text[2];

            if (pCol == '6')
            {
                if (pRow == '6')
                {
                    BoardSizeButtom.Text = "4x4";
                }
                else
                {
                    BoardSizeButtom.Text = string.Format("{0}x{1}", ++pRow, '4');
                }
            }
            else
            {
                if(pRow == '5' && pCol + 1 == '5')
                {
                    ++pCol;
                }

                BoardSizeButtom.Text = string.Format("{0}x{1}", BoardSizeButtom.Text[0], ++pCol);
            }
        }

        private void StartButtom_Click(object sender, EventArgs e)
        {
            gamePreperationWDetails();
        }

        private void gamePreperationWDetails()
        {
            if (this.FirstPlayerNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please add a name for First Player");
            }
            else if (this.SecondPlayerNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please add a name for Second Player");
            }
            else
            {
                m_ManagerPreperation = SetGame();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public SystemManager SetGame()
        {
            //bool v_IsPlayerTwoAi = !(this.SecondPlayerNameTextBox.Enabled == false);

            Player playerOne = new Player(this.FirstPlayerNameTextBox.Text, false, Color.Aquamarine);
            Player playerTwo;

            if (SecondPlayerNameTextBox.Text == "-computer-")
            {
                playerTwo = new Player(null, true, Color.DarkOrchid);
            }
            else
            {
                playerTwo = new Player(this.SecondPlayerNameTextBox.Text, false, Color.DarkOrchid);
            }

            return new SystemManager(playerOne, playerTwo, this.BoardSizeButtom.Text[0] - '0', this.BoardSizeButtom.Text[2] - '0');
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
