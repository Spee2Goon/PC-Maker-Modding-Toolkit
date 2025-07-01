namespace PCMaker.ModAPI
{
    public interface IMod
    {
        void Initialize(ModBehaviour mod);
        bool CanBeDisabled();
    }

    public interface IAwakeableMod
    {
        void Awake();
    }

    public interface IStartableMod
    {
        void Start();
    }

    public interface IUpdatableMod
    {
        void Update();
    }

    public interface IFixedUpdatableMod
    {
        void FixedUpdate();
    }

    public interface ILateUpdatableMod
    {
        void LateUpdate();
    }

    public interface IWorkOnMainMenuMod
    {
        void OnLoadOnMainMenu();
        void OnUnloadFromMainMenu();
    }

    public interface IWorkOnGameplayMod
    {
        void OnLoadOnGameScene();
        void OnUnloadFromGameScene();
        void OnPauseEnabled();
        void OnPauseDisabled();
    }
}