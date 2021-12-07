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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_rotation = GameObject.FindGameObjectWithTag("PlayerRotation");
        triggered = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!triggered && collider.gameObject.tag == "Player")
        {
            fx.SetActive(true);
            AudioSource.PlayClipAtPoint(sfx, transform.position, VolumeManager.sfx_volume);
            triggered = true;
            if (object_player_faces)
            {
                player.GetComponent<Player>().DisableControlsForSeconds(disable_control_period);
                player.GetComponent<Player>().player_ani.SetBool(player.GetComponent<Player>().running_animation, false);
                player_rotation.GetComponent<PlayerRotation>().LookAtTargetForSeconds(object_player_faces, disable_control_period);
            }
        }
    }
}
