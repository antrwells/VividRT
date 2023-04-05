using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividRT.SceneGraph
{
    public class SceneGraph
    {

        public GraphNode Root
        {
            get;
            set;
        }

        public NodeCamera Camera
        {
            get;
            set;
        }

        public List<NodeLight> Lights
        {
            get;
            set;
        }

        public SceneGraph()
        {

            Root = new GraphNode();
            Camera = new NodeCamera();
            Lights = new List<NodeLight>();

        }
        public void AddLight(NodeLight light)
        {

            Lights.Add(light);

        }

    }
}
