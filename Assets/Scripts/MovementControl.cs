using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTankControl : MonoBehaviour
{
    //Variables for input
    public InputAction moveAction, rotateAction;

    private Vector2 moveInput, rotateInput;

    //Variables for speed
    public float movementSpeed, rotationSpeed;
    public float jumpSpeed;

    //Additional variables
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject playerCam;
    public float upDownModifier;

    private Rigidbody rb;

    //raycast stuff
    RaycastHit hitInfo;
    public GameObject rayDisplay;

    bool jumpPressed;
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        jumpPressed = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player inputs (and convert to the vector2)
        moveInput = moveAction.ReadValue<Vector2>();
        rotateInput = rotateAction.ReadValue<Vector2>();

        if (!jumpPressed && canJump)
        {
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.spaceKey.wasPressedThisFrame)
                {
                    jumpPressed = true;
                    canJump = false;
                }
            }
        }

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo))
        {
            rayDisplay.transform.position = hitInfo.point;
        }

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.rotation.eulerAngles.normalized);
    }

    private void FixedUpdate()
    {
        //movement
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * movementSpeed * Time.fixedDeltaTime);

        //up/down rotation for weapon/camera
        weapon.transform.Rotate(Vector3.right * -1, rotateInput.y * (rotationSpeed * upDownModifier) * Time.fixedDeltaTime);
        playerCam.transform.Rotate(Vector3.right * -1, rotateInput.y * (rotationSpeed * upDownModifier) * Time.fixedDeltaTime);

        //if (Mathf.Abs(weapon.transform.rotation.x - 90) > 45)
        //{
        //}

        //left/right rotation
        transform.Rotate(Vector3.up, rotateInput.x * rotationSpeed * Time.fixedDeltaTime);

        if (jumpPressed)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            canJump = false;
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
    }
}
