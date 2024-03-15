using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject maxScoreTextObject;
    private TextMeshProUGUI maxScoreText;

    [SerializeField] private GameObject currentScoreTextObject;
    private TextMeshProUGUI currentScoreText;

    [SerializeField] private GameObject restartButtonObject;
    private Button restartButton;

    [SerializeField] private GameObject exitButtonObject;
    private Button exitButton;

    private void Start()
    {
        maxScoreText = maxScoreTextObject.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScoreTextObject.GetComponent<TextMeshProUGUI>();
        restartButton = restartButtonObject.GetComponent<Button>();
        exitButton = exitButtonObject.GetComponent<Button>();
    }
    
    private void OnEnable() => UpdateEndGamePanel();

    private void OnDisable() => UpdateEndGamePanel();
    
    [ProButton]
    private void UpdateEndGamePanel()
    {
        var isGameEnded = IsGameEnded();

        maxScoreText.SetText($"{GameManager.Instance.LoadScoreboard().GetMaxScore()}");
        currentScoreText.SetText($"{GameManager.Instance.Score}");
        Time.timeScale = isGameEnded ? 0 : 1;
    }

    [ProButton]
    public void Resume() => gameObject.SetActive(false);

    [ProButton]
    public void Restart()
    {
        GameManager.Instance.ResetGame();
        Resume();
    }
    
    [ProButton]
    public void Exit() => MainMenuPanel.Instance.gameObject.SetActive(true);

    [ProButton]
    public bool IsGameEnded() => gameObject.activeSelf;
}