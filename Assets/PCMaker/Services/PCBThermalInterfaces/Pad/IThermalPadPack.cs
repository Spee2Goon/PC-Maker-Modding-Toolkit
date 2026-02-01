namespace PCMaker.Services
{
    public interface IThermalPadPack
    {
        PCPart GetPart();
        
        PCPart CanYouGiveMeAPadPlease(float width, float length);
        
        
        float Thickness { get; }
        
        float Area { get; }
    }
}