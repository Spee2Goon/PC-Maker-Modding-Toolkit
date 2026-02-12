namespace PCMaker.Services
{
    public interface IPCCaseObject
    {
        PCPartObject GetPCPartObject();
        
        Screw GetPCIScrewPrefab();
        
        Screw GetStandoffScrewPrefab();
        
        bool CanConnect(PCPart part);
        
        PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments);
    }
}