using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static int orbs_collected;
    public static bool key_collected;
    void Start()
    {
        orbs_collected = 0;
        key_collected = false;
    }
}
