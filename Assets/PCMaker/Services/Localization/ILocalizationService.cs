using System;

namespace PCMaker.Services
{
    public interface ILocalizationService
    {
        event Action OnLanguageChange;
        LocalizationLanguageType CurrentLanguage { get; }

        void SetLanguage(LocalizationLanguageType language);
        string GetLocalizedString(string key);
        string GetLocalizedString(string key, LocalizationLanguageType language);
        string GetLocalizedDate(int day, int month, int year);
        string GetLocalizedDateWithTime(int min, int hour, int day, int month, int year);
        string GetLocalizedMinute(int minutes);
        string GetLocalizedHour(int hours);
        string GetLocalizedMonth(int month);
    }
}