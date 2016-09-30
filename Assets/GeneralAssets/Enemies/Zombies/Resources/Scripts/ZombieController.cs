using AdNecriasMeldowMethod;
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

        [SerializeField]
        [Tooltip("THIS IS TO BE DETERMINED BY THE PLAYER - USED FOR DEBUG TO CHOSE THE ZOMBIE ATTACK THREAT.")]
        private ThreatLevel threatLevel;


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
            if (target != null && IsPlayerNear() && CanAttackDueCooldown()) AttackTarget();
        }

        private void AttackTarget() {
            //do attack stuff
            mainAttackCooldown = 2;
            anim.SetTrigger("attack");   
            //Do not like this at all! 
            //This is to be done in the player, and he must select the correct threat level
            AMMPlayer.ExecuteOnEntityInteracted(GetComponent<AdNecriasMeldowMethod.Enemy>().Type, threatLevel);
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

        public bool IsPlayerNear() {
            return Vector3.Distance(transform.position, target.transform.position) <= 2;
        }
    }
}
