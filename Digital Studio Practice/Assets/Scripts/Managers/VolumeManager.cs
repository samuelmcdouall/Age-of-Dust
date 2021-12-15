using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static float sfx_volume;
    public static float music_volume;
    [SerializeField]
    float default_value;
    void Start()
    {
        // todo make another instance of this in main scene
        DontDestroyOnLoad(gameObject);
        sfx_volume = PlayerPrefs.GetFloat("SFX Volume", default_value);
        music_volume = PlayerPrefs.GetFloat("Music Volume", default_value);
    }
}
