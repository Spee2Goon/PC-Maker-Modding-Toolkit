using UnityEngine;

namespace PCMaker.Services
{
    public class CableInstance : MonoBehaviour
    {
        public CablePoint[] Points;
        public float Radius = 1;
        public Vector2 RadiusScale = new Vector2(1, 1);
        
        public int SidesSegmentCount = 12;
        public MeshFilter FilterComponent;
        public MeshRenderer RendererComponent;

        public Transform GenerationPointsParent;
    }
}