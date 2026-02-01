using UnityEngine;

namespace PCMaker.Services
{
    public interface IPlayerService
    {
        IPlayer CreatePlayer(Transform spawnPoint);
        
        void DisposePlayer();
        
        IPlayer GetPlayer();
    }
}