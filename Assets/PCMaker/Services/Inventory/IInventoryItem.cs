using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IInventoryItem
    {
        Sprite GetInventoryIcon();
        
        event Action OnIconChange;
        
        string GetNotificationText();
        
        
        float IconScale { get; }
        
        string[] SaveKeys { get; }
        
        string GUID { get; }
        
        float Price { get; }
    }
}