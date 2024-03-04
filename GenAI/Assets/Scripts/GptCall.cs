using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Net;

public class ChatGPTAPIExample : MonoBehaviour
{
    private string apiEndpoint = "https://api.openai.com/v1/chat/completions"; // Example endpoint, replace with your ChatGPT API endpoint
    private string apiKey = "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"; // Replace with your ChatGPT API key
    public string googleTTSApiKey = "AIzaSyA14OJ8FQHcnnmwhjPJLcwri6uURLkNOSc";

    public void Start()
    {
        string prompt = "Hello"; // Your message to ChatGPT
        StartCoroutine(GPTCall(prompt));
        ConvertTextToSpeech(prompt);
    }

    public void ConvertTextToSpeech(string text)
    {
        StartCoroutine(DownloadSpeechAudio(text));
    }

    public class WavUtility
    {
        public static AudioClip ToAudioClip(byte[] audioData, int offset, int length, int sampleRate)
        {
            // Create a new empty WAV file
            var wav = new WAV(audioData, "temp", sampleRate, 16, 1);

            // Create a new AudioClip
            AudioClip audioClip = AudioClip.Create("AudioClip", wav.SampleCount, 1, wav.Frequency, false);

            // Set the audio data
            audioClip.SetData(wav.LeftChannel, 0);

            return audioClip;
        }
    }

    public class WAV
    {
        public float[] LeftChannel { get; private set; }
        public float[] RightChannel { get; private set; }
        public int ChannelCount { get; private set; }
        public int SampleCount { get; private set; }
        public int Frequency { get; private set; }

        public WAV(byte[] wavFile, string filename, int sampleRate, int bitDepth, int channels)
        {
            // Read WAV file header
            int headerSize = 44;
            int dataSize = wavFile.Length - headerSize;
            ChannelCount = channels;
            SampleCount = dataSize / (bitDepth / 8) / channels;
            Frequency = sampleRate;
            LeftChannel = new float[SampleCount];
            RightChannel = new float[SampleCount];

            // Convert bytes to floats
            int bytesPerSample = bitDepth / 8;
            for (int i = 0; i < SampleCount; i++)
            {
                int offset = i * bytesPerSample * channels + headerSize;
                LeftChannel[i] = BytesToFloat(wavFile, offset, bytesPerSample);
                if (channels == 2)
                {
                    offset += bytesPerSample;
                    RightChannel[i] = BytesToFloat(wavFile, offset, bytesPerSample);
                }
            }
        }

        private float BytesToFloat(byte[] bytes, int offset, int bytesPerSample)
        {
            float result = 0;
            for (int i = 0; i < bytesPerSample; i++)
            {
                result += bytes[offset + i] * Mathf.Pow(256, i);
            }
            result /= Mathf.Pow(2, 8 * bytesPerSample - 1);
            return result;
        }
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



    IEnumerator DownloadSpeechAudio(string text)
    {
        string url = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + googleTTSApiKey;
        string jsonRequestBody = "{\"input\":{\"text\":\"" + text + "\"},\"voice\":{\"languageCode\":\"en-US\",\"name\":\"en-US-Wavenet-D\"},\"audioConfig\":{\"audioEncoding\":\"LINEAR16\"}}";

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                byte[] audioData = www.downloadHandler.data;
                PlayAudio(audioData, 16000);
            }
        }
    }

    void PlayAudio(byte[] audioData, int sampleRate)
    {
        Debug.Log("Received audio data. Length: " + audioData.Length + ", Sample Rate: " + sampleRate);

        // Check if the sample rate matches the expected sample rate
        if (sampleRate != 16000)
        {
            Debug.LogError("Unexpected sample rate. Expected: 16000, Actual: " + sampleRate);
            return;
        }

        // Convert the audio data into an AudioClip
        AudioClip clip = WavUtility.ToAudioClip(audioData, 0, audioData.Length, audioData.Length);

        if (clip == null)
        {
            Debug.LogError("Failed to create AudioClip.");
            return;
        }

        // Play the AudioClip
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}