using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interact3 : MonoBehaviour
{
    public Button buton3;
    public notification notif3;

    public void turnon()
    {
        buton3.interactable = false;
        CancelInvoke("spam");
        Invoke("turnoff", 5);
    }
    private void turnoff()
    {
        buton3.interactable = true;
        InvokeRepeating("spam", 0, 1);
    }
    private void spam()
    {
        notif3.sendNotif3();
    }
}
