using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent<Drop>(out var drop);
        if (drop == null) return;

        drop.Splash();
    }
}