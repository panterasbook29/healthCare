using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class DoctorInfo
{
    public string code;
    public string name;
    public string specialty;
    public string clinic;
}

[System.Serializable]
public class ClinicInfo
{
    public string code;
    public string name;
}

public class DoctorClinicConnect : MonoBehaviour
{
    public TMP_InputField doctorCodeInput;
    public TMP_InputField clinicCodeInput;
    private string doctorScriptURL = "http://localhost/sqlconnect/doctors.php";
    private string clinicScriptURL = "http://localhost/sqlconnect/clinics.php";

    private bool hasClinic = false;
    private string currentClinic = "";
    public void SearchClinic()
    {
        StartCoroutine(GetClinic());
    }

    IEnumerator GetClinic()
    {
        WWWForm form = new WWWForm();
        form.AddField("clinicCode", clinicCodeInput.text);

        UnityWebRequest www = UnityWebRequest.Post(clinicScriptURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse JSON data
            string jsonData = www.downloadHandler.text;
            // Debug.Log(jsonData);

            // Check for errors or handle the data as needed
            if (jsonData.StartsWith("1:") || jsonData.StartsWith("2:") || jsonData.StartsWith("5:"))
            {
                // Handle server-side errors
                Debug.LogError("Server error: " + jsonData);
            }
            else
            {
                hasClinic = true;

                // Process the JSON data as needed
                ClinicInfo clinic = JsonUtility.FromJson<ClinicInfo>(jsonData);

                // Now 'doctor' contains information about the doctor with the given code
                Debug.Log($"{clinic.name}");

                currentClinic = ($"{clinic.name}");
                getClinic(currentClinic);
            }
        }
    }

    public GameObject clinicLogIn;
    public GameObject clinicShow;
    public TMP_Text clinic_name;

    public void getClinic(string clinicName)
    {
        clinicLogIn.SetActive(false);
        clinicShow.SetActive(true);
        clinic_name.text = "Your clinic is: " + clinicName;
    }

    public void leaveClinic()
    {
        clinicLogIn.SetActive(true);
        clinicShow.SetActive(false);
        currentClinic = "";
        hasClinic = false;
        removeDoctors();
    }

    public void SearchDoctor()
    {
        StartCoroutine(GetDoctor());
    }

    IEnumerator GetDoctor()
    {
        if (hasClinic == true)
        {
            WWWForm form = new WWWForm();
            form.AddField("doctorCode", doctorCodeInput.text);

            UnityWebRequest www = UnityWebRequest.Post(doctorScriptURL, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Parse JSON data
                string jsonData = www.downloadHandler.text;
                // Debug.Log(jsonData);

                // Check for errors or handle the data as needed
                if (jsonData.StartsWith("1:") || jsonData.StartsWith("2:") || jsonData.StartsWith("5:"))
                {
                    // Handle server-side errors
                    Debug.LogError("Server error: " + jsonData);
                }
                else
                {
                    // Process the JSON data as needed
                    DoctorInfo doctor = JsonUtility.FromJson<DoctorInfo>(jsonData);

                    // Now 'doctor' contains information about the doctor with the given code
                    // Debug.Log($"Doctor: {doctor.name}, Specialty: {doctor.specialty}, Clinic: {doctor.clinic}");
                    Debug.Log($"{doctor.clinic}");
                    if (($"{doctor.clinic}") == currentClinic)
                    {
                        addDoctor(doctor.name, doctor.specialty);
                    }
                }
            }
        }
        
    }

    string doctor1 = "", specialty1 = "", doctor2 = "", specialty2 = "";

    public GameObject Doctor1;
    public GameObject Doctor2;
    public TMP_Text doc1_name; 
    public TMP_Text doc2_name;
    public TMP_Text doc1_specialty;
    public TMP_Text doc2_specialty;

    private void addDoctor(string name, string specialty)
    {
        if(doctor1 != "")
        {
            Doctor2.SetActive(true);
            doctor2 = "Doctor " + name;
            specialty2 = specialty;
            doc2_name.text = doctor2;
            doc2_specialty.text = specialty2;
        }
        else
        {
            Doctor1.SetActive(true);
            doctor1 = "Doctor " + name;
            specialty1 = specialty;
            doc1_name.text = doctor1;
            doc1_specialty.text = specialty1;
        }
    }
    private void removeDoctors()
    {
        Doctor1.SetActive(false);
        Doctor2.SetActive(false);
        doctor1 = ""; 
        doctor2 = "";
        specialty1 = "";
        specialty2 = "";
    }
}
