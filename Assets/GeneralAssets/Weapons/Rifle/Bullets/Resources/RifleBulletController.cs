using UnityEngine;

namespace MainGame {
    public class RifleBulletController : MonoBehaviour {
        public float velocity;
        public float lifeTime = 3.0f;
        public float damage = 1.0f;

        // Update is called once per frame
        void Update() {
            transform.position += transform.forward * velocity * Time.deltaTime;
            transform.position += transform.forward * velocity * Time.deltaTime;
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0) Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other) {
            if (other.tag == Utils.instance.EnemyTag) {
                other.GetComponentInParent<Enemy>().ReceiveDamage(damage);
            }
        }
    }
}
