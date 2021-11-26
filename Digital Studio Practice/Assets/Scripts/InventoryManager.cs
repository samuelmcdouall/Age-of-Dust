using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static int orbs_collected;
    public static bool key_collected; // todo could change to int if we can have multiple keys at same time
    void Start()
    {
        orbs_collected = 0;
        key_collected = false;
    }
}
