using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject operationText;
    [SerializeField] private GameObject firstNumberText;
    [SerializeField] private GameObject secondNumberText;
    [SerializeField] private DropData data;

    private TextMeshProUGUI _operatorTextGUI;
    private TextMeshProUGUI _firstNumberTextGUI;
    private TextMeshProUGUI _secondNumberTextGUI;

    private void Awake()
    {
        _operatorTextGUI = operationText.GetComponent<TextMeshProUGUI>();
        _firstNumberTextGUI = firstNumberText.GetComponent<TextMeshProUGUI>();
        _secondNumberTextGUI = secondNumberText.GetComponent<TextMeshProUGUI>();
        UpdateDropData();
    }

    private void Update()
    {
        //TODO: implement physics 
    }

    [ProButton]
    private void UpdateDropData()
    {
        _operatorTextGUI.SetText(data.OperatorType.GetSymbol());
        _firstNumberTextGUI.SetText(data.FirstNumber.ToString());
        _secondNumberTextGUI.SetText(data.SecondNumber.ToString());
    }
}