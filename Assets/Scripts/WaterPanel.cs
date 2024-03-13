using System;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class WaterPanel : MonoBehaviour
{
    [SerializeField] private int waterHeight = 100;
    [SerializeField] private List<WaterLevel> waterLevel;

    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start() => UpdateWaterLevel(0);

    private void Update()
    {
        if (rectTransform.hasChanged)
        {
            UpdateBoxCollider();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent<Drop>(out var drop);
        if (drop == null) return;

        drop.Splash();
        GameManager.Instance.WaterLevel += 1;
    }

    [ProButton]
    public void UpdateWaterLevel(int level)
    {
        if (waterLevel.Count == 0)
        {
            Debug.Log("The water level list is empty");
            return;
        }

        rectTransform.sizeDelta =
            new Vector2(
                rectTransform.sizeDelta.x,
                waterHeight + waterHeight / 2F * level);
    }

    private void UpdateBoxCollider()
    {
        var anchoredPosition = rectTransform.anchoredPosition;
        var sizeDelta = rectTransform.sizeDelta;

        boxCollider.size = sizeDelta;
        boxCollider.offset = 
            new Vector2(anchoredPosition.x,
                anchoredPosition.y + sizeDelta.y / 2F);
    }
}

[Serializable]
public class WaterLevel
{
    public int level;
    public GameObject gameObject;
}