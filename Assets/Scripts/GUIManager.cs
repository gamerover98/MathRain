using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    [SerializeField] private GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScoreText(int newScore) =>
        _scoreText.SetText($"Score: {newScore}");
    
}