using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
namespace Q.Texture
{
    public class Texture2D : Texture
    {
        public static Dictionary<string,Texture> Cached = new Dictionary<string, Texture>();


   
   
        public void CopyTex(int x,int y)
        {
            //Bind(TextureUnit.Unit0);
            //GL.CopyTexSubImage2D(TextureTarget.Texture2d, 0, 0, 0, x, y, Width, Height);
            GL.CopyTextureSubImage2D(Handle, 0, 0, 0, x, y, Width, Height);
            //GL.CopyT
            //Release(TextureUnit.Unit0);
            
        }

      
        

        public override void DestroyTexture()
        {
            //base.DestroyTexture();
       
         //   {
                GL.DeleteTexture(Handle);
                //GL.delete
               
                Raw = null;
                DestroyNow = false;
                Destroyed = true;
                //Console.WriteLine("Tex destroyed.");
       //     }
        }

        public Texture2D(int width,int height)
        {

            Data = new byte[width * height * 3];
            GL.Enable(EnableCap.Texture2d);
            Width = width;
            Height = height;
            Handle = GL.CreateTexture(TextureTarget.Texture2d);
            GL.BindTexture(TextureTarget.Texture2d, Handle);

            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, Data);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
           // GL.GenerateMipmap(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, OpenTK.Graphics.TextureHandle.Zero);

            Format = InternalFormat.Rgb;

        }
        public void Upload()
        {

            GL.Enable(EnableCap.Texture2d);
            GL.BindTexture(TextureTarget.Texture2d, Handle);

            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, Width, Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, Data);

        }

        public void SetPixel(int x,int y,byte r,byte g,byte b)
        {

            int loc = y * Width * 3;
            loc = loc +(x*3);

            Data[loc++] = r;
            Data[loc++] = g;
            Data[loc] = b;



        }

        public Texture2D(string path,bool force_alpha = false)
        {
          
            Console.WriteLine("Loading texture.");
            int image_width=64;
            int image_height=64;

            LoadData(path);

            Handle = GL.CreateTexture(TextureTarget.Texture2d);
            GL.BindTexture(TextureTarget.Texture2d, Handle);

            GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Data);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.GenerateMipmap(TextureTarget.Texture2d);

            GL.BindTexture(TextureTarget.Texture2d, OpenTK.Graphics.TextureHandle.Zero);

            Format = InternalFormat.Rgba8;

            byte[] raw;
           // Handle = GL.GenTexture();



         
        }
        public void LoadData(string path)
        {
          //  Path = path;

            // Load PNG texture from file
            using (var image = Image.Load<Rgba32>(path))
            {
                // Get the raw pixel data as a 2D span

                Data = new byte[image.Width * image.Height * 4];
                Width = image.Width; Height = image.Height;
             //   BPP = 4;

                int loc = 0;

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {

                        var pix = image[x, y];
                        Data[loc++] = pix.R;
                        Data[loc++] = pix.G;
                        Data[loc++] = pix.B;
                        Data[loc++] = pix.A;


                    }
                }

            }
        }

        public void BindData()
        {
            GL.Enable(EnableCap.Texture2d);
           // Handle = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2d, Handle);
            GL.TexImage2D(TextureTarget.Texture2d, 0,InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Raw);

            DataBound = true;
            
            //Handle = GL.CreateTexture(TextureTarget.Texture2d);
            //GL.TextureStorage2D(Handle, 1, SizedInternalFormat.Rgba8, Width, Height);


            /*
            unsafe
            {
                GCHandle pinnedArray = GCHandle.Alloc(Raw, GCHandleType.Pinned);
                IntPtr pointer = pinnedArray.AddrOfPinnedObject();
                GL.TextureSubImage2D(Handle, 0, 0, 0, Width, Height, PixelFormat.Rgba, PixelType.UnsignedByte, pointer);
                //Console.WriteLine("Created texture2D. W:" + Width + " H:" + Height + " Handle:" + Handle.Handle);
            }
            */
            GL.TextureParameteri(Handle, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TextureParameteri(Handle, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
           // GL.GenerateTextureMipmap(Handle);
        }

        public override void Bind(TextureUnit unit)
        {

            if (Loading) return;
            if (DataBound == false)
            {
                DataBound = true;
                BindData();               

            }
            
        
            if (Destroy)
            {
                DestroyAt = Environment.TickCount64 + WaitDestroy;
            }

            uint t_unit = (uint)unit;

            OpenTK.Graphics.OpenGL.TextureUnit texu = OpenTK.Graphics.OpenGL.TextureUnit.Texture0;

            int oid = (int)texu + (int)unit;

            texu = (OpenTK.Graphics.OpenGL.TextureUnit)oid;


            GL.Enable(EnableCap.Texture2d);
            GL.ActiveTexture(texu);
            // GL.ClientActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + texu));
            GL.BindTexture(TextureTarget.Texture2d,Handle);


            // GL.BindImageTexture(t_unit, Handle, 0, false, 0, BufferAccessARB.ReadOnly,Format);

            base.Bind(unit);
        }

        public override void Release(TextureUnit unit)
        {
            uint t_unit = (uint)unit;

            OpenTK.Graphics.OpenGL.TextureUnit texu = OpenTK.Graphics.OpenGL.TextureUnit.Texture0;

            int oid = (int)texu + (int)unit;

            texu = (OpenTK.Graphics.OpenGL.TextureUnit)oid;
            base.Release(unit);
            GL.Enable(EnableCap.Texture2d);
            GL.ActiveTexture(texu);
            // GL.ClientActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + texu));
            GL.BindTexture(TextureTarget.Texture2d,OpenTK.Graphics.TextureHandle.Zero);
        }

    }
}
