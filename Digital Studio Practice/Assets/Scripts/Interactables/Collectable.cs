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
    public OrbCollectionUI orb_ui_script;

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
                PickupCollectable();
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

    void PickupCollectable()
    {
        if (collect_sfx)
        {
            AudioSource.PlayClipAtPoint(collect_sfx, transform.position, SettingsManager.sfx_volume);
        }
        switch (collectable_type)
        {
            case CollectableType.orb:
                InventoryManager.orbs_collected++;
                orb_ui_script.UpdateOrbCollectionText(InventoryManager.orbs_collected);
                orb_ui_script.DisplayAnimatedUI();
                break;
            case CollectableType.key:
                InventoryManager.key_collected = true;
                break;
            default:
                break;
        }
        interact_UI.SetActive(false);
        Destroy(gameObject);
    }

    enum CollectableType
    {
        orb,
        key
    }

}
