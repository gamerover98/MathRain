using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;

    private int _scoreValue;

    public int Score
    {
        get => _scoreValue;
        [ProButton]
        set
        {
            _scoreValue = value;
            UpdateScoreText();
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateScoreText()
    {
        _scoreText.SetText($"Score: {_scoreValue}");
    }
}