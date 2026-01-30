using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PCMaker.Services
{
    public class BoardMicroElementSlot : MonoBehaviour
    {
        [SerializeField]
        private string SlotName;

        [SerializeField]
        private List<BoardElementPin> Pins;

        public MicroElement ElementInSlot;
        
        public void SetName(string name) => SlotName = name;
        public string GetName() => SlotName;
        
        public IReadOnlyList<BoardElementPin> GetPins() => Pins;

        public void AddPin(BoardElementPin pin)
        {
            Pins.Add(pin);
        }

        public void SetElementInSlot(MicroElement element)
        {
            ElementInSlot = element;
        }

        public void SortPins()
        {
            Pins = Pins.OrderBy(p => p.PinName).ToList();
        }
        
        public void ClearAllPins() => Pins.Clear();

        public void SetHoverable(bool hoverable)
        {
            if (ElementInSlot != null)
            {
                ElementInSlot.SetHoverableCollidersActive(hoverable);
            }
        }
    }

    [Serializable]
    public class BoardElementPin
    {
        public string PinName;
        public BoardPin Pin;
    }
}
