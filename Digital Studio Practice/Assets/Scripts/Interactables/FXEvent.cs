using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXEvent : MonoBehaviour
{
    public GameObject fx;
    public AudioClip sfx;
    bool triggered;

    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!triggered && collider.gameObject.tag == "Player")
        {
            fx.SetActive(true);
            AudioSource.PlayClipAtPoint(sfx, transform.position, VolumeManager.sfx_volume);
            triggered = true;
        }
    }
}
