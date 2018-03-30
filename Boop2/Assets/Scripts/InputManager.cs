using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //The rigidbodies will be retrieved from the puckmanager gameobjects
    public PuckManager pm;

    //Update the input arrays of the pucks
    void Update()
    {
        #region Keyboard Input
        //LEFT ON KEYBOARD
        if (Input.GetKey(KeyCode.W))
            pm.leftPuckInput[0] = 1f;
        else
            pm.leftPuckInput[0] = 0f;

        if (Input.GetKey(KeyCode.D))
            pm.leftPuckInput[1] = 1f;
        else
            pm.leftPuckInput[1] = 0f;

        if (Input.GetKey(KeyCode.S))
            pm.leftPuckInput[2] = 1f;
        else
            pm.leftPuckInput[2] = 0f;

        if (Input.GetKey(KeyCode.A))
            pm.leftPuckInput[3] = 1f;
        else
            pm.leftPuckInput[3] = 0f;

        //RIGHT ON KEYBOARD
        if (Input.GetKey(KeyCode.UpArrow))
            pm.rightPuckInput[0] = 1f;
        else
            pm.rightPuckInput[0] = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
            pm.rightPuckInput[1] = 1f;
        else
            pm.rightPuckInput[1] = 0f;

        if (Input.GetKey(KeyCode.DownArrow))
            pm.rightPuckInput[2] = 1f;
        else
            pm.rightPuckInput[2] = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            pm.rightPuckInput[3] = 1f;
        else
            pm.rightPuckInput[3] = 0f;
        #endregion

        #region Arcade Input
        #endregion
    }
}
