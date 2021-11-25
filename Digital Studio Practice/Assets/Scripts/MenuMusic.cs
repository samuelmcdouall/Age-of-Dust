using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public static AudioSource menu_as;

    void Start()
    {
        menu_as = GetComponent<AudioSource>();
        menu_as.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
        menu_as.volume = VolumeManager.music_volume;
        //todo possible issue here if volume manager is initialized after, it will intially be set to 0 rather than getting the value from player prefs
    }

    public static void UpdateMenuMusicVolume(float value)
    {
        menu_as.volume = value;
    }
}
