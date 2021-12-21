using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public static float sfx_volume;
    public static float music_volume;
    public static float look_sensitivity;
    [SerializeField]
    float default_value_sfx_volume;
    [SerializeField]
    float default_value_music_volume;
    [SerializeField]
    float default_look_sensitivity;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        sfx_volume = PlayerPrefs.GetFloat("SFX Volume", default_value_sfx_volume);
        music_volume = PlayerPrefs.GetFloat("Music Volume", default_value_music_volume);
        look_sensitivity = PlayerPrefs.GetFloat("Look Sensitivity", default_look_sensitivity);
    }
}
