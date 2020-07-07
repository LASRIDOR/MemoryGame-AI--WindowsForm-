using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MemoryLogic;

namespace UI
{
    public class UI
    {
        public static SystemManager LoginAndSetGame()
        {
            SystemManager resultManager = null;
            bool v_dialogResult;

            do
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
                v_dialogResult = formLogin.DialogResult == DialogResult.OK;

                if (v_dialogResult == true)
                {
                    resultManager = formLogin.ManagerPreperation;
                }

            } while (v_dialogResult == false);

            return resultManager;
        }
    }
}