using com.cyborgAssets.inspectorButtonPro;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoMenu
{
    [SerializeField] private GameObject maxScoreTextObject;
    private TextMeshProUGUI maxScoreText;

    [SerializeField] private GameObject currentScoreTextObject;
    private TextMeshProUGUI currentScoreText;

    [SerializeField] private GameObject restartButtonObject;
    private Button restartButton;

    [SerializeField] private GameObject exitButtonObject;
    private Button exitButton;
    
    private void Awake()
    {
        maxScoreText = maxScoreTextObject.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScoreTextObject.GetComponent<TextMeshProUGUI>();
        restartButton = restartButtonObject.GetComponent<Button>();
        exitButton = exitButtonObject.GetComponent<Button>();
        
        restartButton.onClick.AddListener(Restart);
        exitButton.onClick.AddListener(Exit);
    }

    public override void Open()
    {
        GameManager.FreezeTime(true);
        UpdateEndGamePanel();
    }

    public override void Close()
    {
        // Nothing to do.
    }

    [ProButton]
    private static void Restart() => GameManager.StartNewGame();

    [ProButton]
    private static void Exit() => 
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.MainMenu;
    
    private void UpdateEndGamePanel()
    {
        maxScoreText.SetText($"{GameManager.LoadScoreboard().GetMaxScore()}");
        currentScoreText.SetText($"{GameManager.Instance.Score}");
    }
}