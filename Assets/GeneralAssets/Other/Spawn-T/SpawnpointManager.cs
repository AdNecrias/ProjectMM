using UnityEngine;

public class SpawnpointManager : MonoBehaviour {

    public GameObject ZerglinGameObject;
    public GameObject HydralyskGameObject;
    public GameObject UltralyskGameObject;

    public void SpawnZergling(int amt) { SpawnGameobject(ZerglinGameObject, amt); }
    public void SpawnHydralisk(int amt) { SpawnGameobject(HydralyskGameObject, amt); }
    public void SpawnUltralysk(int amt) { SpawnGameobject(UltralyskGameObject, amt); }
    public void KillAll() { foreach (var o in GameObject.FindGameObjectsWithTag("enemy")) { Destroy(o); } }

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
