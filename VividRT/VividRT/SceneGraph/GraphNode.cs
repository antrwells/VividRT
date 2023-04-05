using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
namespace VividRT.SceneGraph
{
    public class GraphNode
    {

        public List<GraphNode> Nodes
        {
            get;
            set;
        }

        public GraphNode Root
        {
            get;
            set;
        }

        private Matrix4 _LocalRotation = Matrix4.Identity;
        private Vector3 _LocalPosition = Vector3.Zero;
        private Vector3 _LocalScale = Vector3.Zero;

        public virtual Matrix4 WorldMatrix
        {
            get
            {

                return _LocalRotation;
            }
        }

        public GraphNode()
        {

            Nodes = new List<GraphNode>();
            Root = null;

        }

        public void AddNode(GraphNode node)
        {

            Nodes.Add(node);
            node.Root = this;

        }


        public void SetPosition(Vector3 position)
        {
            _LocalPosition = position;
        }

        public void SetScale(Vector3 scale)
        {
            _LocalScale = scale;
        }

        public void SetRotation(Vector3 euler)
        {

            _LocalRotation = Matrix4.Identity;

        }


    }
}
