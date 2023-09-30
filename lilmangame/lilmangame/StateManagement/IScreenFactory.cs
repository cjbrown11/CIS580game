using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilManGame.StateManagement
{
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
