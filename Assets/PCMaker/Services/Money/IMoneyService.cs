using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IMoneyService
    {
        void AddMoney(float dollars, bool notificate = true);
        
        void RemoveMoney(float dollars, bool notificate = true);
        
        void SetCurrency(CurrencyType type);
        
        float ConvertDollars(float dollars);
        
        string GetCurrencySymbol(CurrencyType type);
        
        Sprite GetCurrencyIcon(CurrencyType type);
        
        string GetCurrentCurrencySymbol();
        
        Sprite GetCurrentCurrencyIcon();
        
        float GetMoneyPrice();
        
        string FormatMoneyToCorrectString(string rawString);
        
        
        float CurrentDollarsBalance { get; }
        
        float CurrentConvertedBalance { get; }
        
        CurrencyType CurrentCurrencyType { get; }
        
        event Action<float> OnBalanceChange;
    }
}