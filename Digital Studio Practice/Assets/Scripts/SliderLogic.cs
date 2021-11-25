using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLogic : MonoBehaviour
{
    public Slider sfx_slider;
    public Slider music_slider;
    void OnEnable()
    {
        print("options enabled");
        // todo this gets done twice, need to sort out
        sfx_slider.value = VolumeManager.sfx_volume;
        music_slider.value = VolumeManager.music_volume;
    }
    public void OnSFXSliderChange()
    {
        VolumeManager.sfx_volume = sfx_slider.value;
        PlayerPrefs.SetFloat("SFX Volume", sfx_slider.value);
        PlayerPrefs.Save();
    }
    public void OnMusicSliderChange()
    {
        VolumeManager.music_volume = music_slider.value;
        MenuMusic.UpdateMenuMusicVolume(music_slider.value);
        PlayerPrefs.SetFloat("Music Volume", music_slider.value);
        PlayerPrefs.Save();
    }
}
