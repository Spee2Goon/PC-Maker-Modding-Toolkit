using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PCMaker.Services
{
    [CreateAssetMenu(fileName = "OutlineNormals")]
    public class SO_OutlineNormalsData : ScriptableObject
    {
        public Mesh SourceMesh;


        public Vector3[] Normals;

#if UNITY_EDITOR
        [SerializeField]
        private Vector3 MeshScale = Vector3.one;


        [ContextMenu("Calculate Normals")]
        public void CalculateNormals()
        {
            if (SourceMesh != null)
            {
                Normals = ProcessMeshToNormals(SourceMesh);

                if (!SourceMesh.isReadable)
                {
                    Debug.LogError($"Make mesh {SourceMesh.name} readable please");
                }
            }
        }

        private Vector3[] calculatedNormals;
        private KeyValuePair<Vector3, int>[] verticesArray;
        private Vector3 smoothNormal;

        private IGrouping<Vector3, KeyValuePair<Vector3, int>>[] groups;

        private Vector3[] ProcessMeshToNormals(Mesh mesh)
        {
            Vector3[] vertices = mesh.vertices;
            Vector3[] originals = mesh.normals;

            Vector3[] results = new Vector3[originals.Length];

            Dictionary<Vector3, List<int>> groups = new Dictionary<Vector3, List<int>>(vertices.Length);

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 pos = vertices[i];

                if (!groups.TryGetValue(pos, out List<int> list))
                {
                    list = new List<int>();
                    groups[pos] = list;
                }

                list.Add(i);
            }

            foreach (var entry in groups)
            {
                List<int> indices = entry.Value;

                if (indices.Count == 1)
                {
                    int index = indices[0];

                    Vector3 normal = originals[index];
                    normal = Vector3.Scale(normal, MeshScale);
                    normal.Normalize();
                    results[index] = normal;
                    continue;
                }

                Vector3 smoothNormal = Vector3.zero;
                for (int i = 0; i < indices.Count; i++)
                {
                    int index = indices[i];
                    smoothNormal += originals[index];
                }

                
                smoothNormal = Vector3.Scale(smoothNormal, MeshScale);

                
                smoothNormal.Normalize();

                
                for (int i = 0; i < indices.Count; i++)
                {
                    int index = indices[i];
                    results[index] = smoothNormal;
                }
            }

            return results;
        }
#endif
    }
}