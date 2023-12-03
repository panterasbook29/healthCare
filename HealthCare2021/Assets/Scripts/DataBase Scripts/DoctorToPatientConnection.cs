using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class PatientInfo
{
    public string patientName;
}

public class DoctorToPatientConnection : MonoBehaviour
{

    public TMP_InputField patientNameInput;


    public void CallFindPatient()
    {
        StartCoroutine(FindPatient());
    }

    IEnumerator FindPatient()
    {
        WWWForm form = new WWWForm();
        form.AddField("patient_name", patientNameInput.text);
        form.AddField("doctor_id", DoctorAccountManager.globalDoctorID);

        UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/findPatient.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse JSON data
            string jsonData = www.downloadHandler.text;

            // Check for errors or handle the data as needed
            if (jsonData.StartsWith("1:") || jsonData.StartsWith("2:") || jsonData.StartsWith("5:") || jsonData.StartsWith("6:"))
            {
                // Handle server-side errors
                Debug.LogError("Server error: " + jsonData);
            }
            else
            {
                // Process the JSON data as needed
               // PatientInfo patientInfo = JsonUtility.FromJson<PatientInfo>(jsonData);
                Debug.Log(www.downloadHandler.text);

            }
        }
    }

}
