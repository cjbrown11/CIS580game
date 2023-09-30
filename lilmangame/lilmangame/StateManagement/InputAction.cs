using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LilManGame.StateManagement
{
    public class InputAction
    {
        private readonly Buttons[] _buttons;
        private readonly Keys[] _keys;
        private readonly bool _firstPressOnly;

        private delegate bool ButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex player);
        private delegate bool KeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex player);

        public InputAction(Buttons[] triggerButtons, Keys[] triggerKeys, bool firstPressOnly)
        {
            // Store the buttons and keys. If the arrays are null, we create a 0 length array so we don't
            // have to do null checks in the Occurred method
            _buttons = triggerButtons != null ? triggerButtons.Clone() as Buttons[] : new Buttons[0];
            _keys = triggerKeys != null ? triggerKeys.Clone() as Keys[] : new Keys[0];
            _firstPressOnly = firstPressOnly;
        }

        public bool Occurred(InputState stateToTest, PlayerIndex? playerToTest, out PlayerIndex player)
        {
            // Figure out which delegate methods to map from the state which takes care of our "firstPressOnly" logic
            ButtonPress buttonTest;
            KeyPress keyTest;

            if (_firstPressOnly)
            {
                buttonTest = stateToTest.IsNewButtonPress;
                keyTest = stateToTest.IsNewKeyPress;
            }
            else
            {
                buttonTest = stateToTest.IsButtonPressed;
                keyTest = stateToTest.IsKeyPressed;
            }

            // Now we simply need to invoke the appropriate methods for each button and key in our collections
            foreach (var button in _buttons)
            {
                if (buttonTest(button, playerToTest, out player))
                    return true;
            }
            foreach (var key in _keys)
            {
                if (keyTest(key, playerToTest, out player))
                    return true;
            }

            // If we got here, the action is not matched
            player = PlayerIndex.One;
            return false;
        }
    }
}
