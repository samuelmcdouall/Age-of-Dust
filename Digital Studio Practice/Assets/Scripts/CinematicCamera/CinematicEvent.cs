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
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        triggered_cinematic_event = false;
    }

    void ChangeCameraBackToPlayer()
    {
        CameraManager.DisableAllEnabledCameras();
        CameraManager.EnableCamera(player_camera);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered_cinematic_event)
        {
            triggered_cinematic_event = true;
            Player.last_camera_tr = Camera.main.transform;
            CameraManager.DisableAllEnabledCameras();
            CameraManager.EnableCamera(cinematic_camera);
            Invoke("ChangeCameraBackToPlayer", cinematic_camera_time);
        }
    }
}
