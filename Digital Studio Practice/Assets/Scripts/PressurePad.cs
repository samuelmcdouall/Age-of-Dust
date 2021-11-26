using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    //public Audio Source Needed
    //public GameObject particle start/enable?

    Animator thisAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisAnimator.SetBool("IsPadActivated", true);
        }
    }
}
