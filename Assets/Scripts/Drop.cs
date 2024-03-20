using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject operationText;
    [SerializeField] private GameObject firstNumberText;
    [SerializeField] private GameObject secondNumberText;

    public DropData Data { get; [ProButton] set; }

    [SerializeField] private GameObject dropImage;
    [SerializeField] private GameObject splashImage;
    [SerializeField] private float splashAnimationTtl;

    private Canvas gameGUICanvas;
    private TextMeshProUGUI operatorTextGUI;
    private TextMeshProUGUI firstNumberTextGUI;
    private TextMeshProUGUI secondNumberTextGUI;

    private DropStateType stateType = DropStateType.Alive;

    public bool IsAlive => stateType == DropStateType.Alive;
    public bool IsResolved => stateType == DropStateType.Resolved;
    public bool IsSplashed => stateType == DropStateType.Splash;

    private Coroutine coroutine;
    private float splashAnimationStartTime = -1;

    private void Awake()
    {
        operatorTextGUI = operationText.GetComponent<TextMeshProUGUI>();
        firstNumberTextGUI = firstNumberText.GetComponent<TextMeshProUGUI>();
        secondNumberTextGUI = secondNumberText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!IsSplashed && !IsResolved) return;

        if (splashAnimationStartTime < 0)
            splashAnimationStartTime = Time.time;

        if (!(Time.time - splashAnimationStartTime >= splashAnimationTtl)) return;

        DropManager.Instance.DespawnDrop(this);
        splashAnimationStartTime = -1;
    }

    private void OnEnable()
    {
        gameGUICanvas = GetComponentInParent<Canvas>();
        stateType = DropStateType.Alive;
        UpdateDropData();
        coroutine = StartCoroutine(Coroutine());
    }

    private void OnDisable()
    {
        if (stateType == DropStateType.Alive)
            stateType = DropStateType.Splash;

        dropImage.SetActive(true);
        splashImage.SetActive(false);

        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (!IsAlive) yield break;

            var vector = new Vector2(0.0F, 9.81F * -Data.Speed) * gameGUICanvas.scaleFactor;
            transform.Translate(vector * Time.deltaTime);
            yield return null;
        }
    }

    public void Resolved()
    {
        stateType = DropStateType.Resolved;
        dropImage.SetActive(false);
        splashImage.SetActive(true);

        GameManager.Instance.Score += Data.Points;
    }

    public void Splash()
    {
        stateType = DropStateType.Splash;
        dropImage.SetActive(false);
        splashImage.SetActive(true);
    }

    private void UpdateDropData()
    {
        operatorTextGUI.SetText(Data.OperatorType.GetSymbol());
        firstNumberTextGUI.SetText(Data.FirstNumber.ToString());
        secondNumberTextGUI.SetText(Data.SecondNumber.ToString());
    }
}

public enum DropStateType
{
    Alive,
    Resolved,
    Splash
}