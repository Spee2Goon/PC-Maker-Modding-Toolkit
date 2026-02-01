namespace PCMaker.Services
{
    public interface IPlayerCondition
    {
        bool PlayerCanMove { get; }
        bool PlayerCanShowBody { get; }
        bool PlayerCanMoveCamera { get; }
        bool PlayerCanZoom { get; }
        bool PlayerCanCrouch { get; }
        bool PlayerCanUpdateActions { get; }
    }
}