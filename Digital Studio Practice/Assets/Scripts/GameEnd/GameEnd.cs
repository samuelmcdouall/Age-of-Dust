using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Menu Scene");
    }

}
