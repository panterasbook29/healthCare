using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PatientInfo
{
    public string name;
    public string notes;
}

public class DoctorToPatientConnection : MonoBehaviour
{
    public GameObject PatientShow;
    public GameObject PatientInput;
    public GameObject PatientRegister;
    public Button ExitPatient;
    public TMP_Text PatientName;
    public TMP_Text PatientNotes;

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
            Debug.Log("Error: " + www.error);
        }
        else
        {
            // Parse JSON data
            string jsonData = www.downloadHandler.text;

            // Check for errors or handle the data as needed
            if (jsonData.StartsWith("{\"1:") || jsonData.StartsWith("{\"2:") || jsonData.StartsWith("{\"5:") || jsonData.StartsWith("{\"3:"))
            {
                // Handle server-side errors
                Debug.Log("Server error: " + jsonData);
            }
            else
            {
                // Process the JSON data as needed
                Debug.Log(www.downloadHandler.text);

                // Deserialize JSON data into PatientInfo class
                PatientInfo patientInfo = JsonUtility.FromJson<PatientInfo>(jsonData);

                // Display patient information
                PatientName.text = "Your Patient Is: " + patientInfo.name;
                PatientNotes.text = patientInfo.notes;

                PatientShow.SetActive(true);
                PatientInput.SetActive(false);
                PatientRegister.SetActive(false);
            }
        }
    }

    public void LeavePatientFunction()
    {
        PatientShow.SetActive(false);
        PatientInput.SetActive(true);
        PatientRegister.SetActive(true);
    }
}
