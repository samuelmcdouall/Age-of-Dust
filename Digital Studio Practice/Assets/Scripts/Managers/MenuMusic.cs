using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public static AudioSource menu_as;

    void Start()
    {
        menu_as = GetComponent<AudioSource>();
        menu_as.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;
        menu_as.volume = SettingsManager.music_volume;
        Cursor.visible = true;
    }

    public static void UpdateMenuMusicVolume(float value)
    {
        menu_as.volume = value;
    }
}
