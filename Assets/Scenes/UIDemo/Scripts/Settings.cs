using System;
using UnityEngine;
using UnityEngine.Events;


public class Settings : Singleton<Settings>
{
    public void VibrationEnabled(bool value)
    {
        PlayerPrefs.SetInt("VibrationEnabled", (value ? 1 : 0));

    }

    public void MusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SoundsVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundsVolume", value);
    }


    public void SelectedLanguage(int value)
    {
        PlayerPrefs.SetInt("SelectedLanguage", value);
    }
}