using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LilManGame.Screens;

namespace LilManGame.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private BackgroundScreen background;

        public MainMenuScreen(BackgroundScreen backgroundScreen) : base("Bird Hunt")
        {
            background = backgroundScreen;

            var playGameMenuEntry = new MenuEntry("Play Game");
            var exitMenuEntry = new MenuEntry("Exit");

            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            this.ExitScreen();
            background.ExitScreen();

        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
