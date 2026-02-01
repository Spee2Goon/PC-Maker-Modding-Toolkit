namespace PCMaker.Services
{
    public class LoadedPCParts
    {
        public LoadedPCParts(PCPart[] parts) { }

        public PCPart[] Parts;

        public bool TryGetPartBySaveKey(string saveKey, out PCPart part)
        {
            part = null;
            return false;
        }

        public void Dispose() { }
    }
}