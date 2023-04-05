using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividRT.SceneGraph
{
    public class NodeCamera : GraphNode
    {

        public override Matrix4 WorldMatrix
        {
            get
            {
                return base.WorldMatrix;
            }
        }

    }
}
