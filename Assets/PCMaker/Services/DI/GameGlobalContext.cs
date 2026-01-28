using VContainer;

namespace PCMaker.Services
{
    public static class GameGlobalContext
    {
        public static bool isAssetsLoaded = false;
        public static bool RequreMenuTransition = true;

        //Save info
        public static SaveFileInfo CurrentSaveFileInfo;

        public static IObjectResolver LoaderSceneResolver;
        public static IObjectResolver MenuSceneResolver;
        public static IObjectResolver GameSceneResolver;
    }
}