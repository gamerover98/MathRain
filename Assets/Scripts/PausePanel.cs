using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public static PausePanel Instance { get; private set; }
    
    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;
    
    [SerializeField] private GameObject resumeButtonObject;
    private Button _resumeButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        _resumeButton = resumeButtonObject.GetComponent<Button>();
        
        _resumeButton.onClick.AddListener(Resume);
    }

    private void OnEnable()
    {
        UpdatePausePanel();
    }
    
    private void OnDisable()
    {
        UpdatePausePanel();
    }

    [ProButton]
    private void UpdatePausePanel()
    {
        _scoreText.SetText($"{GameManager.Instance.Score}");
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }

    private void Resume()
    {
        gameObject.SetActive(false);
    }
}