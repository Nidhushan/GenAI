using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ChatGPTAPIExample : MonoBehaviour
{
    private string apiEndpoint = "https://api.openai.com/v1/chat/completions"; // Example endpoint, replace with your ChatGPT API endpoint
    private string apiKey = "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"; // Replace with your ChatGPT API key

    public void Start()
    {
        string prompt = "Hello, ChatGPT!"; // Your message to ChatGPT
        GPTCall(prompt);
        
    }

    IEnumerator GPTCall(string prompt)
    {
        string jsonData = "{\"prompt\": \"" + prompt + "\", \"max_tokens\": 150, \"model\": \"gpt-3.5-turbo\"}"; // Example JSON data, adjust as needed

        // Create UnityWebRequest
        UnityWebRequest request = UnityWebRequest.PostWwwForm(apiEndpoint, jsonData);

        // Set request headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // Send request and wait for response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Handle successful response
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}