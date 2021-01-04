using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float speed = 6f;
    float gravity;

    private Animator mAnimator;

    private CameraController camCon;
    Vector3 movePoint;

    public bool canMove = true;
    private bool running;
    public bool attacking;

    private EQManager eqManager;
    private float staminaTime = 0f;

    private Camera camera;
    private Vector3 worldPos;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        camCon = FindObjectOfType<CameraController>();
        eqManager = GetComponent<EQManager>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        running = false;
        //attacking = false;
        if (canMove) {
            if (!attacking) {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= 0.1f) {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);

                    running = true;
                } else {
                    running = false;
                }
            }

            if (Input.GetMouseButton(0) && eqManager.currentStamina >= 5 && eqManager.weaponEquiped) {
                attacking = true;
                LookAtMouse();
                staminaTime += Time.deltaTime;
                if(staminaTime >= 1f) {
                    eqManager.StaminaUse(5);
                    staminaTime = 0f;
                }
            } else {
                attacking = false;
            }
        }

        // gravity
        gravity -= 0.1f * Time.deltaTime;
        if (controller.isGrounded)
            gravity = 0f;
        controller.Move( new Vector3(0f, gravity, 0f));

        mAnimator.SetBool("running", running);
        mAnimator.SetBool("attacking", attacking);
    }

    void LookAtMouse() {
        float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Move Camera")) {
            movePoint = new Vector3(other.transform.position.x, camCon.gameObject.transform.position.y, other.transform.position.z - 5f);
            camCon.MoveCamera(movePoint);
            Debug.Log(movePoint.z);
        }
    }
}
