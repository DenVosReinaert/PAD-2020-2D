using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class MySQLHandler : MonoBehaviour {

    public void Awake() {
        CreateTables();
    }

    public void CreateTables() {
        StartCoroutine(Tables());
    }

    private IEnumerator Tables() {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        form.Add(new MultipartFormDataSection("name", "Stomar"));
        form.Add(new MultipartFormDataSection("score", "2400"));
        UnityWebRequest tables = UnityWebRequest.Post(Application.dataPath + "\\Scripts\\PHP\\InputData.php", form);
        yield return tables.SendWebRequest();
        if (tables.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("Connection error!");
        } else if (tables.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("Protocol error!");
        } else if (tables.result == UnityWebRequest.Result.Success) {
            Debug.Log("Tables made successfully!");
        }
    }
}
