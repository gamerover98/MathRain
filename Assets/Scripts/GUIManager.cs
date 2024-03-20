using UnityEngine;
using Menu;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenuPanelObject;
    public MainMenu MainMenu { get; private set; }

    [SerializeField] private GameObject inGameMenuObject;
    public InGameMenu InGameMenu { get; private set; }
    
    [SerializeField] private GameObject pausePanelObject;
    public PauseMenu PauseMenu { get; private set; }

    [SerializeField] private GameObject endGameMenuPanelObject;
    public EndGameMenu EndGameMenu { get; private set; }

    private MonoMenu currentMenu;

    public MonoMenu CurrentMenu
    {
        get => currentMenu;
        set
        {
            if (currentMenu != null) currentMenu.SetActive(false);
            currentMenu = value;
            value.SetActive(true);
        }
    }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;

        MainMenu = mainMenuPanelObject.GetComponent<MainMenu>();
        InGameMenu = inGameMenuObject.GetComponent<InGameMenu>();
        PauseMenu = pausePanelObject.GetComponent<PauseMenu>();
        EndGameMenu = endGameMenuPanelObject.GetComponent<EndGameMenu>();
        
        CurrentMenu = MainMenu;
    }
}