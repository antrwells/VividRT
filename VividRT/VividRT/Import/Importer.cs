using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VividRT.SceneGraph;
using Assimp;
using VividRT.Mesh;

namespace VividRT.Import
{
    public class Importer
    {

        public NodeEntity ImportMesh(string path)
        {


            NodeEntity res = new NodeEntity();

            AssimpContext importer = new AssimpContext();

            // Import the 3D model from a file
            Scene scene = importer.ImportFile(path, PostProcessSteps.Triangulate );

            List<Mesh3D> meshes = new List<Mesh3D>();

            foreach(var mesh in scene.Meshes)
            {

                Mesh3D vm = new Mesh3D();
                int vc = mesh.Vertices.Count;

                for(int v = 0; v < vc; v++)
                {

                    Vertex vertex = new Vertex();
                    vertex.Position = new OpenTK.Mathematics.Vector3(mesh.Vertices[v].X, mesh.Vertices[v].Y, mesh.Vertices[v].Z);
                    vertex.Normal = new OpenTK.Mathematics.Vector3(mesh.Normals[v].X, mesh.Normals[v].Y, mesh.Normals[v].Z);
                    vertex.TexCoord = new OpenTK.Mathematics.Vector3(mesh.TextureCoordinateChannels[0][v].X, mesh.TextureCoordinateChannels[0][v].Y, 0);

                    vm.AddVertex(vertex);

                }

                for(int t = 0; t < mesh.FaceCount; t++)
                {

                    Triangle tri = new Triangle();
                    tri.V0 = mesh.Faces[t].Indices[0];
                    tri.V1 = mesh.Faces[t].Indices[1];
                    tri.V2 = mesh.Faces[t].Indices[2];

                    vm.AddTriangle(tri);

                }

                res.AddMesh(vm);

            }

            return res;

        }

    }
}
