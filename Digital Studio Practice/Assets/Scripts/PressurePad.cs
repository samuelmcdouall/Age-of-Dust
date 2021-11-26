using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject fx;
    public AudioClip sfx;
    public GameObject[] triggered_objects;

    Animator pressure_pad_ani;
    bool pressed;
    
    // Start is called before the first frame update
    void Start()
    {
        pressure_pad_ani = GetComponent<Animator>();
        pressed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.gameObject.CompareTag("Player"))
        {
            fx.SetActive(true);
            AudioSource.PlayClipAtPoint(sfx, transform.position, VolumeManager.sfx_volume);
            pressure_pad_ani.SetBool("IsPadActivated", true);

            foreach(GameObject obj in triggered_objects)
            {
                obj.GetComponent<Animator>().SetTrigger("pressure_pad_pressed");
            }

            pressed = true;
        }
    }
}
