using System;

namespace PCMaker.Services
{
    public interface IPCPartsService
    {
        public void RegisterPCPart(PCPart pcPart);
        
        public void RegisterPCPart(PCPart pcPart, string partGuid);
        
        bool TryGetPCPartByGUID(string guid, out PCPart pcPart);
        
        PCPart GetPCPartByGUID(string guid);

        void InjectPCPart(PCPart pcPart);
        
        void InjectPCPartObject(PCPartObject pcPart);

        event Action<PCPart> OnRegisterPCPart;
    }
}