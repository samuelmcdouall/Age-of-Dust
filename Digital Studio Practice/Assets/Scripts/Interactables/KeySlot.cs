using UnityEngine;

public class KeySlot : MonoBehaviour
{
    public AudioClip place_sfx;
    public GameObject key_object_placed;
    public GameObject[] animation_triggered_objects;
    bool player_nearby;
    bool placed_object;
    [SerializeField]
    float cinematic_camera_time;
    public GameObject cinematic_camera;
    GameObject player_camera;

    void Start()
    {
        player_camera = GameObject.FindGameObjectWithTag("MainCamera");
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
            AudioSource.PlayClipAtPoint(place_sfx, transform.position, SettingsManager.sfx_volume);
        }
        AnimateSelectedObjects();
        Player.last_camera_tr = Camera.main.transform;
        CameraManager.DisableAllEnabledCameras();
        CameraManager.EnableCamera(cinematic_camera);
        Invoke("ChangeCameraBackToPlayer", cinematic_camera_time);
        key_object_placed.SetActive(true);
        InventoryManager.key_collected = false;
        placed_object = true;
    }
    void AnimateSelectedObjects()
    {
        foreach (GameObject obj in animation_triggered_objects)
        {
            obj.GetComponent<Animator>().SetTrigger("key_placed_in_slot");
        }
    }
    void ChangeCameraBackToPlayer()
    {
        CameraManager.DisableAllEnabledCameras();
        CameraManager.EnableCamera(player_camera);
    }
}
