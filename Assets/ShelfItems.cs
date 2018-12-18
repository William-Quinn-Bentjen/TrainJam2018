using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shelf Props", menuName = "ShelfProps")]
public class ShelfItems : ScriptableObject {
    public static ShelfItems Instance;
    [System.Serializable]
    public class ShelfItemSpawnRate
    {
        public Prop Prop;
        [Range(0,1)]
        public int SpawnRate = 1;
    }
    public List<ShelfItemSpawnRate> Props = new List<ShelfItemSpawnRate>();
    [Range(0,1)]
    public float spawnRate = .7f;
    public float snapDistance = 1;
    public int totalWeight;
    [ContextMenu("Bake Prop Spawn Rate")]
    public void BakePropSpawnWeight()
    {
        totalWeight = 0;
        foreach(ShelfItemSpawnRate rate in Props)
        {
            totalWeight += rate.SpawnRate;
        }
    }
    private void OnEnable()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Shelf Items already exists in assets");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public ShelfItemSpawnRate GetProp()
    {
        int rand = Random.Range(0, totalWeight+1);
        for (int i = 0; i < Props.Count; i++)
        {
            rand -= Props[i].SpawnRate;
            if (rand <= 0)
            {
                return Props[i];
            }
        }
        // should never reach here
        Debug.LogError("Prop Spawn Failed");
        return new ShelfItemSpawnRate();
    }
    public GameObject SpawnProp(Vector3 pos, Quaternion rotation, Prop shelfItem, Transform parent = null)
    {
        GameObject prop = Instantiate(shelfItem.gameObject, pos, rotation, parent);
        prop.SetActive(false);
        new Ray(pos, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(new Ray(pos, Vector3.down), out hit, 1))
        {
            Vector3 extents = new Vector3(shelfItem.boxCollider.size.x * prop.transform.lossyScale.x, shelfItem.boxCollider.size.y * prop.transform.lossyScale.y, shelfItem.boxCollider.size.z * prop.transform.lossyScale.z);
            Vector3 newPos = hit.point + new Vector3(0, (extents.y / 2), 0);
            Collider[] hits = Physics.OverlapBox(newPos, extents / 2, prop.transform.rotation);
            if (hits.Length == 0)
            {
                prop.transform.position = newPos;
            }
        }
        prop.SetActive(true);
        return prop;
    }
    public void PopulateShelf(Shelf shelf)
    { 
        foreach(Transform spawn in shelf.spawnPoints)
        {
            if (spawn != null && Random.Range(0f,1f) < spawnRate)
            {
                SpawnProp(spawn.position, spawn.rotation, GetProp().Prop);
            }
        }

    }
}
