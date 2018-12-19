using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Pickup : MonoBehaviour {
    public Collider Collider;
    public Rigidbody rb;
    public float throwForce = 20;
    public float distanceFromFace = .5f;
    public virtual void Use()
    {

    }
    public virtual void OnPickup(Transform hand)
    {
        Collider.enabled = false;
        transform.parent = hand;
        transform.position = hand.position;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
        
    }
    public virtual void OnDrop()
    {
        transform.position = transform.position + (transform.forward * distanceFromFace);
        Collider.enabled = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        rb.transform.parent = null;
    }
    private void Reset()
    {
        Collider[] cols = GetComponents<Collider>();
        for(int i = 0; i < cols.Length; i++)
        {
            Destroy(cols[i]);
        }
        Collider = gameObject.AddComponent<MeshCollider>();
        (Collider as MeshCollider).convex = true;
        rb = GetComponent<Rigidbody>();
    }
    public void ResetMe()
    {
        Reset();
    }
}
