﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ButtonActions
    {
        interact,
        jump,
        yell
    }
    class InputBinder
    {
        public Dictionary<ButtonActions, KeyCode> KeybindingMap = new Dictionary<ButtonActions, KeyCode>();

        public InputBinder()
        {
            KeybindingMap.Add(ButtonActions.interact, KeyCode.E);
            KeybindingMap.Add(ButtonActions.yell, KeyCode.F);
            KeybindingMap.Add(ButtonActions.jump, KeyCode.Space);
        }

    }
}
