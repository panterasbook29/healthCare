using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TabManager : MonoBehaviour
{
    public GameObject[] panels;
    public void SwitchTab(int tabIndex)
    {
        // Deactivate all panels
        foreach (var panel in panels)
        panel.SetActive(false);
        // Activate the selected panel
        panels[tabIndex].SetActive(true);
    }

    public void LeaveSelectedTab(int tabIndex)
    {
        panels[tabIndex].SetActive(false);
    }
}
