using Cloo;
using OpenTK.Compute.OpenCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividRT
{
    public class CLProgram
    {


        string code;
        public static ComputeContext context;
        public static ComputeCommandQueue queue;


        public ComputeProgram program;
        public static bool InitCL()
        {
            context = new ComputeContext(ComputeDeviceTypes.Gpu, new ComputeContextPropertyList(ComputePlatform.Platforms[0]), null, IntPtr.Zero);

            // Create an OpenCL command queue
            queue = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);

            return true;

        }

        public CLProgram(string path)
        {

            code = File.ReadAllText(path);
            program = new ComputeProgram(context, code);
            program.Build(null, null, null, IntPtr.Zero);

         

        }

        

    }
    public class CLKernel
    {
        public ComputeKernel kern;
        public CLKernel(CLProgram program,string kernel)
        {

            kern = program.program.CreateKernel("VectorAdd");



        }

        public void Exec(int size)
        {

            var workSize = new long[] { size };
            long start = Environment.TickCount64;
            CLProgram.queue.Execute(kern, null, workSize, null, null);
            long  end = Environment.TickCount64;
            long time = end - start;
            Console.WriteLine("MS:" + time);

           
        }
       
        public void SetMem(int index,ComputeMemory mem)
        {

            kern.SetMemoryArgument(index, mem);

        }
        public void SetValue<T>(int index,T val) where T : struct 
        {

            kern.SetValueArgument<T>(index, val);

        }

    }
   
}
