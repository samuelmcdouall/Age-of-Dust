using System.Collections.Generic;
using UnityEngine;

public class PressurePadPuzzleManager : MonoBehaviour
{
    [Header("Pressure Pads")]
    public GameObject[] pressure_pads;
    public GameObject[] animation_triggered_objects;
    public AudioClip completion_sfx;
    [System.NonSerialized]
    public bool incorrect_combination;
    List<int> combination;
    [SerializeField]
    float cinematic_camera_time;
    public GameObject cinematic_camera;
    GameObject player_camera;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        incorrect_combination = false;
        combination = new List<int>();
    }

    public void PressurePadPressed(GameObject pressed_pad)
    {
        for (int count = 0; count < pressure_pads.Length; count++)
        {
            if (ReferenceEquals(pressed_pad, pressure_pads[count]))
            {
                combination.Add(count);
                EvaluateCurrentPuzzleCode();
                break;
            }
        }
    }
    void EvaluateCurrentPuzzleCode()
    {
        if (combination.Count == pressure_pads.Length)
        {
            for (int count = 0; count < pressure_pads.Length; count++)
            {
                if (combination[count] != count)
                {
                    incorrect_combination = true;
                    break;
                }
            }
            if (!incorrect_combination)
            {
                PuzzleComplete();
            }
        }
    }

    void PuzzleComplete()
    {
        AudioSource.PlayClipAtPoint(completion_sfx, player.transform.position, SettingsManager.sfx_volume);
        foreach (GameObject obj in animation_triggered_objects)
        {
            obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
        }
        Player.last_camera_tr = Camera.main.transform;
        CameraManager.DisableAllEnabledCameras();
        CameraManager.EnableCamera(cinematic_camera);
        Invoke("ChangeCameraBackToPlayer", cinematic_camera_time);
    }

    public void ResetPuzzle()
    {
        combination.Clear();
        incorrect_combination = false;
        foreach (GameObject pad in pressure_pads)
        {
            pad.GetComponent<PressurePad>().ResetPad();
        }
    }

    void ChangeCameraBackToPlayer()
    {
        CameraManager.DisableAllEnabledCameras();
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraManager.EnableCamera(player_camera);
    }
}
