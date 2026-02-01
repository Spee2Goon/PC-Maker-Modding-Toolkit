namespace PCMaker.Services
{
    
    public interface ISaveable
    {
        object CaptureState();
        
        void RestoreState(string stateJson);
        
        string SaveKey { get; }
    }
}