using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrampolineScript : MonoBehaviour
{
    //TODO: ADD POSITIVE/NEGATIVE BINDING for J and K. Make trampoline rotate between values of 30 to -30. Set velocity to -y * angle coefficient, and x * opposite angle coefficient.

    //Public variables (modification)
    public float bounceCoefficient;
    public float bounceLoss;
    public float rotateSpeed;

    public InputAction trampRotate;

    //Private variables (for function)
    private float lastSpeed;
    private Rigidbody lastRB;

    private float yCoefficient;
    private float zCoefficient;

    private float rotateDirection;

    // Start is called before the first frame update
    void Start()
    {
        lastSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Bonus functions for rotating the trampoline
        rotateDirection = trampRotate.ReadValue<float>();

        transform.Rotate(Vector3.right * rotateDirection * rotateSpeed);

        Debug.Log(yCoefficient);
        //transform.rotation = new Quaternion(Mathf.Clamp(transform.rotation.x, -0.18f, 0.18f), transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (transform.rotation.eulerAngles.x > 180)
        {
            yCoefficient = 1 - ((360 - transform.rotation.eulerAngles.x) / 360) * 4;
            zCoefficient = ((360 - transform.rotation.eulerAngles.x) / 360) * 4;
        }
        else
        {
            yCoefficient = 1 - (transform.rotation.eulerAngles.x / 360) * 4;
            zCoefficient = (transform.rotation.eulerAngles.x / 360) * 4;

            zCoefficient *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Check")
        {
            lastSpeed = other.attachedRigidbody.velocity.y;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Check")
        {
            //Debug.Log(lastSpeed);
            collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x, lastSpeed * bounceCoefficient * bounceLoss * yCoefficient, collision.rigidbody.velocity.z + lastSpeed * zCoefficient);

            if (lastSpeed > -2.5f)
            {
                //Forces objects to settle faster once their bounces become miniscule and causes problems for the trigger.
                lastSpeed *= 0.75f;
            }
        }
    }

    private void OnEnable()
    {
        trampRotate.Enable();
    }

    private void OnDisable()
    {
        trampRotate.Disable();
    }
}