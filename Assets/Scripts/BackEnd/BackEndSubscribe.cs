using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEditor;

public sealed class BackendSubscribe : MonoBehaviour
{
    public static BackendSubscribe Instance { get; private set; }
    private string deviceid;
    private string reqBodyJson;
    private string updateBodyJson;
    public int baseCoin;
    public int initialbaseCoin;
    public int levelsCompleted;
    public int currentLevel;

    [SerializeField] private GameObject offlinePanel;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private GameObject levelSystem;

    void Awake()
    {
        if (Instance != null)
        {

        }
        else
        {
            Instance = this;
        }
        Login();
        //Application.targetFrameRate = 60;
    }

    public async void Login()
    {
        deviceid = SystemInfo.deviceUniqueIdentifier;
        Debug.Log(deviceid);
        var payload = new Dictionary<string, object>
        {
            { "deviceId", deviceid }
        };

        string encryptedKey = "5CXytVVdktc/caNI1msTGh7a3kB1kw5zMuEXo4lb7fRF/8/P5KmokEkN5LVIlWIj";
        string secretKey = EncryptionUtility.Decrypt(encryptedKey);
        Debug.Log(secretKey);

        if (string.IsNullOrEmpty(secretKey))
        {
            return;
        }

        string token = JWTGenerator.CreateToken(payload, secretKey);
        var reqBody = new Dictionary<string, object>
        {
            { "info", token }
        };

        reqBodyJson = JsonConvert.SerializeObject(reqBody);

        await SendLoginRequest("https://games.adbreakmedia.com/api/games/hyperzombie/users/add", reqBodyJson);
    }

    public async void UpdateCoin()
    {
        var payload = new Dictionary<string, object>
        {
            { "deviceId", deviceid },
            { "coins", initialbaseCoin }
        };

        string encryptedKey = "E425F2B5555D92D73F71A348D66B131A1EDADE4075930E7332E117A3895BEDF445FFCFCFE4A9A890490DE4B548956223";
        string secretKey = EncryptionUtility.Decrypt(encryptedKey);

        if (string.IsNullOrEmpty(secretKey))
        {
            return;
        }

        string token = JWTGenerator.CreateToken(payload, secretKey);

        Debug.Log("Generated JWT: " + token);

        var reqBody = new Dictionary<string, object>
        {
            { "info", token },
        };

        updateBodyJson = JsonConvert.SerializeObject(reqBody);

        Debug.Log(initialbaseCoin);
        await SendRequest("https://games.adbreakmedia.com/api/games/hyperzombie/updateLevel", updateBodyJson);
    }

    public async Task SendRequest(string url, string json)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            // Wait until the operation is completed
            while (!operation.isDone)
            {
                await Task.Yield(); // Yield to the Unity main loop
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);

            }
            else
            {
                Debug.Log("API Response: " + request.downloadHandler.text);
                // Handle API response here
                string jsonResponse = request.downloadHandler.text;

                // Deserialize the JSON to a C# object
                ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                // Update UI with earnings
                if (response != null && response.success)
                {
                    baseCoin = response.totalEarning;
                    levelsCompleted = response.completedLevels;
                    //Debug.Log("CurrentLevel: " + currentLevel);
                }
            }
        }
    }

    public async Task SendLoginRequest(string url, string json)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            // Wait until the operation is completed
            while (!operation.isDone)
            {
                await Task.Yield(); // Yield to the Unity main loop
            }

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                introPanel.SetActive(false);
                offlinePanel.SetActive(true);
                levelSystem.SetActive(false);
            }
            else
            {
                Debug.Log("API Response: " + request.downloadHandler.text);
                // Handle API response here
                string jsonResponse = request.downloadHandler.text;

                // Deserialize the JSON to a C# object
                ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                // Update UI with earnings
                if (response != null && response.success)
                {   
                    baseCoin = response.totalEarning;
                    levelsCompleted = response.completedLevels;
                    introPanel.SetActive(true);
                    offlinePanel.SetActive(false);
                    levelSystem.SetActive(true);
                }
                else
                {
                    Debug.Log("1");
                    introPanel.SetActive(false);
                    offlinePanel.SetActive(true);
                    levelSystem.SetActive(false);
                }
            }
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private class ApiResponse
    {
        public bool success;
        public string message;
        public int totalEarning;
        public int completedLevels;
    }
}