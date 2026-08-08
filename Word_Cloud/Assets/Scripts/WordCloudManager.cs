using UnityEngine;

public class WordCloudManager : MonoBehaviour
{
    [SerializeField] private RectTransform wordCloudArea;
    [SerializeField] private WordView wordPrefab;

    private void Start()
    {
        Init();
    }

    [ContextMenu(nameof(Init))]
    public void Init()
    {
        foreach (Transform child in wordCloudArea.transform)
        {
            Destroy(child.gameObject);
        }

        CreateWord("Cloud", 5);
        CreateWord("Unity", 3);
        CreateWord("Hello", 1);
    }

    private void CreateWord(string word, int importance)
    {
        WordView wordInstance = Instantiate(wordPrefab, wordCloudArea);

        wordInstance.SetWord(word, importance);

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
        const float radiusStep = 200f;
        const float angleStep = 30f;
        const float maxRadius = 500f;

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