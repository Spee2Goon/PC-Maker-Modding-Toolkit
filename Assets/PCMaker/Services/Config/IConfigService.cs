using System;

namespace PCMaker.Services
{
    public interface IConfigService
    {
        public void SaveCurrentConfig();
        
        public void LoadConfigFile();
        
        GameConfig GetConfig();
        
        event Action<GameConfig> OnSaveConfig;
    }
}