using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoMenu
{
    [SerializeField] private GameObject maxScoreTextObject;
    private TextMeshProUGUI maxScoreText;

    [SerializeField] private GameObject currentScoreTextObject;
    private TextMeshProUGUI currentScoreText;

    [SerializeField] private GameObject resumeButtonObject;
    private Button resumeButton;

    [SerializeField] private GameObject restartButtonObject;
    private Button restartButton;

    [SerializeField] private GameObject exitButtonObject;
    private Button exitButton;
    
    private void Awake()
    {
        maxScoreText = maxScoreTextObject.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScoreTextObject.GetComponent<TextMeshProUGUI>();
        resumeButton = resumeButtonObject.GetComponent<Button>();
        restartButton = restartButtonObject.GetComponent<Button>();
        exitButton = exitButtonObject.GetComponent<Button>();

        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
    }

    public override void Open()
    {
        GameManager.FreezeTime(true);

        maxScoreText.SetText($"{GameManager.LoadScoreboard().GetMaxScore()}");
        currentScoreText.SetText($"{GameManager.Instance.Score}");
    }

    public override void Close()
    {
        if (GameManager.Instance.GameState == GameState.InGame)
        {
            Resume();
        }
        else
        {
            Exit();
        }
    }

    private static void Resume()
    {
        GameManager.FreezeTime(false);
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.InGameMenu;
    }

    private static void Restart() => GameManager.StartNewGame();

    private static void Exit() => GameManager.IdleGame();

    public bool IsPaused() => gameObject.activeSelf;
}