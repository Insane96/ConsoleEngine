using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("[Warning] Renderer has already been Initialized");

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
        public static void Put(string text, Vector2 pos, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Put(text[i], Utils.GetMatrixPosition(Utils.GetArrayPosition(pos, WINDOW_WIDTH) + i, WINDOW_WIDTH), color, backgroundColor);
            }
        }

        /// <summary>
        /// Puts the character into the window buffer at the selected position
        /// </summary>
        public static void Put(char character, Vector2 pos, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            if (pos.x < 0 || pos.x >= WINDOW_WIDTH || pos.y < 0 || pos.y >= WINDOW_HEIGHT)
                return;

            int a = Utils.GetArrayPosition(pos, WINDOW_WIDTH);
            windowBuffer[a].Set(character, color, backgroundColor);
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
            char c;
            ConsoleColor textColor;
            ConsoleColor backgroundColor;

            public Pixel()
            {
                this.c = ' ';
                this.textColor = ConsoleColor.White;
                this.backgroundColor = ConsoleColor.Black;
            }
            public Pixel(char c, ConsoleColor textColor, ConsoleColor backgroundColor)
            {
                this.c = c;
                this.textColor = textColor;
                this.backgroundColor = backgroundColor;
            }

            /// <summary>
            /// Sets the pixel to the passed arguments, passing no arguments will reset the Pixel to nothing, black background and white character
            /// </summary>
            /// <param name="c"></param>
            /// <param name="textColor"></param>
            /// <param name="backgroundColor"></param>
            public void Set(char c = ' ', ConsoleColor textColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
            {
                this.c = c;
                this.textColor = textColor;
                this.backgroundColor = backgroundColor;
            }

            public char GetCharacter()
            {
                return this.c;
            }
            public ConsoleColor GetColor()
            {
                return this.textColor;
            }
            public ConsoleColor GetBackgroundColor()
            {
                return this.backgroundColor;
            }

            public override string ToString()
            {
                return $"Pixel[char: {c}, textColor: {textColor}, backgrounColor: {backgroundColor}]";
            }

            public override bool Equals(object obj)
            {
                if (obj is Pixel)
                {
                    Pixel p = (Pixel)obj;
                    return
                        this.c.Equals(p.c) &&
                        this.textColor.Equals(p.textColor) &&
                        this.backgroundColor.Equals(p.backgroundColor);
                }
                return false;
            }
        }
    }
}
