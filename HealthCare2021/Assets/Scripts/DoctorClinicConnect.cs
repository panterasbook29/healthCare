using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DoctorClinicConnect : MonoBehaviour
{
    public TMP_InputField doctorCodeInput;
    private string phpScriptURL = "http://localhost/sqlconnect/login.php";

    public void searchDoctor()
    {
        StartCoroutine(getDoctor());
    }

    IEnumerator getDoctor()
    {
        WWWForm form = new WWWForm();
        form.AddField("doctorCode", doctorCodeInput.text);


        UnityWebRequest www = UnityWebRequest.Get(phpScriptURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse JSON data
            string jsonData = www.downloadHandler.text;
            Debug.Log(jsonData);

            // Process the JSON data as needed
        }
    }
}
