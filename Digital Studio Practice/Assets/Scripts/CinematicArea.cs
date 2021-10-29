using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicArea : MonoBehaviour
{
    public GameObject cinematic_camera;
    public GameObject cinematic_area_point;
    public GameObject regular_area_point;
    GameObject player_camera;

    private void Start()
    {
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float distance_from_player_to_regular_point = Vector3.Distance(collider.gameObject.transform.position, regular_area_point.transform.position);
            float distance_from_player_to_cinematic_point = Vector3.Distance(collider.gameObject.transform.position, cinematic_area_point.transform.position);
            if (distance_from_player_to_regular_point <= distance_from_player_to_cinematic_point)
            {
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera(player_camera);
            }
            else
            {
                Player.last_camera_tr = Camera.main.transform;
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera(cinematic_camera);
            }
        }
    }
}
