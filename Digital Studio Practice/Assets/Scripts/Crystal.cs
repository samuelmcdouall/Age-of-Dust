using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    GameObject player;
    public GameObject interact_fx;
    public AudioClip interact_sfx;
    public Material fixed_material;


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
}
