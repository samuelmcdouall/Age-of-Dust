using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    GameObject player;
    public GameObject interact_fx;
    public AudioClip interact_sfx;
    public Material corrupted_material;
    public Material fixed_material;

    public GameObject[] corrupted_objects;
    Material[] original_materials;
    public GameObject[] animation_triggered_objects;


    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_y_broken;
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_x_broken;

    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_y_fixed;
    [SerializeField]
    [Range(0.1f, 60.0f)]
    float revolution_time_x_fixed;

    bool crystal_fixed;
    [SerializeField]
    float player_interaction_threshold;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crystal_fixed = false;
        original_materials = new Material[corrupted_objects.Length];
        for(int count = 0; count < corrupted_objects.Length; count++)
        {
            original_materials[count] = corrupted_objects[count].GetComponent<Renderer>().material;
            corrupted_objects[count].GetComponent<Renderer>().material = corrupted_material;
        }
        
    }
    void Update()
    {
        if (!crystal_fixed && Vector3.Distance(player.transform.position, transform.position) <= player_interaction_threshold)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                crystal_fixed = true;
                Instantiate(interact_fx, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(interact_sfx, transform.position);
                GetComponent<Renderer>().material = fixed_material;
                foreach (GameObject obj in animation_triggered_objects)
                {
                    obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
                }
                Invoke("ChangeMaterialsBackToOriginal", 1.0f);
            }
        }
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

    void ChangeMaterialsBackToOriginal()
    {
        for (int count = 0; count < corrupted_objects.Length; count++)
        {
            corrupted_objects[count].GetComponent<Renderer>().material = original_materials[count];
        }
    }
}
