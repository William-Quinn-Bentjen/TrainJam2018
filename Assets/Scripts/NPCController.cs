using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour {
    public NavMeshAgent agent;
    public GameObject target;
    public float meanWaitTime;
    [Range(0,1)]
    public float variance = .5f;
    [System.Serializable]
    public struct Materials
    {
        public Material Alive;
        public Material Dead;
        public SkinnedMeshRenderer meshRenderer;
    }
    public Materials materials;
    public delegate void OnDeath(NPCController controller);
    public OnDeath onDeath;
    public bool _alive = true;
    public GameObject Ragdoll;
    public Rigidbody rigidBody;
    public TransformManager transformManager;
    public bool Alive
    {
        get
        {
            return _alive;
        }
        set
        {
            if (value)
            {
                AliveMaterial();
                _alive = true;
            }
            else if (_alive)
            {
                _alive = false;
            }
        }
    }
    [ContextMenu("Fix material to be alive texture")]
    public void AliveMaterial()
    {
        materials.meshRenderer.material = materials.Alive;
    }
    private void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
        materials.meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materials.meshRenderer.material = materials.Alive;
        transformManager = GetComponent<TransformManager>();
        rigidBody = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start () {
        Alive = true;
        StartWander();
	}
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == target)
        {
            StartCoroutine(Wait(meanWaitTime * Random.Range(-variance, variance)));
        }
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(Mathf.Abs(time));
        StartWander();
        Die();
    }
    public void StartWander()
    {
        StopAllCoroutines();
        target = NPCWanderPoint.wanderPoints[Random.Range(0, NPCWanderPoint.wanderPoints.Length)].gameObject;
        agent.destination = target.transform.position;
    }
    public void Die()
    {
        Alive = false;
        if (onDeath != null) onDeath(this);
        GameObject ragdoll = Instantiate(Ragdoll);
        ragdoll.GetComponentInChildren<SkinnedMeshRenderer>().material = materials.Dead;
        ragdoll.GetComponent<TransformManager>().MatchTransforms(transformManager);
        Rigidbody rb = ragdoll.GetComponent<Rigidbody>();
        rb.velocity = rigidBody.velocity;
        rb.angularVelocity = rigidBody.angularVelocity;
        Destroy(gameObject);
    }
}
