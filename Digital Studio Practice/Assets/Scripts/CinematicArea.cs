using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicArea : MonoBehaviour
{
    public GameObject cinematic_camera;
    public GameObject cinematic_area_point;
    public GameObject regular_area_point;
    public AudioClip entrance_audioclip;
    public AudioClip area_audioclip;
    bool played_entrance_audioclip;
    GameObject player_camera;
    SoundManager sound_manager_script;

    private void Start()
    {
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
        sound_manager_script = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        played_entrance_audioclip = false;
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

                
                if (sound_manager_script.player_as.clip.name != sound_manager_script.main_game_music.name)
                {
                    //print("playing main music");
                    sound_manager_script.PlayMainMusic();
                }
            }
            else
            {
                Player.last_camera_tr = Camera.main.transform;
                CameraManager.DisableAllEnabledCameras();
                CameraManager.EnableCamera(cinematic_camera);
                if (!played_entrance_audioclip)
                {
                    //print("playing area entrance clip for the first and only time");
                    sound_manager_script.PlayAreaClip(entrance_audioclip);
                    played_entrance_audioclip = true;
                    Invoke("PlayAreaClipAfterEntrance", entrance_audioclip.length + 1.0f);
                }
                else
                {
                    if (sound_manager_script.player_as.clip.name != area_audioclip.name)
                    {
                        //print("playing area ambient sound now on returning to area");
                        sound_manager_script.PlayAreaClip(area_audioclip);
                    }
                }
            }
        }
    }

    void PlayAreaClipAfterEntrance()
    {
        if (sound_manager_script.player_as.clip.name != sound_manager_script.main_game_music.name)
        {
            //print("playing area ambient sound now after entrance");
            sound_manager_script.PlayAreaClip(area_audioclip);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

        }
    }
}
