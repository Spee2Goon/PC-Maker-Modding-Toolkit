using VContainer;

namespace PCMaker.Services
{
    public static class GameContext
    {
        public static bool isAssetsLoaded = false;
        public static bool RequreMenuTransition = true;

        //Save info
        public static SaveFileInfo CurrentSaveFileInfo;

        public static IObjectResolver LoaderSceneResolver;
        public static IObjectResolver MenuSceneResolver;
        public static IObjectResolver GameSceneResolver;
        
        //Really Really bad thing
        public static IOutlineService OutlineService;
    }
}