namespace PCMaker.Services
{
    public interface IThermalPad
    {
        float Thickness { get; }
        float Status { get; }
        float Width { get; }
        float Length { get; }
        
        PCPart GetPart();
        void SetStatus(float status);
    }
}