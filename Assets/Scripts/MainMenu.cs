using System.Text;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoMenu
{
    [SerializeField] private GameObject newGameButtonObject;
    private Button newGameButton;

    [SerializeField] private GameObject contentTextAreObject;
    private TextMeshProUGUI contentText;

    [SerializeField] private GameObject exitGameObject;
    private Button exitGame;

    private void Awake()
    {
        contentText = contentTextAreObject.GetComponent<TextMeshProUGUI>();
        newGameButton = newGameButtonObject.GetComponent<Button>();
        exitGame = exitGameObject.GetComponent<Button>();

        newGameButton.onClick.AddListener(GameManager.StartNewGame);
        exitGame.onClick.AddListener(Application.Quit);
    }
    
    public override void Open()
    {
        GameManager.FreezeTime(true);
        DropManager.Instance.StopDropCoroutine();
        UpdateScoreboardText();
    }

    public override void Close() => GameManager.ResetAndClean();

    private void UpdateScoreboardText()
    {
        if (!GameManager.Instance) return;

        var counter = 1;
        var stringBuilder = new StringBuilder();
        foreach (var game in GameManager.LoadScoreboard().GetGamesSortedByScoreDescending())
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