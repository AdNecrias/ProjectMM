using UnityEngine;
using System.Collections;

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
        if (Utils.instance.isEnemy(other.gameObject)) {
            other.GetComponentInParent<Enemy>().ReceiveDamage(damage);
        }
    }
}
