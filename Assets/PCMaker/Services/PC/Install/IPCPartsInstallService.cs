namespace PCMaker.Services
{
    public interface IPCPartsInstallService
    {
        PCPartObject[] InstallPCPart(PCPart pcPart, PCPartObjectSpawnArguments arguments);
        
        bool HasInstallingParts { get; }
    }
}