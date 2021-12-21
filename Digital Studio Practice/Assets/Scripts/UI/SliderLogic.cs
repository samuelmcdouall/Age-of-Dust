using UnityEngine;
using UnityEngine.UI;

public class SliderLogic : MonoBehaviour
{
    public Slider sfx_slider;
    public Slider music_slider;
    public Slider sensitivity_slider;
    void OnEnable()
    {
        print("options enabled");
        // todo this gets done twice, need to sort out, move to another script maybe the menu music 
        sfx_slider.value = SettingsManager.sfx_volume;
        music_slider.value = SettingsManager.music_volume;
        sensitivity_slider.value = SettingsManager.look_sensitivity;
    }
    public void OnSFXSliderChange()
    {
        SettingsManager.sfx_volume = sfx_slider.value;
        PlayerPrefs.SetFloat("SFX Volume", sfx_slider.value);
        PlayerPrefs.Save();
    }
    public void OnMusicSliderChange()
    {
        SettingsManager.music_volume = music_slider.value;
        MenuMusic.UpdateMenuMusicVolume(music_slider.value);
        PlayerPrefs.SetFloat("Music Volume", music_slider.value);
        PlayerPrefs.Save();
    }
    public void OnSensitivitySliderChange()
    {
        SettingsManager.look_sensitivity = sensitivity_slider.value;
        PlayerPrefs.SetFloat("Look Sensitivity", sensitivity_slider.value);
        PlayerPrefs.Save();
    }
}
