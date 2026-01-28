using UnityEngine;

namespace PCMaker.Services
{
    public interface IInventoryView
    {
        void InitializeView();
        void EnableView();
        void DisableView(bool immediately);
        void Dispose();
        
        GameObject gameObject { get; } 
    }
}