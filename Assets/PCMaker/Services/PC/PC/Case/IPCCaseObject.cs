namespace PCMaker.Services
{
    public interface IPCCaseObject
    {
        PCPartObject GetPCPartObject();
        
        Screw GetPCIScrewPrefab();
        
        bool CanConnect(PCPart part);
        
        PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments);
    }
}