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
    public float walkSpeedCap = 3;
    public float runSpeedCap = 6;
    public float moveDeadZone = .1f;
    public float pickupRadius = 1;
    public Pickup held;
    public bool enableInput = true;
    public Camera main;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float lookDeadZone = 0.00001f;
    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        if (enableInput)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            yaw += speedH * x;
            if (Mathf.Abs(y) > lookDeadZone) pitch -= speedV * y;
            main.transform.eulerAngles = new Vector3(pitch, 0, 0.0f);
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, yaw, 0);

            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red, 3);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")  * moveSpeed);
            bool sprinting = false;
            if (Input.GetButtonDown("Fire2"))
            {
                //pickup/drop
                AttemptPickUp();
            }
            else if (Input.GetButton("Fire1"))
            {
                if (held != null) held.Use();
                //interact/use
            }
            if (Input.GetButton("Fire3"))
            {
                //sprint
                movement *= sprintModifier;
                sprinting = true;
            }
            if (movement.magnitude > moveDeadZone)
            {
                if (sprinting && rb.velocity.magnitude < runSpeedCap)
                {
                    ;
                    rb.AddForce(main.transform.TransformVector(movement), moveType);
                }
                else if (!sprinting && rb.velocity.magnitude < walkSpeedCap)
                {
                    rb.AddForce(main.transform.TransformVector(movement), moveType);
                }
            }
        }
    }
    public void PickUp(Pickup toBeHeld)
    {
        if (held != null) held.OnDrop();
        toBeHeld.OnPickup(holdPosition);
        held = toBeHeld;
    }
    public void AttemptPickUp()
    {
        SortedList<float,Pickup> pickups = new SortedList<float,Pickup>();
        foreach(Collider col in Physics.OverlapSphere(transform.position, pickupRadius))
        {
            Pickup test = col.GetComponent<Pickup>();
            if (test != null) pickups.Add(Vector3.Distance(test.transform.position, transform.position),test);
        }
        if (pickups.Count != 0)
        {
            foreach(float dist in pickups.Keys)
            {
                PickUp(pickups[dist]);
                return;
            }
        }
        if (held != null)
        {
            held.OnDrop();
            held = null;
        }


    }
}