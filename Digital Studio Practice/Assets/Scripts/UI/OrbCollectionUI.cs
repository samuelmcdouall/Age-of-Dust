using UnityEngine;

public class OrbCollectionUI : DisplayUI
{
    [SerializeField]
    int total_orb_count;

    public void UpdateOrbCollectionText(int orbs_collected)
    {
        ui_text.text = orbs_collected + "/" + total_orb_count;
    }
}
