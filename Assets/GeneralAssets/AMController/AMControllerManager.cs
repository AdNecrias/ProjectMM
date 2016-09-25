using UnityEngine;
using System.Collections;
using System;
using System.Net;

public enum ControlInfo {
    Dash, MouseMoved, Fire, FireAlt, FireReleased, SwapWeapon
}

public static class KeyBindings {
    //Movement
    ///Mouse&Keyboard
    public static KeyCode moveLeft = KeyCode.A;
    public static KeyCode moveRight = KeyCode.D;
    public static KeyCode moveUp = KeyCode.W;
    public static KeyCode moveDown = KeyCode.S;
    public static KeyCode moveWalk = KeyCode.LeftControl;
    public static KeyCode fire = KeyCode.Mouse0;
    public static KeyCode dash = KeyCode.LeftShift;
    ///Controller
    public static KeyCode dashController = KeyCode.JoystickButton0; //A
    public static KeyCode swapWeaponController = KeyCode.Joystick1Button3;  //Y
    public static string fireAxis = "RightTrigger";
    public static string fireAltAxis = "LeftTrigger";
    public static string lookAxisX = "LookStickX";
    public static string lookAxisY = "LookStickY";
    public static string movementAxisX = "Horizontal";
    public static string movementAxisY = "Vertical";

}

/// <summary>
/// version 0.1
/// </summary>
public class AMControllerManager : MonoBehaviour {
    public static AMControllerManager instance;

    Action<ControlInfo> keyPressed, keyReleased;
    public float MovementAxisX, MovementAxisY;
    public float LookAxisX, LookAxisY;
    public bool Dash;
    public bool FireHeld, FireAltHeld, SwapWeapon;
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

    public void RegisterKeyReleasedCallback(Action<ControlInfo> callback) {
        keyReleased += callback;
    }

    public void UnregisterKeyReleasedCallback(Action<ControlInfo> callback) {
        keyReleased -= callback;
    }

    public void singleton() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Use this for initialization
    void Awake() {
        singleton();
        mousePlane = new Plane(Vector3.up, Vector3.zero); // TODO Switch Vector3.zero to playerPosition? Do we even have that here?
    }

    // Update is called once per frame
    void Update() {
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

        //Dash
        Dash = Input.GetKeyDown(KeyBindings.dash) || Input.GetKeyDown(KeyBindings.dashController);
        if (Dash && keyPressed != null) keyPressed(ControlInfo.Dash);

        //Fire
        FireHeld = Input.GetAxis(KeyBindings.fireAxis) > 0.1f;
        FireAltHeld = Input.GetAxis(KeyBindings.fireAltAxis) > 0.1f;
        if (FireHeld && keyPressed != null) {
            keyPressed(ControlInfo.Fire);
        }
        if (FireAltHeld && keyPressed != null) {
            keyPressed(ControlInfo.FireAlt);
        }

        //Swap Weapon
        SwapWeapon = Input.GetKey(KeyBindings.swapWeaponController);
        if (SwapWeapon) keyPressed(ControlInfo.SwapWeapon);

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
    }
}
