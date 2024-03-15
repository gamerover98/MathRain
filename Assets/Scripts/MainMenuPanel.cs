using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public static MainMenuPanel Instance { get; private set; }
    
    [SerializeField] private GameObject newGameButtonObject;
    private Button newGameButton;
    
    [SerializeField] private GameObject contentTextAreObject;
    private TextMeshProUGUI contentText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        contentText = contentTextAreObject.GetComponent<TextMeshProUGUI>();
        newGameButton = newGameButtonObject.GetComponent<Button>();
        newGameButton.onClick.AddListener(StartNewGame);
    }

    private void Start()
    {
        GameManager.Instance.ResetGame();
        UpdateScoreboardText();
    }

    private void OnEnable()
    {
        UpdateScoreboardText();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        GameManager.Instance.ResetGame();
        GUIManager.Instance.PausePanel.gameObject.SetActive(false);
    }

    private void StartNewGame() => gameObject.SetActive(false);
    
    private void UpdateScoreboardText()
    {
        if (!GameManager.Instance) return;
        
        var counter = 1;
        var stringBuilder = new StringBuilder();
        foreach (var game in GameManager.Instance.LoadScoreboard().GetGamesSortedByScoreDescending())
        {
            stringBuilder.AppendLine(
                $"<b><size=100%>{counter}. " +
                $"<color=\"green\">{game.score}</color> " +
                $"<size=80%><color=\"white\">({game.dateTime})</color>");
            counter++;
        }

        contentText.text = stringBuilder.ToString();
    }
}
