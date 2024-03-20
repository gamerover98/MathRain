using com.cyborgAssets.inspectorButtonPro;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoMenu
{
    [SerializeField] private GameObject pauseButtonObject;
    private Button pauseButton;

    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject waterPanelObject;
    private WaterPanel waterPanel;
    
    [SerializeField] private GameObject inputFieldObject;
    private InputField inputField;
    
    public void Awake()
    {
        pauseButton = pauseButtonObject.GetComponent<Button>();
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        waterPanel = waterPanelObject.GetComponent<WaterPanel>();
        inputField = inputFieldObject.GetComponent<InputField>();
        
        pauseButton.onClick.AddListener(Pause);
    }

    public override void Open()
    {
        inputField.Reset();
        inputField.Select();
        GameManager.FreezeTime(false);
        GameManager.Instance.Score = GameManager.Instance.Score; // update score.
    }

    public override void Close() => GameManager.FreezeTime(true);

    [ProButton]
    private void Pause() => 
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.PauseMenu;

    public void UpdateScoreText(int newScore) =>
        scoreText.SetText($"Score: {newScore}");

    public void UpdateWaterLevel(int newWaterLevel) =>
        waterPanel.UpdateWaterLevel(newWaterLevel);
}