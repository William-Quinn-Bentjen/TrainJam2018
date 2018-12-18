using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class NPCWanderPoint : MonoBehaviour {
    public SphereCollider Collider;
    public int index = -1;
    public static NPCWanderPoint[] wanderPoints = new NPCWanderPoint[0] { };
    private void Reset()
    {
        Collider = GetComponent<SphereCollider>();
        Collider.isTrigger = true;
    }
    // Use this for initialization
    void Awake () {
		if (wanderPoints.Length == 0)
        {
            wanderPoints = FindObjectsOfType<NPCWanderPoint>();
            for(int i = 0; i < wanderPoints.Length; i++)
            {
                wanderPoints[i].index = i;
            }
        }
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(transform.position, Collider.radius);
    }
}
