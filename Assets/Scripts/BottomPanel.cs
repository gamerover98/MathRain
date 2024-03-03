using TMPro;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int maxDigits = 5;

    //TODO: Temporary
    [SerializeField] private GameObject dropObject;
    private Drop _drop;

    private void Start()
    {
        inputField.Select();
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        inputField.onValidateInput += OnValidateInput;
        _drop = dropObject.GetComponent<Drop>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent<Drop>(out var drop);
        if (drop == null) return;

        drop.Splash();
    }

    private void OnInputValueChanged(string newValue)
    {
        if (!int.TryParse(newValue, out var value)) return;
        if (_drop.data.Result == value)
        {
            _drop.Resolved();
            inputField.text = $"{char.MinValue}";
        }
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (text.Length + 1 > maxDigits) return char.MinValue;
        return char.IsDigit(addedChar) ? addedChar : char.MinValue;
    }
}