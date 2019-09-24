using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Time
    {
        static Stopwatch sw = new Stopwatch();

        private static float deltaTime;
        /// <summary>
        /// Time passed since last frame
        /// </summary>
        public static float DeltaTime { get { return deltaTime; } }

        public static void Update()
        {
            sw.Stop();
            deltaTime = sw.ElapsedMilliseconds / 1000f;
            sw.Reset();

            sw.Start();
        }
    }
}
