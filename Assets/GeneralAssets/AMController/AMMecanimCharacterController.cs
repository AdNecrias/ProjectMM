using System;
using UnityEngine;
using System.Collections;

public class AMMecanimCharacterController : MonoBehaviour {
    float sin45 = Mathf.Sin(Mathf.PI / 2);

    private Animator anim;
    private int velXHash;
    private int velZHash;
    private int dashHash;
    private int swapWeaponHash;
    private int fireHash;

    private bool dash = false;
    private bool fire = false;
    private bool swapWeapon = false;
    private bool useMouseLookPoint = false;

    [SerializeField]
    private Vector2 lookDirection;
    [SerializeField]
    private Vector2 moveDirection;

    [SerializeField]
    private GameObject bulletSpawnpoint;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private bool isAimming = false;
    private bool isMoving = false;

    // Use this for initialization
    void Start() {
        anim = GetComponentInChildren<Animator>();
        velXHash = Animator.StringToHash("vel_X");
        velZHash = Animator.StringToHash("vel_Z");
        dashHash = Animator.StringToHash("dash");
        fireHash = Animator.StringToHash("fire_rifle");
        swapWeaponHash = Animator.StringToHash("swap_weapon");

        AMControllerManager.instance.RegisterKeyPressedCallback(OnKeyPressed);
        AMControllerManager.instance.RegisterKeyReleasedCallback(OnKeyReleased);
        AMControllerManager.instance.RegisterKeyPressedCallback(OnMouseMoved);

        if (bulletSpawnpoint == null) Debug.LogError("No Bullet Spawnpoint defined.");
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
        if (fire) UpdateFire();
        if (swapWeapon) UpdateSwapWeapon();
    }

    private void UpdateMovement() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("locomotion")) {
            lookDirection = new Vector2(AMControllerManager.instance.LookAxisX, -AMControllerManager.instance.LookAxisY);
            moveDirection = new Vector2(AMControllerManager.instance.MovementAxisX, AMControllerManager.instance.MovementAxisY);
            //lookDirection = axisAlign(lookDirection);
            
            isMoving = moveDirection.magnitude > 0.1;
            isAimming = lookDirection.magnitude > 0.25;

            if (isAimming) {
                AimmingMovement();
            } else {
                GenericMovement();
            }
        }
    }

    #region Movement
    //Non Aimming movement
    private void GenericMovement() {
        if (isMoving) {
            transform.forward = new Vector3(moveDirection.x, 0, moveDirection.y);
        }

        anim.SetFloat(velZHash,
            Mathf.Sqrt(
                Mathf.Pow(moveDirection.x, 2) +
                Mathf.Pow(moveDirection.y, 2)
                )
        );

        if (dash) UpdateMovementDash();
    }

    //Aimming Movement
    private void AimmingMovement() {
        transform.forward = new Vector3(lookDirection.x, 0, lookDirection.y);

        var angl = Vector2.Angle(new Vector2(0, 1), lookDirection);
        //var final = Quaternion.Euler(0, angl, 0) * moveDirection;
        if(lookDirection.x < 0) {
            angl = 360 - angl;
        }
        var final = Rotate(moveDirection, angl);

        Debug.Log( angl + " "+ final);

        anim.SetFloat(velZHash, final.y);
        anim.SetFloat(velXHash, final.x);
    }

    #endregion

    //Other
    private void UpdateMovementDash() {
        anim.SetTrigger(dashHash);
        dash = false;
    }

    private void UpdateFire() {
        anim.SetBool(fireHash, true);
        fire = false;
    }

    private void UpdateSwapWeapon() {
        swapWeapon = false;
    }

    Vector2 axisAlign(Vector2 input, float rad) {
        // TODO: if camera stops being isometric this wont work
        return new Vector2((input.y + input.x) * sin45, (input.y - input.x) * sin45);
    }

    public Vector2 Rotate(this Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    private void OnKeyPressed(ControlInfo info) {
        if (info == ControlInfo.Dash) dash = true;
        if (info == ControlInfo.Fire) fire = true;
        if (info == ControlInfo.SwapWeapon) swapWeapon = true;
    }

    private void OnKeyReleased(ControlInfo info) {
        if (info == ControlInfo.FireReleased) anim.SetBool(fireHash, false);
    }

    private void OnMouseMoved(ControlInfo info) {
        if (info == ControlInfo.MouseMoved) {
            useMouseLookPoint = true;
        }
    }

    //This is called by an animation event... don't ask
    public void FireTrigger() {
        Instantiate(bullet, bulletSpawnpoint.transform.position, transform.rotation);
    }
}
