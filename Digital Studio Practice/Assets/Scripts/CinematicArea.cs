using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicArea : MonoBehaviour
{
    public GameObject cinematic_camera;
    public GameObject cinematic_area_point;
    public GameObject regular_area_point;
    public AudioClip cinematic_audioclip;
    bool played_audioclip;
    GameObject player_camera;
    SoundManager sound_manager_script;

    private void Start()
    {
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        sound_manager_script = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        played_audioclip = false;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (!played_audioclip)
            {
                print("go to sound manager and play new clip");
                sound_manager_script.PlayNewClip(cinematic_audioclip);
                played_audioclip = true;
            }
        }
    }
}
