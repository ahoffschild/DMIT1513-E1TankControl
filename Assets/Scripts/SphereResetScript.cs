using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereResetScript : MonoBehaviour
{
    //Saved position
    private Vector3 startingPosition;
    private bool releaseWait;
    private float resetValue;

    //Button to press
    public InputAction resetAction;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        releaseWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        resetValue = resetAction.ReadValue<float>();

        if (resetValue == 1 && !releaseWait)
        {
            Debug.Log("asdf");
            releaseWait = true;
        }
        if (resetValue == 0 && releaseWait)
        {
            var rb = GetComponent<Rigidbody>();
            rb.position = startingPosition;
            rb.velocity = Vector3.zero;
            releaseWait = false;
        }
    }

    private void OnEnable()
    {
        resetAction.Enable();
    }

    private void OnDisable()
    {
        resetAction.Disable();
    }
}
