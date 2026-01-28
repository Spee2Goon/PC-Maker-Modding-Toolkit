namespace PCMaker.Services
{
    public interface IConfigService
    {
        public void SaveCurrentConfig();
        public void LoadConfigFile();
        GameConfig GetConfig();
    }
}