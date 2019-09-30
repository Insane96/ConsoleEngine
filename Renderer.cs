using System;
using System.Diagnostics;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleEngine
{
    /// <summary>
    /// Renderer than prints 
    /// </summary>
    public class Renderer
    {
        private static Pixel[] windowBuffer;
        private static Pixel[] drawnBuffer;
        private static int WINDOW_WIDTH;
        private static int WINDOW_HEIGHT;

        private static bool hasAlreadyBeenInit = false;

        /// <summary>
        /// Initializes the Renderer by creating the Window Buffer, setting the window Width, Height and Title and Show/Hide the Cursor.
        /// </summary>
        public static void Init(int width, int height, string title)
        {
            if (hasAlreadyBeenInit)
                throw new Exception("Renderer has already been Initialized");

            WINDOW_WIDTH = width;
            WINDOW_HEIGHT = height;
            windowBuffer = new Pixel[WINDOW_WIDTH * WINDOW_HEIGHT];
            drawnBuffer = new Pixel[WINDOW_WIDTH * WINDOW_HEIGHT];

            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.CursorVisible = false;

            for (int i = 0; i < windowBuffer.Length; i++)
            {
                windowBuffer[i] = new Pixel();
                drawnBuffer[i] = new Pixel();
            }

            hasAlreadyBeenInit = true;
        }

        /// <summary>
        /// Puts the string into the window buffer by cycling every character at the selected position
        /// </summary>
        public static void Put(string text, Vector2 pos, Color color, Color backgroundColor)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Put(text[i], Utils.GetMatrixPosition(Utils.GetArrayPosition(pos, WINDOW_WIDTH) + i, WINDOW_WIDTH), color, backgroundColor);
            }
        }
        public static void Put(string text, Vector2 pos, Color color, ConsoleColor backgroundColor)
        {
            Put(text, pos, color, Color.Black);
        }
        public static void Put(string text, Vector2 pos)
        {
            Put(text, pos, Color.White, Color.Black);
        }

        /// <summary>
        /// Puts the character into the window buffer at the selected position
        /// </summary>
        public static void Put(char character, Vector2 pos, Color color, Color backgroundColor)
        {
            if (pos.x < 0 || pos.x >= WINDOW_WIDTH || pos.y < 0 || pos.y >= WINDOW_HEIGHT)
                return;

            int a = Utils.GetArrayPosition(pos, WINDOW_WIDTH);
            windowBuffer[a].Set(character, color, backgroundColor);
        }
        public static void Put(char character, Vector2 pos, Color color)
        {
            Put(character, pos, color, Color.Black);
        }
        public static void Put(char character, Vector2 pos)
        {
            Put(character, pos, Color.White, Color.Black);
        }

        /// <summary>
        /// Called by the Engine draws the window buffer
        /// </summary>
        public static void Draw()
        {
            for (int i = 0; i < windowBuffer.Length; i++)
            {
                if (windowBuffer[i].GetCharacter().Equals(' ') && windowBuffer[i].Equals(drawnBuffer[i]))
                    continue;
                int x = Utils.GetMatrixPosition(i, WINDOW_WIDTH).GetXInt();
                int y = Utils.GetMatrixPosition(i, WINDOW_WIDTH).GetYInt();
                Console.SetCursorPosition(x, y);
                if (!windowBuffer[i].GetColor().Equals(drawnBuffer[i].GetColor()) || !windowBuffer[i].GetColor().Equals(Console.ForegroundColor))
                    Console.ForegroundColor = windowBuffer[i].GetColor();
                if (!windowBuffer[i].GetBackgroundColor().Equals(drawnBuffer[i].GetBackgroundColor()) || !windowBuffer[i].GetBackgroundColor().Equals(Console.BackgroundColor))
                    Console.BackgroundColor = windowBuffer[i].GetBackgroundColor();
                Console.Write(windowBuffer[i].GetCharacter());

                drawnBuffer[i].Set(windowBuffer[i].GetCharacter(), windowBuffer[i].GetColor(), windowBuffer[i].GetBackgroundColor());

                windowBuffer[i].Set();
            }
        }

        /// <summary>
        /// Returns the Window Width
        /// </summary>
        public static int GetWindowWidth()
        {
            return WINDOW_WIDTH;
        }

        /// <summary>
        /// Returns the Window Height
        /// </summary>
        public static int GetWindowHeight()
        {
            return WINDOW_HEIGHT;
        }

        /// <summary>
        /// Returns the Window Width and Height as Vector2
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetWindowSize()
        {
            return new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        private class Pixel
        {
            char character;
            Color textColor;
            Color backgroundColor;

            public Pixel()
            {
                this.character = ' ';
                this.textColor = Color.White;
                this.backgroundColor = Color.Black;
            }
            public Pixel(char character, Color textColor, Color backgroundColor)
            {
                this.character = character;
                this.textColor = textColor;
                this.backgroundColor = backgroundColor;
            }

            /// <summary>
            /// Sets the pixel to the passed arguments, passing no arguments will reset the Pixel to nothing, black background and white character
            /// </summary>
            public void Set(char character, Color textColor, Color backgroundColor)
            {
                this.character = character;
                this.textColor = textColor;
                this.backgroundColor = backgroundColor;
            }
            public void Set(char character, Color textColor)
            {
                Set(character, textColor, Color.Black);
            }
            public void Set(char character = ' ')
            {
                Set(character, Color.White, Color.Black);
            }

            public char GetCharacter()
            {
                return this.character;
            }
            public Color GetColor()
            {
                return this.textColor;
            }
            public Color GetBackgroundColor()
            {
                return this.backgroundColor;
            }

            public override string ToString()
            {
                return $"Pixel[char: {character}, textColor: {textColor}, bColor: {backgroundColor}]";
            }

            public override bool Equals(object obj)
            {
                if (obj is Pixel p)
                {
                    return
                        this.character.Equals(p.character) &&
                        this.textColor.Equals(p.textColor) &&
                        this.backgroundColor.Equals(p.backgroundColor);
                }
                return false;
            }
        }
    }
}
