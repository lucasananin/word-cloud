using UnityEngine;

public class WordCloudManager : MonoBehaviour
{
    [SerializeField] private RectTransform wordCloudArea;
    [SerializeField] private WordView wordPrefab;

    private void Start()
    {
        CreateWord("Hello", 1);
        CreateWord("Unity", 3);
        CreateWord("Cloud", 5);
    }

    //private void CreateWord(string word, int importance)
    //{
    //    WordView wordInstance = Instantiate(wordPrefab, wordCloudArea);

    //    wordInstance.SetWord(word, importance);

    //    if (TryFindPosition(wordInstance, out Vector2 position))
    //    {
    //        wordInstance.RectTransform.anchoredPosition = position;
    //    }
    //    else
    //    {
    //        Destroy(wordInstance.gameObject);
    //    }
    //}

    //private bool TryFindPosition(WordView word, out Vector2 position)
    //{
    //    const float angleStep = 15f;
    //    const float radiusStep = 10f;
    //    const float maxRadius = 500f;

    //    for (float radius = 0f; radius <= maxRadius; radius += radiusStep)
    //    {
    //        for (float angle = 0f; angle < 360f; angle += angleStep)
    //        {
    //            float radians = angle * Mathf.Deg2Rad;

    //            Vector2 candidate = new Vector2(
    //                Mathf.Cos(radians) * radius,
    //                Mathf.Sin(radians) * radius
    //            );

    //            if (IsPositionValid(word, candidate))
    //            {
    //                position = candidate;
    //                return true;
    //            }
    //        }
    //    }

    //    position = Vector2.zero;
    //    return false;
    //}

    //private bool IsPositionValid(WordView word, Vector2 position)
    //{
    //    RectTransform rect = word.RectTransform;

    //    Vector2 size = rect.rect.size;
    //    Vector2 halfSize = size * 0.5f;

    //    Rect candidateRect = new Rect(
    //        position - halfSize,
    //        size
    //    );

    //    foreach (WordView existingWord in wordCloudArea.GetComponentsInChildren<WordView>())
    //    {
    //        if (existingWord == word)
    //            continue;

    //        RectTransform existingRect = existingWord.RectTransform;

    //        Vector2 existingPosition = existingRect.anchoredPosition;
    //        Vector2 existingHalfSize = existingRect.rect.size * 0.5f;

    //        Rect existingRectBounds = new Rect(
    //            existingPosition - existingHalfSize,
    //            existingRect.rect.size
    //        );

    //        if (candidateRect.Overlaps(existingRectBounds))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    private void CreateWord(string word, int importance)
    {
        WordView wordInstance = Instantiate(wordPrefab, wordCloudArea);

        wordInstance.SetWord(word, importance);
        wordInstance.RectTransform.anchoredPosition = Vector2.zero;
    }
}