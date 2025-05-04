using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public string serverURL = "http://localhost:3000/api";

    public void OnRegisterClicked()
    {
        StartCoroutine(RegisterUser());
    }

    public void OnLoginClicked()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator RegisterUser()
    {
        var user = new { username = usernameField.text, password = passwordField.text };
        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(serverURL + "/register", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("✅ Registered successfully");
        else
            Debug.Log("❌ Registration failed: " + request.downloadHandler.text);
    }

    IEnumerator LoginUser()
    {
        var user = new { username = usernameField.text, password = passwordField.text };
        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(serverURL + "/login", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("✅ Logged in successfully");
        else
            Debug.Log("❌ Login failed: " + request.downloadHandler.text);
    }
}
