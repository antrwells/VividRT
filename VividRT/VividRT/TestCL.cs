using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cloo;


namespace VividRT
{
    public class TestCL
    {

        public void Seutup()
        {

            const int vectorSize = 1024;

            // Create an OpenCL context
            var context = new ComputeContext(ComputeDeviceTypes.Gpu, new ComputeContextPropertyList(ComputePlatform.Platforms[0]), null, IntPtr.Zero);

            // Create an OpenCL command queue
            var queue = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);

            // Create input vectors and output vector
            var vectorA = Enumerable.Range(0, vectorSize).Select(i => (float)i).ToArray();
            var vectorB = Enumerable.Range(0, vectorSize).Select(i => (float)(vectorSize - i)).ToArray();
            var vectorC = new float[vectorSize];

            // Create OpenCL memory buffers for the input and output vectors
            var bufferA = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, vectorA);
            var bufferB = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, vectorB);
            var bufferC = new ComputeBuffer<float>(context, ComputeMemoryFlags.WriteOnly, vectorSize);

            // Create an OpenCL kernel from a program
            var programSource = @"
            __kernel void VectorAdd(__global const float* a, __global const float* b, __global float* c, const unsigned int n)
            {
                const unsigned int i = get_global_id(0);
                if (i < n)
                    c[i] = a[i]+b[i]*4;
            }
        ";
            var program = new ComputeProgram(context, programSource);
            program.Build(null, null, null, IntPtr.Zero);

            var kernel = program.CreateKernel("VectorAdd");

            // Set the kernel arguments
            kernel.SetMemoryArgument(0, bufferA);
            kernel.SetMemoryArgument(1, bufferB);
            kernel.SetMemoryArgument(2, bufferC);
            kernel.SetValueArgument(3, (uint)vectorSize);

            // Execute the kernel
            var workSize = new long[] { vectorSize };
            queue.Execute(kernel, null, workSize, null, null);

            // Read the output vector from OpenCL memory buffer
            queue.ReadFromBuffer(bufferC, ref vectorC, true, null);

            // Print the output vector
            Console.WriteLine(string.Join(", ", vectorC));
            int bb = 5;

        }

    }
}
