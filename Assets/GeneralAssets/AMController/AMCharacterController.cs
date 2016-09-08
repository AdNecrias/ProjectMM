using UnityEngine;
using UnityEditor;
using System.Collections;

//TODO Make a AMCharacterControllerCameraAligned version of this controller which aligns it's movement axis to the main camera.
public class AMCharacterController : MonoBehaviour {
    public float dragCoefficient = .9f;
    public float accelerationCoefficient = 1f;
    public float dashAmmount = 15f;
    public float dashCooldown = 1.5f;
    float dashCooldownTimer;

    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    Vector2 acceleration;
    [SerializeField]
    Vector2 lookDirection;

    bool dash = false;
    bool useMouseLookPoint = false;

    GameObject lookTarget;

    void Start() {
        velocity = new Vector2();

        AMControllerManager.instance.RegisterKeyPressedCallback(OnDashKeyPressed);
        AMControllerManager.instance.RegisterKeyPressedCallback(OnMouseMoved);

        lookTarget = new GameObject();
        lookTarget.name = this.name + "_LookTarget";
        lookTarget.transform.parent = this.transform;
        lookTarget.transform.position = this.transform.forward + this.transform.position;
    }

    void Update () {
        UpdateMovement();
        UpdateLook();

        //TODO Remove me.
        MaterialTest();
	}

    void UpdateLook() {
        lookDirection = new Vector2(AMControllerManager.instance.LookAxisX, -AMControllerManager.instance.LookAxisY).normalized;
        if (useMouseLookPoint) {
            useMouseLookPoint = false;
            lookDirection = new Vector2(AMControllerManager.instance.MouseLookPoint.x, AMControllerManager.instance.MouseLookPoint.z);

        }

        // TODO Smooth look target movement?
        lookTarget.transform.position =
                new Vector3(this.transform.position.x+lookDirection.x, this.transform.position.y, this.transform.position.z + lookDirection.y);
            this.transform.LookAt(lookTarget.transform);
    }

    void UpdateMovement() {
        acceleration = new Vector2(AMControllerManager.instance.MovementAxisX, AMControllerManager.instance.MovementAxisY);

        velocity *= dragCoefficient;
        velocity += acceleration * accelerationCoefficient;

        if (dash && Time.time > dashCooldownTimer) {
            dashCooldownTimer = Time.time + dashCooldown;
            velocity += acceleration.normalized * dashAmmount;
        }
        dash = false; // Put false so when dash cd ends dash isn't at true without the key being pressed.

        this.transform.position = new Vector3(this.transform.position.x + (velocity.x) * Time.deltaTime, this.transform.position.y, this.transform.position.z + (velocity.y) * Time.deltaTime);
    }

    void MaterialTest() {
        float expectedTopSpeed = (1 * accelerationCoefficient) / (1 -  dragCoefficient);
        expectedTopSpeed += (1-dragCoefficient) * expectedTopSpeed;
        if(velocity.sqrMagnitude > expectedTopSpeed * expectedTopSpeed) {
            Debug.Log("Speedy: " + velocity.magnitude);
            this.GetComponent<Renderer>().material.SetColor("_AM_NSOutline", Color.red);
        } else {
            this.GetComponent<Renderer>().material.SetColor("_AM_NSOutline", Color.cyan);
        }
    }

    void OnDashKeyPressed(ControlInfo info) {
        if (info == ControlInfo.Dash) dash = true;
    }
    void OnMouseMoved(ControlInfo info) {
        if (info == ControlInfo.MouseMoved) {
            useMouseLookPoint = true;
        }
    }
}
