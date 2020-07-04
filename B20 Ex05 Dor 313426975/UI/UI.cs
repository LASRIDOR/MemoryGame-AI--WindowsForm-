using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    internal class UI
    {
        private MemoryLogic.SystemManager m_GameManager;

        private static readonly List<char> sr_IconSymbolStorage = makeRandSymbolListOfIconAccordingToSizeOfBoard();

        internal void Run()
        {
            FormLogin formLogin = new FormLogin();
            m_GameManager = formLogin.SetGame();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                FormGame formGame = new FormGame();
                formGame.StartPosition = FormStartPosition.CenterScreen;
                formGame.FormBorderStyle = FormBorderStyle.FixedDialog;
                formGame.MaximizeBox = false;
                formGame.ShowDialog();
            }
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
    }
}