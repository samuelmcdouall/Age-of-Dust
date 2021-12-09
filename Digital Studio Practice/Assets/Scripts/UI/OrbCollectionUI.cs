using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbCollectionUI : DisplayUI
{
    [SerializeField]
    int total_orb_count;

    public void UpdateOrbCollectionText(int orbs_collected)
    {
        ui_text.text = "Orbs Collected" + "\n" + orbs_collected + "/" + total_orb_count;
    }
}
