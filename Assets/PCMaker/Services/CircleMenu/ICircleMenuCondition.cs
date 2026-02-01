using System;

namespace PCMaker.Services
{
    public interface ICircleMenuCondition
    {
        bool CanOpenCircleMenu { get; }
        
        event Action UpdateCircleMenuConditions;
    }
}