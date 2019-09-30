using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine
{
    /// <summary>
    /// Main Game object class, every object in game should inherith from this class
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Game object name, must be univocal
        /// </summary>
        private string name;
        /// <summary>
        /// Game Object Position
        /// </summary>
        public Vector2 pos;
        /// <summary>
        /// Game Object size
        /// </summary>
        public Vector2 size;
        /// <summary>
        /// The shape that's usually drawn on screen
        /// </summary>
        public string[] shape;

        /// <summary>
        /// Returns true if GameObject size is bigger than 0,0 (and 0,1 or 1,0)
        /// </summary>
        public bool HasCollision
        {
            get { return size.x != 0 && size.y != 0; }
        }


        /// <summary>
        /// Creates a new gameObject with set name pos to 0,0 and size 0,0 and with empty shape
        /// </summary>
        public GameObject(string name) : this(name, Vector2.Zero) { }

        /// <summary>
        /// Creates a new gameObject with set name and pos and size 0,0 and with empty shape
        /// </summary>
        public GameObject(String name, Vector2 pos) : this(name, pos, Vector2.Zero) { }

        /// <summary>
        /// Creates a new gameObject with set name, pos and size with empty shape
        /// </summary>
        public GameObject(String name, Vector2 pos, Vector2 size) : this(name, pos, size, new string[size.GetYInt()]) { }

        /// <summary>
        /// Creates a new gameObject with name, pos, size, shape and trueCollision
        /// </summary>
        public GameObject(String name, Vector2 pos, Vector2 size, string[] shape)
        {
            this.name = name;
            this.pos = pos;
            this.size = size;
            this.shape = shape;
        }

        /// <summary>
        /// Called every cycle to update the e.g. position, etc. 
        /// By default it does nothing
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called every cycle to draw this GameObject on the screen. By default it does nothing
        /// </summary>
        public virtual void Draw()
        {
            
        }

        /// <summary>
        /// Called everytime a GameObject (other) collides with this one
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other) { }

        /// <summary>
        /// Returns GameObject name
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            return name;
        }

        /// <summary>
        /// Returns a string showing the name of the gameobject
        /// </summary>
        public override string ToString()
        {
            return $"GameObject[name: {name}]";
        }
    }
}
