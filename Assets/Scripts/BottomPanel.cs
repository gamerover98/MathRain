using System.Linq;
using TMPro;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int maxDigits = 5;

    private void Start()
    {
        inputField.Select();
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        inputField.onValidateInput += OnValidateInput;
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