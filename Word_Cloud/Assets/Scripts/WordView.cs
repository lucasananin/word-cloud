using TMPro;
using UnityEngine;

public class WordView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public RectTransform RectTransform => transform as RectTransform;

    public void SetWord(string word, float fontSize)
    {
        text.text = word;
        text.fontSize = fontSize;
        //text.color = Random.ColorHSV();

        UpdateRectSize();
    }

    private void UpdateRectSize()
    {
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.ForceMeshUpdate();

        Vector2 preferredSize = text.GetPreferredValues();

        RectTransform.sizeDelta = preferredSize;
    }
}