using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCloudManager : MonoBehaviour
{
    [SerializeField] private RectTransform wordCloudArea;
    [SerializeField] private WordView wordPrefab;
    [SerializeField] private TMP_InputField inputField;

    private readonly List<WordData> words = new();

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        inputField.onSubmit.AddListener(_ => AddWord());
    }

    private void OnDisable()
    {
        inputField.onSubmit.RemoveAllListeners();
    }

    [ContextMenu(nameof(Init))]
    public void Init()
    {
        words.Add(new WordData("Cloud", 5));
        words.Add(new WordData("Unity", 3));
        words.Add(new WordData("Hello", 1));

        RebuildCloud();
    }

    public void AddWord()
    {
        string word = inputField.text.Trim();

        if (string.IsNullOrEmpty(word))
            return;

        WordData existingWord = words.Find(
            wordData => string.Equals(
                wordData.Word,
                word,
                System.StringComparison.OrdinalIgnoreCase
            )
        );

        if (existingWord != null)
        {
            existingWord.Importance++;
        }
        else
        {
            words.Add(new WordData(word, 1));
        }

        inputField.text = string.Empty;

        RebuildCloud();
    }

    private void RebuildCloud()
    {
        //ClearCloud();

        //List<WordData> sortedWords = new(words);

        //sortedWords.Sort((a, b) => b.Importance.CompareTo(a.Importance));

        //foreach (WordData word in sortedWords)
        //{
        //    CreateWord(word);
        //}
        StartCoroutine(RebuildCloud_Routine());
    }

    private IEnumerator RebuildCloud_Routine()
    {
        ClearCloud();
        yield return null;

        List<WordData> sortedWords = new(words);

        sortedWords.Sort((a, b) => b.Importance.CompareTo(a.Importance));

        foreach (WordData word in sortedWords)
        {
            CreateWord(word);
        }
    }

    private void ClearCloud()
    {
        foreach (Transform child in wordCloudArea)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateWord(WordData wordData)
    {
        WordView wordInstance = Instantiate(wordPrefab, wordCloudArea);

        wordInstance.SetWord(
            wordData.Word,
            wordData.Importance
        );

        if (TryFindPosition(wordInstance, out Vector2 position))
        {
            wordInstance.RectTransform.anchoredPosition = position;
        }
        else
        {
            Destroy(wordInstance.gameObject);
        }
    }

    private bool TryFindPosition(WordView word, out Vector2 position)
    {
        const float radiusStep = 20f;
        const float angleStep = 30f;

        float maxRadius = GetMaxSearchRadius(word);

        for (float radius = 0f; radius <= maxRadius; radius += radiusStep)
        {
            float startAngle = Random.Range(0f, 360f);

            for (float angle = 0f; angle < 360f; angle += angleStep)
            {
                float radians = (startAngle + angle) * Mathf.Deg2Rad;

                Vector2 candidate = new Vector2(
                    Mathf.Cos(radians) * radius,
                    Mathf.Sin(radians) * radius
                );

                if (IsPositionValid(word, candidate))
                {
                    position = candidate;
                    return true;
                }
            }
        }

        position = Vector2.zero;
        return false;
    }

    private float GetMaxSearchRadius(WordView word)
    {
        Vector2 cloudSize = wordCloudArea.rect.size;
        Vector2 wordSize = word.RectTransform.rect.size;

        float horizontalRadius = (cloudSize.x - wordSize.x) * 0.5f;
        float verticalRadius = (cloudSize.y - wordSize.y) * 0.5f;

        return Mathf.Max(0f, Mathf.Min(horizontalRadius, verticalRadius));
    }

    private bool IsPositionValid(WordView word, Vector2 position)
    {
        RectTransform rect = word.RectTransform;

        Vector2 size = rect.rect.size;
        Vector2 halfSize = size * 0.5f;

        Rect candidateRect = new Rect(
            position - halfSize,
            size
        );

        if (!IsInsideCloud(candidateRect))
        {
            return false;
        }

        foreach (WordView existingWord in wordCloudArea.GetComponentsInChildren<WordView>())
        {
            if (existingWord == word)
                continue;

            RectTransform existingRect = existingWord.RectTransform;

            Vector2 existingPosition = existingRect.anchoredPosition;
            Vector2 existingSize = existingRect.rect.size;
            Vector2 existingHalfSize = existingSize * 0.5f;

            Rect existingRectBounds = new Rect(
                existingPosition - existingHalfSize,
                existingSize
            );

            if (candidateRect.Overlaps(existingRectBounds))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsInsideCloud(Rect wordRect)
    {
        Rect cloudRect = wordCloudArea.rect;

        return cloudRect.Contains(wordRect.min)
            && cloudRect.Contains(wordRect.max);
    }
}