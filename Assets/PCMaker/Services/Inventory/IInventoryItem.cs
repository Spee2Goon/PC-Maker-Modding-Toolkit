using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IInventoryItem
    {
        ObjectSaveData CaptureInventoryPart();
        
        Task LoadIconResources();
        
        Sprite GetInventoryIcon();
        
        event Action OnIconChange;
        
        string GetNotificationText();
        
        
        float IconScale { get; }
        
        string SaveKey { get; }
        
        string GUID { get; }
        
        float Price { get; }
        
        bool CanSell { get; }
    }
}