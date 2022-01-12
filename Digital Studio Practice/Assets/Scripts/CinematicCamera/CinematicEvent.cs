using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEvent : MonoBehaviour
{
    [SerializeField]
    float cinematic_camera_time;
    public GameObject cinematic_camera;
    bool triggered_cinematic_event;
    GameObject player_camera;

    void Start()
    {
        triggered_cinematic_event = false;
    }

    void ChangeCameraBackToPlayer()
    {
        CameraManager.DisableAllEnabledCameras();
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraManager.EnableCamera(player_camera);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered_cinematic_event && other.gameObject.CompareTag("Player"))
        {
            triggered_cinematic_event = true;
            Player.last_camera_tr = Camera.main.transform;
            CameraManager.DisableAllEnabledCameras();
            CameraManager.EnableCamera(cinematic_camera);
            Invoke("ChangeCameraBackToPlayer", cinematic_camera_time);
        }
    }
}
