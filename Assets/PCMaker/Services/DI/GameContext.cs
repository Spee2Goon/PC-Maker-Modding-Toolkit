using VContainer;

namespace PCMaker.Services
{
    public static class GameContext
    {
        public static bool isAssetsLoaded = false;

        public static SaveFileInfo CurrentSaveFileInfo;

        public static IObjectResolver LoaderSceneResolver;
        public static IObjectResolver MenuSceneResolver;
        public static IObjectResolver GameSceneResolver;
        
        
        public static IOutlineService OutlineService;
    }
}