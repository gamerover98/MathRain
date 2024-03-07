using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject operationText;
    [SerializeField] private GameObject firstNumberText;
    [SerializeField] private GameObject secondNumberText;
    [SerializeField] private DropData data;

    public DropData Data
    {
        get => data;
        [ProButton]
        set
        {
            data = value;
            UpdateDropData();
        }
    }

    [SerializeField] private GameObject dropImage;
    [SerializeField] private GameObject splashImage;
    [SerializeField] private float splashAnimationTtl;

    private Canvas _gameGUICanvas;
    private TextMeshProUGUI _operatorTextGUI;
    private TextMeshProUGUI _firstNumberTextGUI;
    private TextMeshProUGUI _secondNumberTextGUI;

    private DropStateType _stateType = DropStateType.Alive;

    public bool IsAlive => _stateType == DropStateType.Alive;
    public bool IsResolved => _stateType == DropStateType.Resolved;
    public bool IsSplashed => _stateType == DropStateType.Splash;

    private Coroutine _coroutine;
    private float _splashAnimationStartTime = -1;

    private void Awake()
    {
        _operatorTextGUI = operationText.GetComponent<TextMeshProUGUI>();
        _firstNumberTextGUI = firstNumberText.GetComponent<TextMeshProUGUI>();
        _secondNumberTextGUI = secondNumberText.GetComponent<TextMeshProUGUI>();
        OnEnable();
    }
    
    private void Update()
    {
        if (!IsSplashed && !IsResolved) return;

        if (_splashAnimationStartTime < 0)
            _splashAnimationStartTime = Time.time;

        if (!(Time.time - _splashAnimationStartTime >= splashAnimationTtl)) return;

        DropManager.Instance.ReturnObjectToPool(this);
        _splashAnimationStartTime = -1;
    }

    private void OnEnable()
    {
        _gameGUICanvas = GetComponentInParent<Canvas>();
        _stateType = DropStateType.Alive;
        UpdateDropData();
        _coroutine = StartCoroutine(Coroutine());
    }

    private void OnDisable()
    {
        if (_stateType == DropStateType.Alive)
            _stateType = DropStateType.Splash;

        dropImage.SetActive(true);
        splashImage.SetActive(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
    
    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (!IsAlive) yield break;

            var vector = new Vector2(0.0F, 9.81F * -Data.Speed) * _gameGUICanvas.scaleFactor;
            transform.Translate(vector * Time.deltaTime);
            yield return null;
        }
    }

    public void Resolved()
    {
        _stateType = DropStateType.Resolved;
        dropImage.SetActive(false);
        splashImage.SetActive(true);

        GameManager.Instance.Score += Data.Points;
    }

    public void Splash()
    {
        _stateType = DropStateType.Splash;
        dropImage.SetActive(false);
        splashImage.SetActive(true);
    }

    private void UpdateDropData()
    {
        _operatorTextGUI.SetText(Data.OperatorType.GetSymbol());
        _firstNumberTextGUI.SetText(Data.FirstNumber.ToString());
        _secondNumberTextGUI.SetText(Data.SecondNumber.ToString());
    }
}

public enum DropStateType
{
    Alive,
    Resolved,
    Splash
}