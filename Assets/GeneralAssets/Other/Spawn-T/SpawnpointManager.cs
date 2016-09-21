using UnityEngine;

public class SpawnpointManager : MonoBehaviour {

    public GameObject ZombieDefaultGameObject;
    public GameObject ZombieFastGameObject;
    public GameObject ZombieTankGameObject;

    public void SpawnZombieDefault(int amt) { SpawnGameobject(ZombieDefaultGameObject, amt); }
    public void SpawnZombieFast(int amt) { SpawnGameobject(ZombieFastGameObject, amt); }
    public void SpawnZombieTank(int amt) { SpawnGameobject(ZombieTankGameObject, amt); }
    public void KillAll() { foreach (var o in GameObject.FindGameObjectsWithTag("enemy")) { Destroy(o.transform.parent.gameObject); } }

    private void SpawnGameobject(GameObject go, int amt) {
        var spawnpoints = GameObject.FindGameObjectsWithTag("spawnpoint");
        var i = 0;
        foreach (var spawnpoint in spawnpoints) {
            var scrpt = spawnpoint.GetComponent<Spawnpoint>();
            if (scrpt.Enemy == null) {
                scrpt.SpawnEnemy(go);
                i++;
                if (i >= amt) return;
            }
        }
        Debug.LogError("Trying to spawn Enemy, but no available spawn was found.");
    }
}
