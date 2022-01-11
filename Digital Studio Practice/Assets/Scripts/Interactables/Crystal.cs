using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    // General
    GameObject player;
    bool crystal_fixed;

    [Header("Interaction")]
    [SerializeField]
    float player_interaction_threshold;
    public GameObject interact_UI;
    public GameObject interact_fx;
    public AudioClip[] sfx_clips;
    public float[] post_sfx_delays;
    public GameObject[] animation_triggered_objects;
    GameObject player_rotation;
    public GameObject object_player_faces;
    public GameObject timeline_object;
    [SerializeField]
    float disable_control_period;

    [Header("Corruption Mechanics")]
    public Material corrupted_material;
    public Material fixed_material;
    public GameObject[] corrupted_objects;
    Material[] original_materials;

    [Header("Broken Rotation")]
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_y_broken;
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_x_broken;

    [Header("Fixed Rotation")]
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_y_fixed;
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_x_fixed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_rotation = GameObject.FindGameObjectWithTag("PlayerRotation");
        crystal_fixed = false;
        CorruptObjects();
    }

    void Update()
    {
        if (!crystal_fixed && Vector3.Distance(player.transform.position, transform.position) <= player_interaction_threshold)
        {
            if (!interact_UI.activeSelf)
            {
                interact_UI.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithCrystal();
            }
        }
        else
        {
            if (interact_UI.activeSelf)
            {
                interact_UI.SetActive(false);
            }
        }
        RotateCrystal();
    }

    void CorruptObjects()
    {
        original_materials = new Material[corrupted_objects.Length];
        for (int count = 0; count < corrupted_objects.Length; count++)
        {
            original_materials[count] = corrupted_objects[count].GetComponent<Renderer>().material;
            corrupted_objects[count].GetComponent<Renderer>().material = corrupted_material;
        }
    }

    void InteractWithCrystal()
    {
        crystal_fixed = true;
        if (interact_fx)
        {
            Instantiate(interact_fx, transform.position, Quaternion.identity);
        }
        StartCoroutine(PlayAllSFXClips());
        AnimateSelectedObjects();
        FixMaterials();
        if (object_player_faces)
        {
            TurnPlayerToObject();
        }
        if (timeline_object)
        {
            timeline_object.SetActive(true);
        }
    }

    IEnumerator PlayAllSFXClips()
    {
        for (int count = 0; count < sfx_clips.Length; count++)
        {
            AudioSource.PlayClipAtPoint(sfx_clips[count], transform.position, SettingsManager.sfx_volume);
            yield return new WaitForSeconds(post_sfx_delays[count]);
        }
    }

    void AnimateSelectedObjects()
    {
        foreach (GameObject obj in animation_triggered_objects)
        {
            obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
        }
    }

    void FixMaterials()
    {
        GetComponent<Renderer>().material = fixed_material;
        Invoke("ChangeMaterialsBackToOriginal", 1.0f);
    }

    void ChangeMaterialsBackToOriginal()
    {
        for (int count = 0; count < corrupted_objects.Length; count++)
        {
            corrupted_objects[count].GetComponent<Renderer>().material = original_materials[count];
        }
    }

    void TurnPlayerToObject()
    {
        player.GetComponent<Player>().DisableControlsForSeconds(disable_control_period);
        player.GetComponent<Player>().player_ani.SetBool(player.GetComponent<Player>().running_animation, false);
        player_rotation.GetComponent<PlayerRotation>().LookAtTargetForSeconds(object_player_faces, disable_control_period);
    }

    void RotateCrystal()
    {
        if (!crystal_fixed)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 360.0f / revolution_time_y_broken);
            transform.Rotate(Vector3.right, Time.deltaTime * 360.0f / revolution_time_x_broken);
        }
        else
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 360.0f / revolution_time_y_fixed);
            transform.Rotate(Vector3.right, Time.deltaTime * 360.0f / revolution_time_x_fixed);
        }
    }
}
