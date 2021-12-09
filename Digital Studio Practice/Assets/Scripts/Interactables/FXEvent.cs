using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXEvent : MonoBehaviour
{
    GameObject player;
    GameObject player_rotation;
    public GameObject fx;
    public AudioClip sfx;
    bool triggered;

    public GameObject object_player_faces;
    [SerializeField]
    float disable_control_period;
    [SerializeField]
    float kneel_period;
    public DisplayUI display_ui_script;
    float kneel_down_clip_time_offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_rotation = GameObject.FindGameObjectWithTag("PlayerRotation");
        kneel_down_clip_time_offset = 4.0f;
        triggered = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!triggered && collider.gameObject.tag == "Player")
        {
            //todo see if there are RET_IF macro equivalent 
            if (fx)
            {
                fx.SetActive(true);
            }
            if (sfx)
            {
                AudioSource.PlayClipAtPoint(sfx, transform.position, VolumeManager.sfx_volume);
            }
            triggered = true;
            if (object_player_faces)
            {
                player.GetComponent<Player>().InspectAnimation(disable_control_period, kneel_period + kneel_down_clip_time_offset);
                display_ui_script.DisplayAnimatedUI();
                player_rotation.GetComponent<PlayerRotation>().LookAtTargetForSeconds(object_player_faces, disable_control_period);
            }
        }
    }
}
