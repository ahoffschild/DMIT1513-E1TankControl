using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Necessary variables
    public float movementSpeed;
    public GameObject[] waypoints;
    int index;
    Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        //Get rbody
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Chose destination
        if (Vector3.Distance(transform.position, waypoints[index].transform.position) < 0.1f)
        {
            if (index + 1 < waypoints.Length)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        //Move platform

        //transform.Translate((waypoints[index].transform.position - transform.position).normalized * movementSpeed * Time.deltaTime);
        
    }

    private void FixedUpdate()
    {
        //new move platform
        rbody.AddForce((waypoints[index].transform.position - transform.position).normalized * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    //Carry player with you
    /*private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.transform.root.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        //other.gameObject.transform.root.parent = null; 
    }*/
}
