using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip collect_sfx;
    [SerializeField]
    CollectableType collectable_type;
    bool player_nearby;

    void Start()
    {
        player_nearby = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player_nearby = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player_nearby = false;
        }
    }

    void Update()
    {
        if (player_nearby && Input.GetKeyDown(KeyCode.E))
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

    enum CollectableType
    {
        orb,
        key
    }

}
