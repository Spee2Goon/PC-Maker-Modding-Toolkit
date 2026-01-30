namespace PCMaker.Services
{
    public interface IPCPartsRemoveService
    {
        void RemovePCPart(PCPartObject pcPartObject);
        bool HasRemovingPart { get; }
    }
}