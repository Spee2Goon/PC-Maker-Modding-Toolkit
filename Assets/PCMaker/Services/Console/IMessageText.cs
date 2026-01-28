using UnityEngine;

namespace PCMaker.Services
{
    public interface IMessageText
    {
        void Setup(string text, int fontSize, int textIndex, RectTransform ViewPortRectTransform);
        void DestroyTextObject();
        float GetHeight(bool allowPerformanceOptimization);
    }
}