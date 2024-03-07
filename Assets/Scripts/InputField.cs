using System.Linq;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    [SerializeField] private int maxDigits = 5;
    private TMP_InputField _tmpInputField;

    public void Awake()
    {
        _tmpInputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        _tmpInputField.Select();
        _tmpInputField.onValueChanged.AddListener(OnInputValueChanged);
        _tmpInputField.onValidateInput += OnValidateInput;
    }

    private void OnInputValueChanged(string newValue)
    {
        if (!int.TryParse(newValue, out var value)) return;

        foreach (var drop in
                 DropManager.Instance.SpawnedDrops
                     .Where(drop => drop.IsAlive)
                     .Where(drop => drop.Data.Result == value))
        {
            drop.Resolved();
            _tmpInputField.text = $"{char.MinValue}";
        }
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (text.Length + 1 > maxDigits) return char.MinValue;
        return char.IsDigit(addedChar) ? addedChar : char.MinValue;
    }
}