using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckManager : MonoBehaviour
{
    public GameManager gm;
    public WorldManager wm;

    public GameObject PuckPrefab;
    public GameObject neutralPuckPrefab;
    public Transform leftPuckAnchorOverride;
    public Transform rightPuckAnchorOverride;
    public Transform neutralPuckAnchorOverride;
    public Material leftPuckMaterial; //Temporary
    public Material rightPuckMaterial; //Temporary

    private GameObject leftPuck;
    private GameObject rightPuck;
    [HideInInspector]
    public GameObject neutralPuck;
    private Rigidbody leftPuck_rb;
    private Rigidbody rightPuck_rb;
    private Vector3 leftPuckForce;
    private Vector3 rightPuckForce;
    [HideInInspector]
    public float[] leftPuckInput; //index 0: up, 1: right, 2: down, 3: left
    [HideInInspector]
    public float[] rightPuckInput;
    [HideInInspector]
    public float boopButton; //whether the boop button is pressed
    [HideInInspector]
    public float prevBoop = 0f;
    public float boopMultiplier;

    //The maximum force that a keypress adds
    //(For a keyboard, it always adds max force)
    public float maxForce;
    //The maximum speed a puck can go
    //public float maxSpeed;
    //An implementation of quadratic drag
    public float drag;

    public void Init()
    {
        leftPuckInput = new float[] { 0f, 0f, 0f, 0f };
        rightPuckInput = new float[] { 0f, 0f, 0f, 0f };
        boopButton = 0f;
    }

    void FixedUpdate()
    {
        switch (gm.currentState)
        {
            case GameManager.GameState.Title:
                break;
            case GameManager.GameState.Game:
                float multiplier;
                if (boopButton == 1f && boopButton != prevBoop)
                    multiplier = boopMultiplier;
                else
                    multiplier = 1f;
                prevBoop = boopButton;

                leftPuckForce = Vector3.zero;
                rightPuckForce = Vector3.zero;

                //Left
                leftPuckForce += Vector3.up * leftPuckInput[0] * multiplier;
                leftPuckForce += Vector3.right * leftPuckInput[1] * multiplier;
                leftPuckForce += Vector3.down * leftPuckInput[2] * multiplier;
                leftPuckForce += Vector3.left * leftPuckInput[3] * multiplier;

                //Right
                rightPuckForce += Vector3.up * rightPuckInput[0] * multiplier;
                rightPuckForce += Vector3.right * rightPuckInput[1] * multiplier;
                rightPuckForce += Vector3.down * rightPuckInput[2] * multiplier;
                rightPuckForce += Vector3.left * rightPuckInput[3] * multiplier;

                //normalize the vectors and then multiply by the max force
                leftPuck_rb.AddForce(leftPuckForce * maxForce); //TODO: maybe figure out a way to normalize this and stop diagonals from being faster
                rightPuck_rb.AddForce(rightPuckForce * maxForce);

                //Drag
                Vector3 leftDragForce = leftPuck_rb.velocity.normalized * (drag * leftPuck_rb.velocity.sqrMagnitude - 10);
                Vector3 rightDragForce = rightPuck_rb.velocity.normalized * (drag * rightPuck_rb.velocity.sqrMagnitude - 10);
                leftPuck_rb.AddForce(leftDragForce);
                rightPuck_rb.AddForce(rightDragForce);

                break;
            case GameManager.GameState.Finish:
                break;
        }
    }

    public void SpawnPucks()
    {
        Vector3[] cameraBounds = wm.CameraBounds;

        float canvasWidth = cameraBounds[1].x - cameraBounds[0].x;

        Vector3 leftPuckLoc = new Vector3(-canvasWidth / 4, 0, 0);
        Vector3 rightPuckLoc = new Vector3(canvasWidth / 4, 0, 0);

        leftPuck = Object.Instantiate(PuckPrefab, leftPuckLoc, Quaternion.Euler(90, 0, 0));
        rightPuck = Object.Instantiate(PuckPrefab, rightPuckLoc, Quaternion.Euler(90, 0, 0));
        neutralPuck = Object.Instantiate(neutralPuckPrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));

        leftPuck.GetComponent<MeshRenderer>().probeAnchor = leftPuckAnchorOverride;
        rightPuck.GetComponent<MeshRenderer>().probeAnchor = rightPuckAnchorOverride;
        neutralPuck.GetComponent<MeshRenderer>().probeAnchor = neutralPuckAnchorOverride;

        //Temporary
        leftPuck.gameObject.GetComponent<Renderer>().material = leftPuckMaterial;
        rightPuck.gameObject.GetComponent<Renderer>().material = rightPuckMaterial;

        leftPuck_rb = leftPuck.GetComponent<Rigidbody>();
        rightPuck_rb = rightPuck.GetComponent<Rigidbody>();
    }

    public void DestroyPucks()
    {
        Destroy(leftPuck);
        Destroy(rightPuck);
        Destroy(neutralPuck);
    }
}