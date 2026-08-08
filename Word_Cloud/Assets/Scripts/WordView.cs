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
        UpdateRectSize();
    }

    private void UpdateSize(int importance)
    {
        text.fontSize = 20f + importance * 5f;
    }

    private void UpdateRectSize()
    {
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.ForceMeshUpdate();

        Vector2 preferredSize = text.GetPreferredValues();

        RectTransform.sizeDelta = preferredSize;
    }
}