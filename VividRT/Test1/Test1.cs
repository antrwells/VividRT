using Cloo;
using Q.Draw.Simple;
using Q.Texture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VividRT;
using VividRT.App;
using VividRT.Import;
using VividRT.SceneGraph;

namespace Test1
{
    public class Test1 : VividApp
    {

        public Test1()
        {

        }

        public override void Init()
        {
            draw = new Draw2D();
            tex = new Texture2D(256, 256);

            var imp = new Importer();

            var m1 = imp.ImportMesh("data/test1.fbx");
            mesh1 = m1;
            graph1 = new SceneGraph();
            graph1.Camera = new NodeCamera();
            graph1.AddLight(new NodeLight());
            cam1 = graph1.Camera;
            graph1.Root.AddNode(m1);
            //TestCL cl1 = new TestCL();
            //cl1.Seutup();

            int vectorSize = 1024;

            // Create input vectors and output vector
            var vectorA = Enumerable.Range(0, vectorSize).Select(i => (float)i).ToArray();
            var vectorB = Enumerable.Range(0, vectorSize).Select(i => (float)(vectorSize - i)).ToArray();
            var vectorC = new float[vectorSize];

            // Create OpenCL memory buffers for the input and output vectors
            var bufferA = new ComputeBuffer<float>(CLProgram.context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, vectorA);
            var bufferB = new ComputeBuffer<float>(CLProgram.context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, vectorB);
            var bufferC = new ComputeBuffer<float>(CLProgram.context, ComputeMemoryFlags.WriteOnly, vectorSize);

            CLProgram t1 = new CLProgram("cl/test.cl");
            CLKernel k1 = new CLKernel(t1, "VectorAdd");

            k1.SetMem(0, bufferA);
            k1.SetMem(1, bufferB);
            k1.SetMem(2, bufferC);
            k1.SetValue(3,(uint)vectorSize);
           
            k1.Exec(vectorSize);

            CLProgram.queue.ReadFromBuffer(bufferC, ref vectorC, true, null);




            int b = 5;
        }

        public override void Render()
        {
            //base.Render();

            draw.Rect(20, 20, 256, 256, tex, new OpenTK.Mathematics.Vector4(1, 1, 1, 1));

        }

        Draw2D draw;
        Texture2D tex;
        SceneGraph graph1;
        NodeCamera cam1;
        NodeEntity mesh1;

    }
}
