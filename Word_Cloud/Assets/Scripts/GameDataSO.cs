using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Scriptable Objects/GameDataSO")]
public class GameDataSO : ScriptableObject
{
    private readonly List<WordData> _words = new();

    public List<WordData> Words => _words;

    private void OnEnable()
    {
        _words.Clear();
    }

    public void AddWord(string word)
    {
        word.Trim();

        if (string.IsNullOrEmpty(word)) return;

        WordData existingWord = _words.Find(
            wordData => string.Equals(
                wordData.Word,
                word,
                System.StringComparison.OrdinalIgnoreCase
            )
        );

        if (existingWord != null)
        {
            //existingWord.Importance++;
            existingWord.Importance += Random.Range(1, 3);
        }
        else
        {
            _words.Add(new WordData(word, 1));
        }
    }
}
