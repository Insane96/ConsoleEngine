﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    public class Vector2
    {
        public float x;
        public float y;

        public static Vector2 Zero
        {
            get
            {
                return new Vector2();
            }
        }

        public static Vector2 One
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2(Vector2 pos)
        {
            this.x = pos.x;
            this.y = pos.y;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetXInt()
        {
            return (int)this.x;
        }
        public int GetYInt()
        {
            return (int)this.y;
        }
        public Vector2 GetInt()
        {
            return new Vector2((int)this.x, (int)this.y);
        }

        public Vector2 Add(float x, float y)
        {
            return new Vector2(this.x + x, this.y + y);
        }

        public Vector2 Left()
        {
            return new Vector2(x - 1, y);
        }
        public Vector2 Right()
        {
            return new Vector2(x + 1, y);
        }

        public Vector2 Up()
        {
            return new Vector2(x, y - 1);
        }
        public Vector2 Down()
        {
            return new Vector2(x, y + 1);
        }

        public override string ToString()
        {
            return $"Vector2[X: {x}, Y: {y}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                Vector2 v = (Vector2)obj;
                if (this.x == v.x && this.y == v.y)
                    return true;
            }
            return false;
        }

        public bool EqualsInt(Vector2 v)
        {
            return this.GetXInt() == v.GetXInt() && this.GetYInt() == v.GetYInt();
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator +(Vector2 v1, float f)
        {
            return new Vector2(v1.x + f, v1.y + f);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator -(Vector2 v1, float f)
        {
            return new Vector2(v1.x - f, v1.y - f);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return new Vector2(v1.x * f, v1.y * f);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            if (v2.x == 0f || v2.y == 0f)
                throw new DivideByZeroException("Tried to divide Vector2 by another Vector2 that has a zero");
            return new Vector2(v1.x / v2.x, v1.y / v2.y);
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            if (f == 0f)
                throw new DivideByZeroException("Tried to divide Vector2 by zero");
            return new Vector2(v1.x / f, v1.y / f);
        }

        /// <summary>
        /// Returns a clone of the current Vector2
        /// </summary>
        /// <returns></returns>
        public Vector2 Clone()
        {
            return new Vector2(this.x, this.y);
        }

        public float Length
        {
            get { return (float)Math.Sqrt(this.x * this.x + this.y * this.y); }
        }

        /// <summary>
        /// Returns a new Vector2 normalized
        /// </summary>
        /// <returns></returns>
        public Vector2 Normalize()
        {
            if (this.Length == 0f)
                return Vector2.Zero;
            float x = this.x / this.Length;
            float y = this.y / this.Length;
            return new Vector2(x, y);
        }
    }
}
