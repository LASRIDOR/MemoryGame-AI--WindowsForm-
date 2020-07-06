using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    internal class UI
    {
        internal void Run()
        {
            FormLogin formLogin = new FormLogin();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                FormGame formGame = new FormGame(formLogin.SetGame());
                formGame.StartPosition = FormStartPosition.CenterScreen;
                formGame.FormBorderStyle = FormBorderStyle.FixedDialog;
                formGame.MaximizeBox = false;
                formGame.ShowDialog();
            }
        }
    }
}