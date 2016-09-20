using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public float velocity;
    public float lifeTime = 3.0f;
    // Update is called once per frame
    void Update() {
        transform.position += transform.forward * velocity * Time.deltaTime;
        transform.position += transform.forward * velocity * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
}
