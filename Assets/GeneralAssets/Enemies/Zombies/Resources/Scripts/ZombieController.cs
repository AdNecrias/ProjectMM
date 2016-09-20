﻿using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ZombieController : MonoBehaviour {

    public GameObject target;
    private NavMeshAgent agent;
    private Animator anim;

    private int velocityHash;

    private Vector3 previousPosition;
    public float curSpeed;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        velocityHash = Animator.StringToHash("velocity");
    }

    // Update is called once per frame
    void Update() {
        ChasePlayer();


    }

    private void ChasePlayer() {
        if (target != null) {
            agent.SetDestination(target.transform.position);
            anim.SetFloat(velocityHash, anim.velocity.magnitude);

            Vector3 curMove = transform.position - previousPosition;
            curSpeed = curMove.magnitude / Time.deltaTime;
            previousPosition = transform.position;
        }

    }

    void OnTriggerEnter(Collider other) {
        if (Utils.instance.isPlayer(other.gameObject)) {
            target = other.gameObject;
        }

    }
}
