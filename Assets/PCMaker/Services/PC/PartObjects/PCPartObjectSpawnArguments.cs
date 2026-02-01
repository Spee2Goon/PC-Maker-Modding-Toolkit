namespace PCMaker.Services
{
    public class PCPartObjectSpawnArguments
    {
        public PCPartObjectSpawnArguments(bool spawnInstalled, PCPartObject_SpawnEnvironment objectSpawnEnvironment, IPC parentPC)
        {
            SpawnInstalled = spawnInstalled;
            ObjectSpawnEnvironment = objectSpawnEnvironment;
            ParentPC = parentPC;
        }
        
        public PCPartObjectSpawnArguments(PCPartObjectSpawnArguments basicArguments)
        {
            SpawnInstalled = basicArguments.SpawnInstalled;
            ObjectSpawnEnvironment = basicArguments.ObjectSpawnEnvironment;
            ParentPC = basicArguments.ParentPC;
        }
        
        public bool SpawnInstalled;
        
        public PCPartObject_SpawnEnvironment ObjectSpawnEnvironment;
        
        public IPC ParentPC;
    }
}