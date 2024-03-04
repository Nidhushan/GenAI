using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI promptTMP;
    public TextMeshProUGUI itemsTMP;

    public void ShowPrompt(string text)
    {
        StartCoroutine(SetPromptText(text));
    }

    public void SetItemsCollected(int num)
    {
        itemsTMP.text = string.Format("Artifacts: {0}/3", num);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private IEnumerator SetPromptText(string text)
    {
        promptTMP.text = text;
        yield return new WaitForSeconds(3.0f);
        promptTMP.text = "";
    }
}
