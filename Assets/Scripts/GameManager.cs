using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState GameState { get; private set; } = GameState.Idle;

    [SerializeField] private int maxWaterLevel = 2;

    private int score;

    public int Score
    {
        [ProButton] get => score;
        [ProButton]
        set
        {
            score = value;
            (GUIManager.Instance.CurrentMenu as InGameMenu)?.UpdateScoreText(score);
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
            (GUIManager.Instance.CurrentMenu as InGameMenu)?.UpdateWaterLevel(waterLevel);

            if (value < maxWaterLevel) return;
            SaveGameScore();
            EndGame();
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        GameState = GameState.Idle;
    }
    
    [ProButton]
    public static void SaveGameScore()
    {
        if (Instance == null)
        {
            Debug.LogWarning("The GameManager is null, can't save the game");
            return;
        }

        var score = Instance.Score;
        var scoreboard = LoadScoreboard();
        if (score > 0) scoreboard.AddGame(new Game(score));

        PlayerPrefs.SetString("Scoreboard", JsonUtility.ToJson(scoreboard));
        PlayerPrefs.Save();
    }

    public static Scoreboard LoadScoreboard()
    { 
        Scoreboard result = null;

        if (PlayerPrefs.HasKey("Scoreboard"))
            result = 
                JsonUtility.FromJson<Scoreboard>(
                    PlayerPrefs.GetString("Scoreboard", "{}"));
        
        result ??= new Scoreboard();
        return result;
    }

    public static void StartNewGame()
    {
        if (Instance == null)
        {
            Debug.LogWarning("The GameManager is null, cannot start the game.");
            return;
        }
        
        ResetAndClean();
        Instance.GameState = GameState.InGame;
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.InGameMenu;
    }

    public static void EndGame()
    {
        if (Instance == null)
        {
            Debug.LogWarning("The GameManager is null, cannot end the game.");
            return;
        }
        
        FreezeTime(true);
        ResetAndClean();
        Instance.GameState = GameState.EndGame;
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.EndGameMenu;
    }

    public static void IdleGame()
    {
        if (Instance == null)
        {
            Debug.LogWarning("The GameManager is null, cannot idle the game.");
            return;
        }
        
        FreezeTime(true);
        ResetAndClean();
        Instance.GameState = GameState.Idle;
        GUIManager.Instance.CurrentMenu = GUIManager.Instance.MainMenu;
    }
    
    /// <summary>
    /// Utility method to freeze or unfreeze the time scale.
    /// </summary>
    /// <param name="freeze">Boolean value indicating whether to freeze (<c>true</c>)
    ///                      or unfreeze (<c>false</c>) the time scale.</param>
    public static void FreezeTime(bool freeze) => Time.timeScale = freeze ? 0 : 1;
    
    public static void ResetAndClean()
    {
        Instance.Score = 0;
        Instance.WaterLevel = 0;
        DropManager.Instance.DespawnAllDrops();
    }
}

public enum GameState
{
    Idle,
    InGame,
    EndGame
}