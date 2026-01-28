using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IInventoryItem
    {
        string SaveKey { get; }
        string GUID { get; }
        float Price { get; }
        bool CanSell { get; }
        
        ObjectSaveData CaptureInventoryPart();
        Task LoadIconResources();
        Sprite GetInventoryIcon();
        float IconScale { get; }
        event Action OnIconChange;
        string GetNotificationText();
    }
}