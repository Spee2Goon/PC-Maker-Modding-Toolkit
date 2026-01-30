using UnityEngine;

namespace PCMaker.Services
{
    public class OutlineInstance : MonoBehaviour
    {
        public OutlineMode Mode;

        public Color OutlineColor;

        public float Width;

        public MeshFilter FilterComponent;
        public MeshRenderer RendererComponent;

        private SO_OutlineNormalsData _customOutlineNormals;
        
        
        public void SetupOutline()
        {
            Debug.Log($"[OutlineInstance] Setup Outline, Mode: {Mode}, Has Custom Normals: {_customOutlineNormals != null}", gameObject);
        }

        public void ClearOutline()
        {
            
        }
        
        public void SetCustomOutlineNormals(SO_OutlineNormalsData customOutlineNormals) => _customOutlineNormals = customOutlineNormals;
    }
}