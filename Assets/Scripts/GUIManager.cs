using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    [SerializeField] private GameObject pausePanel;
    private PausePanel _pausePanel;

    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;

    [SerializeField] private GameObject waterPanelObject;
    private WaterPanel _waterPanel;

    [SerializeField] private GameObject pauseButtonObject;
    private Button _pauseButton;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        _waterPanel = waterPanelObject.GetComponent<WaterPanel>();
        _pausePanel = pausePanel.GetComponent<PausePanel>();
        _pauseButton = pauseButtonObject.GetComponent<Button>();

        _pauseButton.onClick.AddListener(Pause);
    }

    public void UpdateScoreText(int newScore) =>
        _scoreText.SetText($"Score: {newScore}");

    public void UpdateWaterLevel(int newWaterLevel) =>
        _waterPanel.UpdateWaterLevel(newWaterLevel);

    private void Pause()
    {
        pausePanel.SetActive(true);
    }
}