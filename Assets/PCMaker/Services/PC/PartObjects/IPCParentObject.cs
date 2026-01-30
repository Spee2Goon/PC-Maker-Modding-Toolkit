namespace PCMaker.Services
{
    public interface IPCParentObject
    {
        PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments) { return null; }
        PCPart GetPartReference();
        PCPartObject GetObject();
        string GetGUID();
    }
}