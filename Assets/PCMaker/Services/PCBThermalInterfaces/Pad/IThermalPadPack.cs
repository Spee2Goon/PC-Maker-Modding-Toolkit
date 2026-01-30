namespace PCMaker.Services
{
    public interface IThermalPadPack
    {
        float Thickness { get; }
        float Area { get; }
        
        PCPart GetPart();
        PCPart CanYouGiveMeAPadPlease(float width, float length);
    }
}