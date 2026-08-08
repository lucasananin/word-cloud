using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordButton : MonoBehaviour
{
    [SerializeField] string _word = null;

    private WordCloudManager _manager = null;
    private Button _button = null;
    private TextMeshProUGUI _text = null;

    private void Awake()
    {
        _manager = FindAnyObjectByType<WordCloudManager>();
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.text = _word;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(AddMyWord);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AddMyWord);
    }

    public void AddMyWord()
    {
        _manager.AddWord(_word);
    }
}
