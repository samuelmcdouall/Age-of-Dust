using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool is_grounded;
    private void OnTriggerStay(Collider collider)
    {
        if (collider != null)
        {
            is_grounded = true;
        }
        else
        {
            is_grounded = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        is_grounded = false;
    }

}
