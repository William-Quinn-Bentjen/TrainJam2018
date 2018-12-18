using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Shelf : MonoBehaviour {
    public List<Transform> spawnPoints = new List<Transform>();
    private void Reset()
    {
        foreach(Transform spawn in transform)
        {
            spawnPoints.Add(spawn);
        }
    }    
    // Use this for initialization
    void Awake () {
        ShelfItems.Instance.PopulateShelf(this);
	}
    private void OnDrawGizmos()
    {
        foreach(Transform spawn in spawnPoints)
        {
            if (spawn != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(spawn.position, spawn.position + new Vector3(0, -ShelfItems.Instance.snapDistance, 0));
            }
        }
    }
}
