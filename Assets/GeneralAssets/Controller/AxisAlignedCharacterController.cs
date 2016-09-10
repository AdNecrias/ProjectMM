using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MainGame {
    
    public class AxisAlignedCharacterController : MonoBehaviour {
        float sin45 = Mathf.Sin(Mathf.PI / 2);
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
        bool fire = false;
        bool fireAlt = false;
        bool useMouseLookPoint = false;

        GameObject lookTarget;

        void Start() {
            velocity = new Vector2();

            ControllerManager.instance.RegisterKeyPressedCallback(OnDashKeyPressed);
            ControllerManager.instance.RegisterKeyPressedCallback(OnMouseMoved);
            ControllerManager.instance.RegisterKeyPressedCallback(OnFireKeyPressed);

            lookTarget = new GameObject();
            lookTarget.name = this.name + "_LookTarget";
            lookTarget.transform.parent = this.transform;
            lookTarget.transform.position = this.transform.forward + this.transform.position;
        }

        void Update() {
            UpdateMovement();
            UpdateLook();

            UpdateFire();
        }

        void FireTest() {
            GameObject go = new GameObject(this.transform.name + "_bullet_Test");
            MeshFilter meshFilter = go.AddComponent<MeshFilter>();
            meshFilter.mesh = this.gameObject.GetComponent<MeshFilter>().mesh;
            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            renderer.material = this.gameObject.GetComponent<MeshRenderer>().material;
            go.transform.position = this.transform.position + this.transform.forward*2;
            go.transform.rotation = this.transform.rotation;
            Destroy(go, 1f);
        }

        void UpdateFire() {
            if(fire) {
                Debug.Log("Fire Held command - Received");
                FireTest();
            }
            if (fireAlt) {
                Debug.Log("FireAlt Held command - Received");
            }
            fire = false;
            fireAlt = false;
        }

        void UpdateLook() {
            lookDirection = new Vector2(ControllerManager.instance.LookAxisX, -ControllerManager.instance.LookAxisY).normalized;
            lookTarget.transform.position =
                    new Vector3(this.transform.position.x + lookDirection.x, this.transform.position.y, this.transform.position.z + lookDirection.y);
            if (useMouseLookPoint) {
                useMouseLookPoint = false;
                lookDirection = new Vector2(ControllerManager.instance.MouseLookPoint.x, ControllerManager.instance.MouseLookPoint.z);

                lookTarget.transform.position = new Vector3(lookDirection.x, this.transform.position.y, lookDirection.y);
            }

            // TODO Smooth look target movement?
            this.transform.LookAt(lookTarget.transform);
        }

        Vector2 axisAlign(Vector2 input) {
            // TODO: if camera stops being isometric this wont work
            return new Vector2((input.y + input.x) * sin45, (input.y - input.x) * sin45);
        }

        void UpdateMovement() {
            acceleration = new Vector2(ControllerManager.instance.MovementAxisX, ControllerManager.instance.MovementAxisY);
            acceleration = axisAlign(acceleration);

            velocity *= dragCoefficient;
            velocity += acceleration * accelerationCoefficient;

            if (dash && Time.time > dashCooldownTimer) {
                dashCooldownTimer = Time.time + dashCooldown;
                velocity += acceleration.normalized * dashAmmount;
            }
            dash = false; // Put false so when dash cd ends dash isn't at true without the key being pressed.

            this.transform.position = new Vector3(this.transform.position.x + (velocity.x) * Time.deltaTime, this.transform.position.y, this.transform.position.z + (velocity.y) * Time.deltaTime);
        }

        void OnDashKeyPressed(ControlInfo info) {
            if (info == ControlInfo.Dash) dash = true;
        }
        void OnMouseMoved(ControlInfo info) {
            if (info == ControlInfo.MouseMoved) {
                useMouseLookPoint = true;
            }
        }
        void OnFireKeyPressed(ControlInfo info) {
            if (info == ControlInfo.Fire) fire = true;
            if (info == ControlInfo.FireAlt) fireAlt = true;
        }
    }

}
