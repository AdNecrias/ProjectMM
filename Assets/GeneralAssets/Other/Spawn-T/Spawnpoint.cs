using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public GameObject Enemy;

    public void SpawnEnemy(GameObject go) {
        var randomAngle = Random.Range(0.0f, 359.0f);
        transform.Rotate(0.0f, randomAngle, 0.0f);
        Enemy = (GameObject)Instantiate(go, transform.position, transform.rotation);
    }
}
