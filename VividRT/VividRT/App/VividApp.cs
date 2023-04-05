using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenTK.Windowing.Desktop;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace VividRT.App
{
    public class VividApp
    {

        public static int Width { get; set; }
        public static int Height { get; set; }
        public string Title { get; set; }
        public bool FullScreen { get; set; }

        GameWindow window;
        public VividApp()
        {

            Width = 1024;
            Height = 768;
            Title = "Vivid Application";
            FullScreen = false;

        }

        public void CreateOutput()
        {

            GameWindowSettings settings = new GameWindowSettings();
            NativeWindowSettings native_settings = new NativeWindowSettings();

            settings.RenderFrequency = 60;
            settings.IsMultiThreaded = true;

            native_settings.Size = new OpenTK.Mathematics.Vector2i(Width, Height);
            native_settings.Title = Title;
            native_settings.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            native_settings.APIVersion = new Version(3, 2);
            native_settings.AutoLoadBindings = true;
            native_settings.Flags = OpenTK.Windowing.Common.ContextFlags.Debug;
            native_settings.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;



            window = new GameWindow(settings, native_settings);

        }

        public virtual void Init()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public void Run()
        {

            CreateOutput();

            CLProgram.InitCL();

            Init();

            while (true)
            {
                window.ProcessEvents();
                GL.ClearColor(1, 0, 0, 1);
                // Clear the screen
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Update();

                Render();
                window.SwapBuffers();

            }

        }

    }
}
