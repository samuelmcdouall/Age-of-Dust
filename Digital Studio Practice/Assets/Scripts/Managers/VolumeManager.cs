using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static float sfx_volume;
    public static float music_volume;
    void Start()
    {
        // todo make another instance of this in main scene
        DontDestroyOnLoad(gameObject);
        sfx_volume = PlayerPrefs.GetFloat("SFX Volume", 0.5f);
        music_volume = PlayerPrefs.GetFloat("Music Volume", 0.5f);
    }
}
