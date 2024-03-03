using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject operationText;
    [SerializeField] private GameObject firstNumberText;
    [SerializeField] private GameObject secondNumberText;
    public DropData data;

    [SerializeField] private GameObject dropImage;
    [SerializeField] private GameObject splashImage;
    [SerializeField] private float splashAnimationTtl;
    
    private TextMeshProUGUI _operatorTextGUI;
    private TextMeshProUGUI _firstNumberTextGUI;
    private TextMeshProUGUI _secondNumberTextGUI;

    private DropStateType _stateType = DropStateType.Alive;

    public bool IsAlive => _stateType == DropStateType.Alive;
    public bool IsResolved => _stateType == DropStateType.Resolved;
    public bool IsSplashed => _stateType == DropStateType.Splash;

    private float _splashAnimationStartTime = -1;
    
    private void Awake()
    {
        _operatorTextGUI = operationText.GetComponent<TextMeshProUGUI>();
        _firstNumberTextGUI = firstNumberText.GetComponent<TextMeshProUGUI>();
        _secondNumberTextGUI = secondNumberText.GetComponent<TextMeshProUGUI>();
        UpdateDropData();
    }

    private void Update()
    {
        if (IsAlive)
        {
            transform.Translate(new Vector2(0.0F, 9.81F * -data.Speed) * Time.deltaTime);
        }
        else if (IsSplashed || IsResolved)
        {
            if (_splashAnimationStartTime < 0)
                _splashAnimationStartTime = Time.time;
            
            if (Time.time - _splashAnimationStartTime >= splashAnimationTtl) 
                Destroy(gameObject);
        }
    }

    public void Resolved()
    {
        _stateType = DropStateType.Resolved;
        dropImage.SetActive(false);
        splashImage.SetActive(true);

        GameManager.Instance.Score += data.Points;
    }
    
    public void Splash()
    {
        _stateType = DropStateType.Splash;
        dropImage.SetActive(false);
        splashImage.SetActive(true);
    }
    
    [ProButton]
    private void UpdateDropData()
    {
        _operatorTextGUI.SetText(data.OperatorType.GetSymbol());
        _firstNumberTextGUI.SetText(data.FirstNumber.ToString());
        _secondNumberTextGUI.SetText(data.SecondNumber.ToString());
    }
}

public enum DropStateType
{
    Alive,
    Resolved,
    Splash
}