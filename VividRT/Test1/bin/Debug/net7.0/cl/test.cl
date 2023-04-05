  __kernel void VectorAdd(__global const float* a, __global const float* b, __global float* c, const unsigned int n)
            {
                const unsigned int i = get_global_id(0);
                if (i < n)
                    c[i] = i;
            }
