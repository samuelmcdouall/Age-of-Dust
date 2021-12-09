using UnityEngine;

public class KeySlot : MonoBehaviour
{
    public AudioClip place_sfx;
    public GameObject key_object_placed;
    bool player_nearby;
    bool placed_object;

    void Start()
    {
        player_nearby = false;
        placed_object = false;
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
        if (player_nearby && !placed_object && InventoryManager.key_collected && Input.GetKeyDown(KeyCode.E))
        {
            PlaceKeyInSlot();
            //enabled = false; todo maybe look at using this later on
        }
    }

    void PlaceKeyInSlot()
    {
        if (place_sfx)
        {
            AudioSource.PlayClipAtPoint(place_sfx, transform.position, VolumeManager.sfx_volume);
        }
        key_object_placed.SetActive(true);
        InventoryManager.key_collected = false;
        placed_object = true;
    }
}
