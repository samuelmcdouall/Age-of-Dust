using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool is_grounded;
    void OnTriggerStay(Collider collider)
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

    void OnTriggerExit(Collider collider)
    {
        is_grounded = false;
    }

}
