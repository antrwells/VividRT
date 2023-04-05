using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VividRT.Mesh;

namespace VividRT.SceneGraph
{
    public class NodeEntity : GraphNode
    {

        public List<Mesh3D> Meshes
        {
            get;
            set;
        }

        public NodeEntity()
        {

            Meshes = new List<Mesh3D>();

        }

        public void AddMesh(Mesh3D mesh)
        {

            Meshes.Add(mesh);

        }

    }
}
