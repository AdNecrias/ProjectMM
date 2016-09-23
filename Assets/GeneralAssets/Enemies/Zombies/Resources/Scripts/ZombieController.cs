using UnityEngine;

namespace MainGame {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class ZombieController : Enemy {

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
            base.Update();
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
            if (other.tag == Utils.instance.PlayerTag) {
                target = other.gameObject;
            }
        }


        public override void ReceiveDamage(float dmg) {
            HP -= dmg;
        }

        public override void Death() {
            Destroy(gameObject);
        }
    }
}
