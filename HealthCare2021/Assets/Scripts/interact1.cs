using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interact1 : MonoBehaviour
{
    public Button buton1;
    public notification notif1;

    public void turnon()
    {
        buton1.interactable = false;
        CancelInvoke("spam");
        Invoke("turnoff", 5);
    }
    private void turnoff()
    {
        buton1.interactable = true;
        InvokeRepeating("spam", 0, 1);
    }
    private void spam()
    {
        notif1.sendNotif1();
    }
}
