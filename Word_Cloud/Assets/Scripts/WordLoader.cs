using System.Linq;
using UnityEngine;

public class WordLoader : MonoBehaviour
{
    [SerializeField] GameDataSO _so = null;
    [SerializeField] WordView _prefab;
    [SerializeField] RectTransform _parent;
    [Space]
    [SerializeField] Gradient _gradient = null;
    [SerializeField] float _fontSize = 60;

    private void Start()
    {
        var _count = _so.Words.Count;

        for (int i = 0; i < _count; i++)
        {
            var _instance = Instantiate(_prefab, _parent);
            _instance.SetWord(_so.Words[i].Word, _fontSize, GetColorImportance(_so.Words[i]));
        }
    }

    private Color GetColorImportance(WordData word)
    {
        int minImportance = _so.Words.Min(wordData => wordData.Importance);
        int maxImportance = _so.Words.Max(wordData => wordData.Importance);

        if (minImportance == maxImportance)
            return _gradient.Evaluate(0);

        float normalizedImportance = Mathf.InverseLerp(
            minImportance,
            maxImportance,
            word.Importance
        );

        return _gradient.Evaluate(normalizedImportance);
        //return Mathf.Lerp(minFontSize, maxFontSize, normalizedImportance);
    }
}
