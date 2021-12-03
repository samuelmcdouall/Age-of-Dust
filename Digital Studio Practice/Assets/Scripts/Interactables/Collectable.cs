using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    GameObject player;
    public GameObject interact_UI;
    public AudioClip collect_sfx;
    [SerializeField]
    float player_interaction_threshold;
    [SerializeField]
    CollectableType collectable_type;
    bool player_nearby;

    void Start()
    {
        player_nearby = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        player_nearby = Vector3.Distance(player.transform.position, transform.position) <= player_interaction_threshold;
        if (player_nearby)
        {
            if (!interact_UI.activeSelf)
            {
                interact_UI.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioSource.PlayClipAtPoint(collect_sfx, transform.position, VolumeManager.sfx_volume);
                switch (collectable_type)
                {
                    case CollectableType.orb:
                        InventoryManager.orbs_collected++;
                        print("picked up orb");
                        break;
                    case CollectableType.key:
                        InventoryManager.key_collected = true;
                        print("picked up key");
                        break;
                    default:
                        break;
                }

                Destroy(gameObject);
            }
        }
        else
        {
            if (interact_UI.activeSelf)
            {
                interact_UI.SetActive(false);
            }
        }
    }

    enum CollectableType
    {
        orb,
        key
    }

}
