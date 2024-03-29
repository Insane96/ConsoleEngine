﻿using System;
using OpenTK.Input;

namespace ConsoleEngine
{
    public class Input
    {
        
        /// <summary>
        /// Removes all the stacked key inputs every frame, instead of reading them later. Mostly useful on low FPS
        /// </summary>
        public static bool IsKeyPressed(Key key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
