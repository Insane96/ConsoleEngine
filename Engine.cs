using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine
{
    /// <summary>
    /// Main Engine Class
    /// </summary>
    public class Engine
    {
        public static float fps;

        private static List<GameObject> gameObjects;

        /// <summary>
        /// Initializes the Renderer by creating the Window Buffer, setting the window Width, Height and Title and Show/Hide the Cursor.
        /// 
        /// </summary>
        public static void Init(int width, int height, bool showCursor = true, string title = "")
        {
            Renderer.Init(width, height, showCursor, title);
            gameObjects = new List<GameObject>();
        }

        /// <summary>
        /// Needs to be called in the Main() loop
        /// Takes care of updating Input, Time, Renderer, Removal and Addition of Gameobjects other than their Update and Draw
        /// </summary>
        public static void MainLoop()
        {
            Input.UpdateKeyPress();
            Time.Update();

            AddQueuedGameObjects();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.HasCollision)
                    TestCollisions(gameObject);

                gameObject.Update();
            }
            RemoveQueuedGameObjects();
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw();
            }

            Renderer.Draw();

            fps = 1f / Time.DeltaTime;
        }

        private static void TestCollisions(GameObject gameObject)
        {
            foreach (var gameObjectCollision in gameObjects)
            {
                if (gameObject.Equals(gameObjectCollision))
                    continue;

                if (!gameObjectCollision.HasCollision)
                    continue;

                if (gameObject.pos.GetXInt() < gameObjectCollision.pos.GetXInt() + gameObjectCollision.size.x &&
                    gameObject.pos.GetXInt() + gameObject.size.x > gameObjectCollision.pos.GetXInt() &&
                    gameObject.pos.GetYInt() < gameObjectCollision.pos.GetYInt() + gameObjectCollision.size.y &&
                    gameObject.pos.GetYInt() + gameObject.size.y > gameObjectCollision.pos.GetYInt())
                {
                    gameObject.OnCollision(gameObjectCollision);
                }
            }
        }

        private static List<GameObject> queuedGameObjects = new List<GameObject>();

        /// <summary>
        /// Adds a GameObject to the list of GameObjects to update and draw. Names must be univocal
        /// </summary>
        /// <param name="gameObject"></param>
        public static void AddGameObject(GameObject gameObject)
        {
            foreach (var g in gameObjects)
            {
                if (g.GetName().Equals(gameObject.GetName()))
                    throw DuplicateGameObjectNameException(g.GetName());
            }
            queuedGameObjects.Add(gameObject);

        }

        private static void AddQueuedGameObjects()
        {
            foreach (GameObject gameObject in queuedGameObjects)
            {
                gameObjects.Add(gameObject);
            }

            queuedGameObjects.Clear();
        }


        private static List<GameObject> queuedRemoveGameObjects = new List<GameObject>();

        /// <summary>
        /// Removes a GameObject from the list of GameObjects to update and draw.
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveGameObjectByName(string name)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.GetName().Equals(name))
                {
                    queuedRemoveGameObjects.Add(gameObject);
                    break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RemoveGameObjectByObj(GameObject gameObject)
        {
            foreach (GameObject gO in gameObjects)
            {
                if (gO.Equals(gameObject))
                {
                    queuedRemoveGameObjects.Add(gameObject);
                    return;
                }
            }
            throw NoGameObjectException(gameObject);

        }

        private static void RemoveQueuedGameObjects()
        {
            foreach (GameObject gameObject in queuedRemoveGameObjects)
            {
                gameObjects.Remove(gameObject);
            }

            queuedRemoveGameObjects.Clear();
        }

        private static Exception DuplicateGameObjectNameException(string name)
        {
            return new Exception("Tried to add a GameObject with an already existing name: " + name);
        }
        private static Exception NoGameObjectException(GameObject gameObject)
        {
            return new Exception("Tried to remove a GameObject that didn't exist: " + gameObject);
        }
    }
}
