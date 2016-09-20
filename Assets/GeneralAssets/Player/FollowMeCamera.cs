using UnityEngine;
using System.Collections;

/// <summary>
/// This script tells the main camera DIRECTLY to follow this game object.
/// </summary>
//TODO maybe create a script to focus several game objects?

public class FollowMeCamera : MonoBehaviour {
    public Transform target;
    void Update() {
        //target.position = transform.position;
        target.position = Vector3.Lerp(target.position, transform.position, 0.1f);
    }
}
