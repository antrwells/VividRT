using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VividRT.Mesh
{

    public struct Vertex
    {

        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 BiNormal;
        public Vector3 Tangent;
        public Vector3 TexCoord;
        public Vector3 Color;

    }

    public struct Triangle
    {
        public int V0, V1, V2;
    }

    public class Mesh3D
    {

        public List<Vertex> Vertices
        {
            get;
            set;
        }

        public List<Triangle> Triangles
        {
            get;
            set;
        }

        public Mesh3D()
        {

            Vertices = new List<Vertex>();
            Triangles = new List<Triangle>();

        }
        public void AddVertex(Vertex v)
        {
            Vertices.Add(v);
        }
        public void AddTriangle(Triangle t)
        {
            Triangles.Add(t);
        }

    }
}
