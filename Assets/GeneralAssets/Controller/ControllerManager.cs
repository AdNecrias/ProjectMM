using UnityEngine;
using System.Collections;
using System;

public enum ControlInfo {
    Dash, MouseMoved, Fire, FireAlt
}

public static class KeyBindings {
    //Movement
    ///Mouse&Keyboard
    public static KeyCode moveLeft = KeyCode.A;
    public static KeyCode moveRight = KeyCode.D;
    public static KeyCode moveUp = KeyCode.W;
    public static KeyCode moveDown = KeyCode.S;
    public static KeyCode moveWalk = KeyCode.LeftControl;
    public static KeyCode dash = KeyCode.LeftShift;
    public static KeyCode fire = KeyCode.Mouse0;
    public static KeyCode fireAlt = KeyCode.Mouse1;
    ///Controller
    public static KeyCode dashController = KeyCode.JoystickButton4; //L1
    public static string fireAxis = "Left Trigger";
    public static string fireAltAxis = "Right Trigger";

}

/// <summary>
/// version 0.1
/// </summary>
public class ControllerManager : MonoBehaviour {
    public static ControllerManager instance;
    
    Action<ControlInfo> keyPressed;

    public float MovementAxisX;
    public float MovementAxisY;
    public bool Dash;
    public bool FireHeld;
    public bool FireAltHeld;
    public bool FireTap;
    public bool FireAltTap;
    public float LookAxisX;
    public float LookAxisY;
    public Vector3 MouseLookPoint;

    Vector3 previousMousePosition;
    Plane mousePlane;
    Ray ray;

    public void RegisterKeyPressedCallback(Action<ControlInfo> callback) {
        keyPressed += callback;
    }

    public void UnregisterKeyPressedCallback(Action<ControlInfo> callback) {
        keyPressed -= callback;
    }

    public void singleton() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Use this for initialization
    void Awake () {
        singleton();
        mousePlane = new Plane(Vector3.up, Vector3.zero); // TODO Switch Vector3.zero to playerPosition? Do we even have that here?
	}
	
	// Update is called once per frame
	void Update () {
        // Movement
        /// Controller
        MovementAxisX = Input.GetAxis("Horizontal");
        MovementAxisY = Input.GetAxis("Vertical");

        /// Keyboard&Mouse
        float movementWalk = Input.GetKey(KeyBindings.moveWalk) ? 0.333f : 1f;

        if (Input.GetKey(KeyBindings.moveLeft)) {
            MovementAxisX -= movementWalk;
        }
        if (Input.GetKey(KeyBindings.moveRight)) {
            MovementAxisX += movementWalk;
        }
        if (Input.GetKey(KeyBindings.moveDown)) {
            MovementAxisY -= movementWalk;
        }
        if (Input.GetKey(KeyBindings.moveUp)) {
            MovementAxisY += movementWalk;
        }

        MovementAxisX = Mathf.Clamp(MovementAxisX, -1, 1);
        MovementAxisY = Mathf.Clamp(MovementAxisY, -1, 1);

        Dash = Input.GetKey(KeyBindings.dash) || Input.GetKey(KeyBindings.dashController);
        if(Dash && keyPressed != null)
            keyPressed(ControlInfo.Dash);

        // Look
        ///Controller
        LookAxisX = Input.GetAxis("LookStickX");
        LookAxisY = Input.GetAxis("LookStickY");
        ///Mouse
        if (previousMousePosition != Input.mousePosition) {
            keyPressed(ControlInfo.MouseMoved);
        }
        previousMousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (mousePlane.Raycast(ray, out hitdist)) {
            MouseLookPoint = ray.GetPoint(hitdist);
        }

        //Fire
        FireHeld = Input.GetKey(KeyBindings.fire) || Input.GetAxis(KeyBindings.fireAxis) > 0.1f;
        FireAltHeld = Input.GetKey(KeyBindings.fireAlt) || Input.GetAxis(KeyBindings.fireAltAxis) > 0.1f;
        FireTap = Input.GetKeyDown(KeyBindings.fire);
        FireAltTap = Input.GetKeyDown(KeyBindings.fireAlt);
        if (FireHeld && keyPressed != null) {
            keyPressed(ControlInfo.Fire);
        }
        if (FireAltHeld && keyPressed != null) {
            keyPressed(ControlInfo.FireAlt);
        }
    }
}
