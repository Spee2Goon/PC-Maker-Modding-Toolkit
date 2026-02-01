namespace PCMaker.Services
{
    public interface ITickRateService
    {
        TickRateManager SpawnManager();
        
        void DestroyManager();
    }
}