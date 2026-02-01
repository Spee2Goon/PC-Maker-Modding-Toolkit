namespace PCMaker.Services
{
    public interface IThermalPad
    {
        PCPart GetPart();
        
        void SetStatus(float status);
        
        
        float Thickness { get; }
        
        float Status { get; }
        
        float Width { get; }
        
        float Length { get; }
    }
}