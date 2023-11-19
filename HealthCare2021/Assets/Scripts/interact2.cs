using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interact2 : MonoBehaviour
{
    public Button buton2;
    public notification notif2;

    public void turnon()
    {
        buton2.interactable = false;
        CancelInvoke("spam");
        Invoke("turnoff", 5);
    }
    private void turnoff()
    {
        buton2.interactable = true;
        InvokeRepeating("spam", 0, 1);
    }
    private void spam()
    {
        notif2.sendNotif2();
    }
}
