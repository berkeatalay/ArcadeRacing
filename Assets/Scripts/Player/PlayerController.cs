using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRb = null;

    [SerializeField] private float forwardAcc = 10f;
    [SerializeField] private float reverseAcc = 5f;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float turnStrength = 180f;
    [SerializeField] private float gravityForce = 10f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float rayLength = .5f;
    [SerializeField] private Transform groundRayPosition;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private ButtonHandle forwardButton;
    [SerializeField] private Button backwardButton;


    private float turnInput;
    private float forwardInput;
    private float turnModifier;
    private Vector3 direction;
    [SerializeField] private bool groundCheck;
    private float dragOnGround = 3f;



    private void Start()
    {
        sphereRb.transform.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetAxis("Vertical") > 0)
        {
            forwardInput = Input.GetAxis("Vertical") * forwardAcc * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            forwardInput = Input.GetAxis("Vertical") * reverseAcc * 1000f;
        }
        else
        {
            forwardInput = 0;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(joystick.Horizontal) >= 0.1)
        {
            turnInput = joystick.Horizontal;
        }

        if (forwardButton.Vertical > 0)
        {
            forwardInput = forwardButton.Vertical * forwardAcc * 1000f;
        }


        if (Mathf.Abs(sphereRb.velocity.magnitude) > 5)
        {
            if (sphereRb.velocity.magnitude > 0)
            {
                turnModifier = 1;
            }
            else
            {
                turnModifier = -1;
            }
        }
        else
        {
            turnModifier = Input.GetAxis("Vertical");
        }



        if (groundCheck)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * turnModifier, 0f));
        }



        transform.position = new Vector3(sphereRb.transform.position.x, sphereRb.transform.position.y - 0.35f, sphereRb.transform.position.z);
    }

    private void FixedUpdate()
    {
        groundCheck = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPosition.position, -transform.up, out hit, rayLength, ground))
        {
            groundCheck = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if (groundCheck)
        {
            sphereRb.drag = dragOnGround;

            if (Mathf.Abs(forwardInput) > 0)
            {
                sphereRb.AddForce(transform.forward * forwardInput);
            }
        }
        else
        {
            sphereRb.drag = 0.1f;

            sphereRb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }
}

