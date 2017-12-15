using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckManager : MonoBehaviour {

    public GameObject puck1;
    public GameObject puck2;
    private Rigidbody puck1_rb;
    private Rigidbody puck2_rb;

    //The maximum force that a keypress adds
    //(For a keyboard, it always adds max force)
    public float maxForce;
    //The maximum speed a puck can go
    public float maxSpeed;

    // Use this for initialization
    void Start () {
        puck1_rb = puck1.GetComponent<Rigidbody>();
        puck2_rb = puck2.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        //Lock the rotation
        puck1.transform.rotation = Quaternion.Euler(Vector3.zero);
        puck2.transform.rotation = Quaternion.Euler(Vector3.zero);
        //puck1.transform.position.Set(puck1.transform.position.x, puck1.transform.position.y, 0f); //TODO: there has to be a better way to do this

        puck1_rb.AddForce(new Vector3(0, 1f, 0));
    }
}
