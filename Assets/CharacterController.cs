using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public Rigidbody rb;
    public Collider Collider;
    public Transform holdPosition;
    public float moveSpeed = 10;
    public ForceMode moveType = ForceMode.Impulse;
    public float sprintModifier = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * moveSpeed);
        if (Input.GetButton("Fire2"))
        {
            //pickup/drop
        }
        else if (Input.GetButton("Fire1"))
        {
            //interact/use
        }
        if (Input.GetButton("Fire3"))
        {
            //sprint
            movement *= sprintModifier;
        }
        rb.AddForce(movement, moveType);
	}
    public PickUp(GameObject toBeHeld)
    {
        toBeHeld.transform.parent = holdPosition;
        toBeHeld.transform.localPosition = Vector3.zero;
        toBeHeld.transform.localRotation = Quaternion.identity;
        toBeHeld.GetComponent<Rigidbody>().isKinematic = true;
        Rigidbody HeldRb = held.GetComponent<Rigidbody>();
        HeldRb.isKinematic = false;
        HeldRb.AddForce(transform.forward * 30, ForceMode.Impulse);
        HeldRb.transform.parent = null;
        held = toBeHeld;
    }
    public GameObject held;
}
