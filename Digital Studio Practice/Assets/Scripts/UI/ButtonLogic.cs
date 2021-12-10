using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menu_canvas;
    public GameObject options_canvas;

    [Header("SFX")]
    public AudioClip click_sfx;
    public GameObject audio_listener;

    public void ClickNewGameButton()
    {
        SceneManager.LoadScene("Main Scene");
        AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, VolumeManager.sfx_volume);
    }
    public void ClickOptionsButton()
    {
        menu_canvas.SetActive(false);
        options_canvas.SetActive(true);
        PlayClickSFX();

    }
    public void ClickExitButton()
    {
        // todo remove this after testing
        print("QUIT GAME (remove this after testing)");
        PlayClickSFX();
        Application.Quit();
    }
    public void ClickOptionsBackButton()
    {
        menu_canvas.SetActive(true);
        options_canvas.SetActive(false);
        PlayClickSFX();
    }
    public void ClickResumeButton()
    {
        menu_canvas.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        PlayClickSFX();
    }
    public void ClickExitToMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu Scene");
        PlayClickSFX();
    }

    void PlayClickSFX()
    {
        if (Time.timeScale == 0.0f)
        {
            // todo work around comment because unity is annoying
            Time.timeScale = 1.0f;
            AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, VolumeManager.sfx_volume);
            Time.timeScale = 0.0f;
        }
        else
        {
            AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, VolumeManager.sfx_volume);
        }
    }
}
