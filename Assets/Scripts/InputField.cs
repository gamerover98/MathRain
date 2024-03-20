using System.Linq;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    [SerializeField] private int maxDigits = 5;
    private TMP_InputField inputField;

    public void Awake() =>
        inputField = GetComponent<TMP_InputField>();

    private void Start()
    {
        Select();
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        inputField.onValidateInput += OnValidateInput;
    }

    public void Select() => inputField.Select();

    public void Reset() => inputField.text = "";
    
    private void OnInputValueChanged(string newValue)
    {
        if (!int.TryParse(newValue, out var value)) return;
        if (GUIManager.Instance.PauseMenu.IsPaused()) return;
        
        foreach (var drop in
                 DropManager.Instance.SpawnedDrops
                     .Where(drop => drop.IsAlive)
                     .Where(drop => drop.Data.Result == value))
        {
            drop.Resolved();
            inputField.text = $"{char.MinValue}";
        }
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (text.Length + 1 > maxDigits) return char.MinValue;
        return char.IsDigit(addedChar) ? addedChar : char.MinValue;
    }
}