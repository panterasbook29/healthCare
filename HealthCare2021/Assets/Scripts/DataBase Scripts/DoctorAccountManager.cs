using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DoctorInfoAccount
{
    public string name; // Ensure this matches the field name in the JSON
    public string doctorID;   // Ensure this matches the field name in the JSON
}

public class DoctorAccountManager : MonoBehaviour
{
    public static string globalDoctorID;

    public TMP_Text name_TXT;
    public TMP_Text doctorID_TXT;

    private void Start()
    {
        StartCoroutine(GetDoctorInfo());
    }

    IEnumerator GetDoctorInfo()
    {

        if (string.IsNullOrEmpty(AuthManagerDoctors.globalDoctorCNP))
        {
            Debug.LogError("Global Doctor CNP is null or empty");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("cnp", AuthManagerDoctors.globalDoctorCNP, System.Text.Encoding.UTF8);

        UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/getDoctorInfo.php", form);
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
                DoctorInfoAccount doctorInfo = JsonUtility.FromJson<DoctorInfoAccount>(jsonData);

                // Now 'userInfo' contains information about the user, including the name and patientID
                name_TXT.text = "Name: " + doctorInfo.name;
                doctorID_TXT.text = "Doctor ID: " + doctorInfo.doctorID;
                globalDoctorID = doctorInfo.doctorID;
                // Use the userInfo.name and userInfo.patientID variables in your Unity application
            }
        }
    }
}
