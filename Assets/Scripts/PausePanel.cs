using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pauseButtonObject;

    [SerializeField] private GameObject maxScoreTextObject;
    private TextMeshProUGUI maxScoreText;
    
    [SerializeField] private GameObject currentScoreTextObject;
    private TextMeshProUGUI currentScoreText;

    [SerializeField] private GameObject resumeButtonObject;
    private Button resumeButton;

    [SerializeField] private GameObject inputFieldObject;
    private InputField inputField;

    private void Awake()
    {
        maxScoreText = maxScoreTextObject.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScoreTextObject.GetComponent<TextMeshProUGUI>();
        resumeButton = resumeButtonObject.GetComponent<Button>();
        inputField = inputFieldObject.GetComponent<InputField>();

        resumeButton.onClick.AddListener(Resume);
    }

    private void OnEnable() => UpdatePausePanel();

    private void OnDisable() => UpdatePausePanel();

    [ProButton]
    private void UpdatePausePanel()
    {
        var paused = IsPaused();

        maxScoreText.SetText($"{GameManager.Instance.LoadScoreboard().GetMaxScore()}");
        currentScoreText.SetText($"{GameManager.Instance.Score}");
        pauseButtonObject.SetActive(!paused);

        if (paused)
        {
            inputField.Select();
        }

        Time.timeScale = paused ? 0 : 1;
    }

    [ProButton]
    public void Resume() => gameObject.SetActive(false);

    [ProButton]
    public bool IsPaused() => gameObject.activeSelf;
}