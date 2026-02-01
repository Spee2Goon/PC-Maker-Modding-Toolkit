using UnityEngine;

namespace PCMaker.Services
{
    public class NetPathBuilder : MonoBehaviour
    {
        [SerializeField]
        private float lineWidth = 0.000125f;

        [SerializeField]
        private MeshFilter FilterComponent;

        [SerializeField]
        private MeshRenderer RendererComponent;
        
        public void AddPinLines(BoardPin targetPin) { }
        
        public void BuildMesh() { }

        public void ClearNet() { }
    }
}