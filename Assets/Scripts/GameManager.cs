using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}