using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pause_menu;
    public GameObject options_menu;
    public GameObject controls_menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controls_menu.activeSelf)
            {
                controls_menu.SetActive(false);
                options_menu.SetActive(true);
            }
            else if (options_menu.activeSelf)
            {
                options_menu.SetActive(false);
                pause_menu.SetActive(true);
            }
            else if (!pause_menu.activeSelf)
            {
                pause_menu.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.visible = true;
            }
            else
            {
                pause_menu.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
            }
        }
    }
}
