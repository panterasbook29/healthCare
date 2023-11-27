using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{

    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://www.panterasbook.space/sqlconnect/webtest.php");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string[] webResults = request.downloadHandler.text.Split('\t');
            foreach(string s in webResults)
            {
                Debug.Log(s);

            }
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}
