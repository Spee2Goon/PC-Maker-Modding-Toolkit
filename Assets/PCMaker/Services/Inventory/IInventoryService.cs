using System;
using System.Collections.Generic;

namespace PCMaker.Services
{
    public interface IInventoryService
    {
        IInventoryView CreateInventory();
        void OpenInventory();
        void CloseInventory(bool immediately = false);
        void ClearInventory();
        bool HasAnyItemOfType<T>();
        IInventoryItem GetItemByType<T>();
        bool HasItem(IInventoryItem item);

        void AddItemToInventory(IInventoryItem item);
        void AddItemToInventory(IInventoryItem item, bool notify = true);
        void RemoveItemFromInventory(IInventoryItem item);
        void RemoveItemFromInventory(IInventoryItem item, bool notify = true);
        void RemoveItemFromInventory(string itemGUID);
        void RemoveItemFromInventory(string itemGUID, bool notify = true);
        
        IReadOnlyList<IInventoryItem> GetItems();
        
        bool isInventoryOpen { get; }

        event Action onInventoryOpen;
        event Action onInventoryClose;

        event Action<IInventoryItem> onItemAdded;
        event Action<IInventoryItem> onItemRemoved;
        event Action onItemsChanged;
    }
}