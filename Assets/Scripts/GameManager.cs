using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int maxWaterLevel = 2;

    private int score;

    public int Score
    {
        [ProButton] get => score;
        [ProButton]
        set
        {
            score = value;
            GUIManager.Instance.UpdateScoreText(score);
        }
    }

    private int waterLevel;

    public int WaterLevel
    {
        [ProButton] get => waterLevel;
        [ProButton]
        set
        {
            waterLevel = value;
            GUIManager.Instance.UpdateWaterLevel(waterLevel);

            if (value >= maxWaterLevel)
            {
                //TODO: endgame
                Debug.Log("END GAME!!!");
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        WaterLevel = 0;
    }
}