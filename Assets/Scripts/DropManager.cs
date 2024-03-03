using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

public class DropManager : MonoBehaviour
{
    public static DropManager Instance { get; private set; }

    [SerializeField] private GameObject gameGUICanvas;

    public IEnumerable<Drop> SpawnedDrops =>
        gameGUICanvas.GetComponentsInChildren<Drop>();

    [SerializeField] private GameObject dropObjectPrefab;
    [SerializeField] private int dropPoolSize = 10;
    [SerializeField] private List<DropData> dropDataList;

    public readonly Stack<Drop> DropPool = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        for (var i = 0; i < dropPoolSize; i++)
        {
            var dropObject =
                Instantiate(
                    dropObjectPrefab,
                    Vector3.zero,
                    Quaternion.identity,
                    gameGUICanvas.transform);
            dropObject.SetActive(false);

            DropPool.Push(dropObject.GetComponent<Drop>());
        }
    }

    [ProButton]
    public void SpawnDrop()
    {
        var random = new Random();
        var dropData = dropDataList[random.Next(0, dropDataList.Count)];
        var position = new Vector2(random.Next(150, 1920 - 150), 1100);
        GetDropFromPool(position, dropData);
    }

    [CanBeNull]
    public Drop GetDropFromPool(Vector2 position, DropData dropData)
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

    public void ReturnObjectToPool(Drop drop)
    {
        drop.gameObject.SetActive(false);
        DropPool.Push(drop);
    }
}