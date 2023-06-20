using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class GameEngine
    {
        private static List<KeyPressEventArgs> LastFrameKeyEvents = new List<KeyPressEventArgs>();
        public static List<Keys> KeysDown = new List<Keys>();
        public static Dictionary<Keys, bool> KeysPressed = new Dictionary<Keys, bool>();
        public static List<Keys> KeysUp = new List<Keys>();

        private static List<MouseEventArgs> LastFrameMouseEvents = new List<MouseEventArgs>();
        public static List<MouseButtons> MouseButtonsDown = new List<MouseButtons>();
        public static Dictionary<MouseButtons, bool> MouseButtonsPressed = new Dictionary<MouseButtons, bool>();
        public static List<MouseButtons> MouseButtonsUp = new List<MouseButtons>();

        private static Form EngineDrawForm;
        private static Thread GameLoopThread = null;
        private static int FrameCount = 0;
        public static Camera MainCamera;
        
        public static void Destroy(GameObject go)
        {
            GameObjectManager.AllDeadGameObjects.Add(go);           
        }
        public static void Destroy(UtalText utalText)
        {
            GameObjectManager.AllDeadText.Add(utalText);
        }
        public static void Destroy(EmptyUpdatable empty)
        {
            GameObjectManager.AllDeadEmptyUpdatables.Add(empty);
        }
        public static void InitEngine(Form engineDrawForm)
        {
            EngineDrawForm = engineDrawForm;
            GameLoopThread = new Thread(GameLoop);
            EngineDrawForm.Paint += new System.Windows.Forms.PaintEventHandler(Paint);
            EngineDrawForm.KeyPress +=  new KeyPressEventHandler(KeyPressHandler);
            EngineDrawForm.KeyDown += new KeyEventHandler(KeyDownHandler);
            EngineDrawForm.KeyUp += new KeyEventHandler(KeyUpHandler);
            EngineDrawForm.MouseClick += new MouseEventHandler(ClickPressHandler);
            EngineDrawForm.MouseDown += new MouseEventHandler(ClickDownHandler);
            EngineDrawForm.MouseUp += new MouseEventHandler(ClickUpHandler);
            engineDrawForm.Height = MainCamera.ySize;
            engineDrawForm.Width = MainCamera.xSize;
            GameLoopThread.Start();
        }

        private static void ClickDownHandler(object? sender, MouseEventArgs e)
        {
            MouseButtonsDown.Add(e.Button);
            Debug.WriteLine("Apretado Click " + e.Button);
        }

        private static void ClickPressHandler(object? sender, MouseEventArgs e)
        {
            LastFrameMouseEvents.Add(e);
        }

        private static void ClickUpHandler(object? sender, MouseEventArgs e)
        {
            MouseButtonsUp.Add(e.Button);
            Debug.WriteLine("Soltado Click " + e.Button);
        }

        public static bool Click(MouseButtons buttonClicked)
        {
            foreach (MouseButtons mb in MouseButtonsPressed.Keys)
            {
                if (mb == buttonClicked)
                {
                    return true;
                }
            }
            return false;
        }

        private static void KeyUpHandler(object sender, KeyEventArgs e)
        {
            KeysUp.Add(e.KeyCode);
            Console.WriteLine("Soltada Tecla " + e.KeyCode);
        }

        private static void KeyDownHandler(object sender, KeyEventArgs e)
        {
            KeysDown.Add(e.KeyCode);
            Console.WriteLine("Apretada Tecla " + e.KeyCode);
        }

        private static void KeyPressHandler(object sender, KeyPressEventArgs e)
        {
            LastFrameKeyEvents.Add(e);
            //Console.WriteLine ("Tecla " + e.KeyChar);
        }

        public static bool KeyPress(Keys KeyCode)
        {
            foreach(Keys k in KeysPressed.Keys)
            {
                if (k == KeyCode)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool KeyUp(Keys KeyCode)
        {
            foreach (Keys k in KeysUp)
            {
                if (k == KeyCode)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool KeyDown(Keys KeyCode)
        {
            foreach (Keys k in KeysDown)
            {
                if (k == KeyCode)
                {                   
                    return true;
                }
            }
            return false;
        }

        public static void Start()
        {

        }
        public static void GameLoop()
        {

            while (!EngineDrawForm.IsDisposed)
            {
                Thread.Sleep(1000 / 120);
                try
                {
                    EngineDrawForm.Refresh();
                    continue;
                }
                catch
                {
                    EngineDrawForm.Invalidate();
                    //continue; //Console.WriteLine("Cant");
                }
                
                Time.UpdateDeltaTime();
                GameObjectManager.Update();
                PhysicsEngine.Update();

                for (int i = 0; i < KeysUp.Count; i++)
                {
                    Keys k = KeysUp[i];
                    KeysPressed.Remove(k);
                }

                for (int i = 0; i < KeysDown.Count; i++)
                {
                    Keys k = KeysDown[i];
                    if (!KeysPressed.ContainsKey(k))
                    {
                        KeysPressed.Add(k, true);
                    }
                }
                
                for (int i = 0; i < KeysUp.Count; i++)
                {
                    Keys k = KeysUp[i];
                    KeysPressed.Remove(k);                  
                }
                KeysDown = new List<Keys>();
                KeysUp = new List<Keys>();
                LastFrameKeyEvents = new List<KeyPressEventArgs>();

                for (int i = 0; i < MouseButtonsUp.Count; i++)
                {
                    MouseButtons mb = MouseButtonsUp[i];
                    MouseButtonsPressed.Remove(mb);
                }

                for (int i = 0; i < MouseButtonsDown.Count; i++)
                {
                    MouseButtons mb = MouseButtonsDown[i];
                    if (!MouseButtonsPressed.ContainsKey(mb))
                    {
                        MouseButtonsPressed.Add(mb, true);
                    }
                }

                for (int i = 0; i < MouseButtonsUp.Count; i++)
                {
                    MouseButtons mb = MouseButtonsUp[i];
                    MouseButtonsPressed.Remove(mb);
                }
                MouseButtonsUp = new List<MouseButtons>();
                MouseButtonsDown = new List<MouseButtons>();
                LastFrameMouseEvents = new List<MouseEventArgs>();

                GameObjectManager.DeadUpdate();
            }
            
        }
        public static Vector2 WorldToCameraPos(Vector2 pos)
        {
            return new Vector2((pos.x - MainCamera.Position.x) / MainCamera.scale+MainCamera.xSize*0.5f,
                (pos.y - MainCamera.Position.y) / MainCamera.scale+ MainCamera.ySize * 0.5f);
        }
        private static void Paint(Object sender, PaintEventArgs e)
        {
            int newXSize = EngineDrawForm.Width;
            int newYSize = EngineDrawForm.Height;
            bool changed = false;
            if (e.Graphics.ClipBounds.Width < MainCamera.xSize)
            {
                newXSize = MainCamera.xSize + (MainCamera.xSize - (int)e.Graphics.ClipBounds.Width);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Height < MainCamera.ySize)
            {
                newYSize = MainCamera.ySize + (MainCamera.ySize - (int)e.Graphics.ClipBounds.Height);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Height > MainCamera.ySize)
            {
                newYSize = EngineDrawForm.Height - ((int)e.Graphics.ClipBounds.Height - MainCamera.ySize);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Width > MainCamera.xSize)
            {
                newXSize = EngineDrawForm.Width - ((int)e.Graphics.ClipBounds.Width - MainCamera.xSize);
                changed = true;
            }
            if (changed)
            {
                EngineDrawForm.Size = new Size(newXSize, newYSize);
            }

            Draw(e.Graphics);
            
            //Console.WriteLine("Frame");
        }
        public static void Draw(Graphics graphics)
        {
            for (int i = 0; i < GameObjectManager.AllGameObjects.Count; i++)
            {
                GameObject go = GameObjectManager.AllGameObjects[i];
                go.Draw(graphics, MainCamera);
            }
            for (int i = 0; i < GameObjectManager.AllText.Count; i++)
            {
                UtalText utext = GameObjectManager.AllText[i];
                utext.DrawString(graphics);
            }
        }
    }
}
