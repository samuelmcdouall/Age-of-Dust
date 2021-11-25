using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public GameObject menu_canvas;
    public GameObject options_canvas;
    public AudioClip click_sfx;
    public GameObject audio_listener;
    void Start()
    {
        //GetComponent<AudioSource>().volume = VolumeManager.sfx_volume;
    }

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
        PlayClickSFX();
    }
    public void ClickExitToMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu Scene");
        PlayClickSFX();
    }


    private void PlayClickSFX()
    {
        if (Time.timeScale == 0.0f)
        {
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
