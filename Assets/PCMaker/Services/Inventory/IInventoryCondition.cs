namespace PCMaker.Services
{
    public interface IInventoryCondition
    {
        bool CanOpenInventory { get; }
        bool CanCloseInventory { get; }
    }
}