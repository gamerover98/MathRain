using TMPro;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        inputField.Select();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent<Drop>(out var drop);
        if (drop == null) return;

        drop.Splash();
    }
}