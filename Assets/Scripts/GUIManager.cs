using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    [SerializeField] private GameObject pausePanelObject;
    public PausePanel PausePanel { get; private set; }

    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject waterPanelObject;
    private WaterPanel waterPanel;

    [SerializeField] private GameObject pauseButtonObject;
    private Button pauseButton;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        PausePanel = pausePanelObject.GetComponent<PausePanel>();
        scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        waterPanel = waterPanelObject.GetComponent<WaterPanel>();
        pauseButton = pauseButtonObject.GetComponent<Button>();

        pauseButton.onClick.AddListener(() => pausePanelObject.SetActive(true));
    }

    public void UpdateScoreText(int newScore) =>
        scoreText.SetText($"Score: {newScore}");

    public void UpdateWaterLevel(int newWaterLevel) =>
        waterPanel.UpdateWaterLevel(newWaterLevel);

}