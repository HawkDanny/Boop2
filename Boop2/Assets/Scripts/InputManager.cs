using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //The rigidbodies will be retrieved from the puckmanager gameobjects
    public PuckManager pm;

    //Update the input arrays of the pucks
    private void UpdateInput()
    {
        //reset the values
        pm.leftPuckInput[0] = 0f;
        pm.leftPuckInput[1] = 0f;
        pm.leftPuckInput[2] = 0f;
        pm.leftPuckInput[3] = 0f;
        pm.rightPuckInput[0] = 0f;
        pm.rightPuckInput[1] = 0f;
        pm.rightPuckInput[2] = 0f;
        pm.rightPuckInput[3] = 0f;
        pm.boopButton = 0f;


        //LEFT ON KEYBOARD
        if (Input.GetKey(KeyCode.W))
            pm.leftPuckInput[0] = 1f;
        if (Input.GetKey(KeyCode.D))
            pm.leftPuckInput[1] = 1f;
        if (Input.GetKey(KeyCode.S))
            pm.leftPuckInput[2] = 1f;
        if (Input.GetKey(KeyCode.A))
            pm.leftPuckInput[3] = 1f;
        
        //RIGHT ON KEYBOARD
        if (Input.GetKey(KeyCode.UpArrow))
            pm.rightPuckInput[0] = 1f;
        if (Input.GetKey(KeyCode.RightArrow))
            pm.rightPuckInput[1] = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            pm.rightPuckInput[2] = 1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            pm.rightPuckInput[3] = 1f;

        //BOOP
        if (Input.GetKey(KeyCode.Space))
            pm.boopButton = 1f;
    }

    public void ParsePhysicalInput(string inputString)
    {
        UpdateInput();

        string[] temp = inputString.Split(',');
        int[] input = new int[temp.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            int tempInput;
            if (int.TryParse(temp[i], out tempInput))
                input[i] = tempInput;
            else
                input[i] = 0;
        }

        pm.leftPuckInput[0] = (float)input[0];
        pm.leftPuckInput[1] = (float)input[1];
        pm.leftPuckInput[2] = (float)input[2];
        pm.leftPuckInput[3] = (float)input[3];
        pm.rightPuckInput[0] = (float)input[4];
        pm.rightPuckInput[1] = (float)input[5];
        pm.rightPuckInput[2] = (float)input[6];
        pm.rightPuckInput[3] = (float)input[7];
        pm.boopButton = (float)input[8];
    }
}