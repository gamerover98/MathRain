using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance { get; private set; }

    [SerializeField] private GameObject gameGUICanvasObject;
    private Canvas gameGUICanvas;

    [SerializeField] private GameObject dropsContainer;

    private readonly List<Drop> spawnedDrops = new();
    public List<Drop> SpawnedDrops => new(spawnedDrops.AsReadOnly());

    [SerializeField] private GameObject dropObjectPrefab;
    [SerializeField] private int dropPoolSize = 10;
    [SerializeField] private List<DropData> dropDataList;

    public readonly Stack<Drop> DropPool = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        gameGUICanvas = gameGUICanvasObject.GetComponent<Canvas>();

        for (var i = 0; i < dropPoolSize; i++)
        {
            var dropObject =
                Instantiate(
                    dropObjectPrefab,
                    Vector3.zero,
                    Quaternion.identity,
                    dropsContainer.transform);
            dropObject.SetActive(false);

            DropPool.Push(dropObject.GetComponent<Drop>());
        }
    }

    [ProButton]
    public void SpawnDrop()
    {
        var scaleFactor = gameGUICanvas.scaleFactor;
        var rect = gameGUICanvas.GetComponent<RectTransform>().rect;

        var minX = (int)(Mathf.Abs(rect.xMin) * scaleFactor);
        var maxX = (int)(minX + Mathf.Abs(rect.xMax) * scaleFactor);
        var minY = (int)(Mathf.Abs(rect.yMin) * scaleFactor);
        var maxY = (int)(minY + Mathf.Abs(rect.yMax) * scaleFactor);

        var random = new Random();
        var dropData = dropDataList[random.Next(0, dropDataList.Count)];
        var positionX = (float)random.Next(200, maxX - 200);

        var position = new Vector2(positionX, maxY + 100 * scaleFactor);
        var spawnedDrop = GetDropFromPool(position, dropData);
        if (spawnedDrop != null) spawnedDrops.Add(spawnedDrop);
    }

    public void DespawnDrop(Drop drop)
    {
        drop.gameObject.SetActive(false);
        var dropTransform = drop.transform;

        dropTransform.position = new Vector2(0, 0);
        dropTransform.rotation = Quaternion.identity;
        
        DropPool.Push(drop);
        spawnedDrops.Remove(drop);
    }

    public void DespawnAllDrops()
    {
        foreach (var spawnedDrop in SpawnedDrops)
            DespawnDrop(spawnedDrop);
    }
    
    [CanBeNull]
    private Drop GetDropFromPool(Vector2 position, DropData dropData)
    {
        if (DropPool.Count <= 0) return null;

        var drop = DropPool.Pop();
        drop.Data = dropData;

        var dropObject = drop.gameObject;
        dropObject.transform.position = position;
        dropObject.transform.rotation = Quaternion.identity;
        dropObject.SetActive(true);
        return drop;
    }
}