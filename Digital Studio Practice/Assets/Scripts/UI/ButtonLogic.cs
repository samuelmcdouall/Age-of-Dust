using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menu_canvas;
    public GameObject options_canvas;
    public GameObject controls_canvas;

    [Header("SFX")]
    public AudioClip click_sfx;
    public GameObject audio_listener;

    public void ClickNewGameButton()
    {
        SceneManager.LoadScene("Main Scene");
        AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, SettingsManager.sfx_volume);
    }
    public void ClickOptionsButton()
    {
        menu_canvas.SetActive(false);
        options_canvas.SetActive(true);
        PlayClickSFX();
    }
    public void ClickControlsButton()
    {
        options_canvas.SetActive(false);
        controls_canvas.SetActive(true);
        PlayClickSFX();
    }
    public void ClickExitButton()
    {
        PlayClickSFX();
        Application.Quit();
    }
    public void ClickOptionsBackButton()
    {
        options_canvas.SetActive(false);
        menu_canvas.SetActive(true);
        PlayClickSFX();
    }
    public void ClickControlsBackButton()
    {
        controls_canvas.SetActive(false);
        options_canvas.SetActive(true);
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
            // Work around to allow Unity to play a sound effect whilst timescale is set to 0
            Time.timeScale = 1.0f;
            AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, SettingsManager.sfx_volume);
            Time.timeScale = 0.0f;
        }
        else
        {
            AudioSource.PlayClipAtPoint(click_sfx, audio_listener.transform.position, SettingsManager.sfx_volume);
        }
    }
}
