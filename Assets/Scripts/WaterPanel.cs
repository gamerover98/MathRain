using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterPanel : MonoBehaviour
{
    [SerializeField] private List<WaterLevel> waterLevel;

    private void Start()
    {
        UpdateWaterLevel(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent<Drop>(out var drop);
        if (drop == null) return;

        drop.Splash();
        GameManager.Instance.WaterLevel += 1;
    }

    public void UpdateWaterLevel(int level)
    {
        if (waterLevel.Count == 0)
        {
            Debug.Log("The water level list is empty");
            return;
        }

        var wl = waterLevel.FirstOrDefault(current => current.level == level);

        if (wl == null)
        {
            Debug.Log($"Undefined water level: {level}");
            wl = waterLevel.OrderByDescending(current => current.level).FirstOrDefault();
        }

        var waterLevelObject = wl!.gameObject;
        var wlTransform = waterLevelObject.transform as RectTransform;
        var height = (wlTransform!.position.y + wlTransform.rect.height);
        var panelTransform = transform as RectTransform;

        panelTransform!.position =
            new Vector2(
                panelTransform.position.x,
                -panelTransform.rect.height + height);
    }
}

[Serializable]
public class WaterLevel
{
    public int level;
    public GameObject gameObject;
}