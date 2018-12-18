using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Prop : MonoBehaviour {
    public BoxCollider boxCollider;
    public Rigidbody rb;
    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
}
