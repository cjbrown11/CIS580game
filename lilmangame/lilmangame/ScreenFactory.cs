using LilManGame.StateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilManGame
{
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
