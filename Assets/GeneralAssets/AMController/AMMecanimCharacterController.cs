using System;
using UnityEngine;
using System.Collections;

public class AMMecanimCharacterController : MonoBehaviour {

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

    private GameObject lookTarget;
    [SerializeField]
    private GameObject bulletSpawnpoint;
    [SerializeField]
    private GameObject bullet;

    private bool isAimming = false;

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

        lookTarget = new GameObject();
        lookTarget.name = this.name + "_LookTarget";
        lookTarget.transform.parent = this.transform;
        lookTarget.transform.position = this.transform.forward + this.transform.position;

        if (bulletSpawnpoint == null) Debug.LogError("No Bullet Spawnpoint defined.");
    }

    // Update is called once per frame
    void Update() {
        UpdateMovement();
    }

    private void UpdateMovement() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("locomotion")) {
            lookDirection =
                new Vector2(AMControllerManager.instance.LookAxisX, -AMControllerManager.instance.LookAxisY).normalized;
            moveDirection =
                new Vector2(AMControllerManager.instance.MovementAxisX, -AMControllerManager.instance.MovementAxisY).normalized;


            if (isAimming) {
                //UpdateLook();
            } else {
                //AimmingMovement();
            }
        }
    }

    #region Movement
    //Non Aimming movement
    private void AimmingMovement() {
        //looks into left trigger direction
        lookDirection = new Vector2(AMControllerManager.instance.MovementAxisX, AMControllerManager.instance.MovementAxisY).normalized;

        lookTarget.transform.position =
                new Vector3(
                        transform.position.x + lookDirection.x,
                        transform.position.y,
                        transform.position.z + lookDirection.y
                    );
        transform.LookAt(lookTarget.transform);
        //sets mecanim parameters and moves in that direction
        anim.SetFloat(velZHash,
            Mathf.Sqrt(
                Mathf.Pow(AMControllerManager.instance.MovementAxisX, 2) +
                Mathf.Pow(AMControllerManager.instance.MovementAxisY, 2)
                )
            );

        if (dash) UpdateMovementDash();
        if (fire) UpdateMovementFire();
        if (swapWeapon) UpdateSwapWeapon();
    }

    //Aimming Movement
    private void UpdateLook() {
        Debug.Log("aimming");
        //lookDirection = new Vector2(AMControllerManager.instance.LookAxisX, -AMControllerManager.instance.LookAxisY).normalized;
        //if (useMouseLookPoint) {
        //    useMouseLookPoint = false;
        //    lookDirection = new Vector2(AMControllerManager.instance.MouseLookPoint.x, AMControllerManager.instance.MouseLookPoint.z);

        //}

        //// TODO Smooth look target movement?
        //lookTarget.transform.position =
        //        new Vector3(this.transform.position.x + lookDirection.x, this.transform.position.y, this.transform.position.z + lookDirection.y);
        //this.transform.LookAt(lookTarget.transform);
    }

    #endregion

    //Other
    private void UpdateMovementDash() {
        anim.SetTrigger(dashHash);
        dash = false;
    }

    private void UpdateMovementFire() {
        anim.SetBool(fireHash, true);
        fire = false;
    }

    private void UpdateSwapWeapon() {
        swapWeapon = false;
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
