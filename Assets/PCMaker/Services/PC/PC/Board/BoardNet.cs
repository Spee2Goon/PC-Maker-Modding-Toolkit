using UnityEngine;

namespace PCMaker.Services
{
    public class BoardNet : MonoBehaviour
    {
        [SerializeField]
        private string NetName;

        [SerializeField]
        private BoardPin[] PinsInNet;
        
        public BoardPin[] GetPins()
        {
            return PinsInNet;
        }
        
        public string GetName() => NetName;
    }
}