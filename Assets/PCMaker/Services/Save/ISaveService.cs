using System;
using System.Threading.Tasks;

namespace PCMaker.Services
{
    public interface ISaveService
    {
        SaveFileInfo CreateNewSave(string saveName);
        
        Task SaveGameAsync(bool isAutoSave = false);

        void LoadGame();

        void DeleteSave(SaveFileInfo save);
        
        void OpenSavesDirectory();

        Task<SaveFileInfo[]> GetAllSavesAsync(SaveFilesSortingType sortingType);

        string GetPathToSavesDirectory();

        event Action OnSaveGame;
    }
}