using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField]
    float delay_after_object_activation;
    void Start()
    {
        Invoke("LoadMainMenu", delay_after_object_activation);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }

}
