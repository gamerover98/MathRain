using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int maxWaterLevel = 2;

    private int _scoreValue;

    public int Score
    {
        get => _scoreValue;
        [ProButton]
        set
        {
            _scoreValue = value;
            GUIManager.Instance.UpdateScoreText(_scoreValue);
        }
    }

    private int _waterLevelValue;

    public int WaterLevel
    {
        get => _waterLevelValue;
        [ProButton]
        set
        {
            if (value > 0 && value <= maxWaterLevel)
            {
                _waterLevelValue = value;
                GUIManager.Instance.UpdateWaterLevel(_waterLevelValue);
            }
            else
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
}