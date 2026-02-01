namespace PCMaker.Services
{
    public interface IPCBThermalInterfacesService
    {
        IPCBThermalInterfacesView CreateView();
        
        void DisposeByGameScene();
        
        void WorkWithThermalPads(IPartObjectWithThermalInterfaces padsOwner);
        
        void StopWork();
        
        bool isWorkingOnInterfaces { get; }
    }
}