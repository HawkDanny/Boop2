using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckManager : MonoBehaviour {

    public GameObject leftPuck;
    public GameObject rightPuck;
    private Rigidbody leftPuck_rb;
    private Rigidbody rightPuck_rb;
    private Vector3 leftPuckForce;
    private Vector3 rightPuckForce;
    [HideInInspector]
    public float[] leftPuckInput; //index 0: up, 1: right, 2: down, 3: left
    [HideInInspector]
    public float[] rightPuckInput;
    [HideInInspector]
    public bool boopButton; //whether the boop button is pressed

    //The maximum force that a keypress adds
    //(For a keyboard, it always adds max force)
    public float maxForce;
    //The maximum speed a puck can go
    public float maxSpeed;

    // Use this for initialization
    void Start () {
        leftPuck_rb = leftPuck.GetComponent<Rigidbody>();
        rightPuck_rb = rightPuck.GetComponent<Rigidbody>();

        //default input is NONE
        leftPuckInput = new float[] { 0f, 0f, 0f, 0f };
        rightPuckInput = new float[] { 0f, 0f, 0f, 0f };
        boopButton = false;
    }

    void FixedUpdate()
    {
        leftPuckForce = Vector3.zero;
        rightPuckForce = Vector3.zero;

        //Left
        leftPuckForce += Vector3.up * leftPuckInput[0];
        leftPuckForce += Vector3.right * leftPuckInput[1];
        leftPuckForce += Vector3.down * leftPuckInput[2];
        leftPuckForce += Vector3.left * leftPuckInput[3];

        //Right
        rightPuckForce += Vector3.up * rightPuckInput[0];
        rightPuckForce += Vector3.right * rightPuckInput[1];
        rightPuckForce += Vector3.down * rightPuckInput[2];
        rightPuckForce += Vector3.left * rightPuckInput[3];

        //normalize the vectors and then multiply by the max force
        leftPuck_rb.AddForce(leftPuckForce * maxForce); //TODO: maybe figure out a way to normalize this and stop diagonals from being faster
        rightPuck_rb.AddForce(rightPuckForce * maxForce);
    }
}
