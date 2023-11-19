using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactFumat : MonoBehaviour
{
    public Button butonFumat;
    public notification notifFumat;

    public void turnon()
    {
        butonFumat.interactable = false;
        CancelInvoke("spam");
        Invoke("turnoff", 5);
    }
    private void turnoff()
    {
        butonFumat.interactable = true;
        InvokeRepeating("spam", 0, 1);
    }
    private void spam()
    {
        notifFumat.sendNotifFumat();
    }
}
