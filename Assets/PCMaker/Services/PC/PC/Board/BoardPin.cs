using UnityEngine;

namespace PCMaker.Services
{
    public class BoardPin : MonoBehaviour
    {
        [SerializeField]
        private string DeviceName;

        [SerializeField]
        private BoardNet ParentNet;

        public BoardNet GetNet()
        {
            return ParentNet;
        }
    }
}