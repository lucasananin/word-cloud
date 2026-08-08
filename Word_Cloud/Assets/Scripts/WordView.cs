using TMPro;
using UnityEngine;

public class WordView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public RectTransform RectTransform => transform as RectTransform;

    public void SetWord(string word, int importance)
    {
        text.text = word;
        UpdateSize(importance);
    }

    private void UpdateSize(int importance)
    {
        text.fontSize = 20f + importance * 5f;
    }
}