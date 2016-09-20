using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject followTarget;

    // Update is called once per frame
    void Update() {
        transform.position = followTarget.transform.position;
    }
}
