using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
