using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaugaFumat : MonoBehaviour
{
    public GameObject taskFumat;
    public void refreshTaskFumat()
    {
        if(taskFumat.activeSelf == false)
        {
            taskFumat.SetActive(true);
        }
    }
}
