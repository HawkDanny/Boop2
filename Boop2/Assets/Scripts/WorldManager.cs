using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    //FIELDS

    //The scene's main camera, must be orthographic
    public Camera mainCamera;
    private Vector3[] cameraBounds = new Vector3[4];

    //The thickness of the cube primitives that make up the walls
    public float wallThickness;
    private float wallDepth = 10f; //Maybe make this public

    //PROPERTIES

    /// <summary>
    /// Returns the bounds of the orthographic camera in order of top left, top right, bottom right, bottom left
    /// </summary>
    public Vector3[] CameraBounds { get { return cameraBounds; } }


	void Start () {
        CreateWorld();
    }

    public void CreateWorld()
    {
        CalculateOrthographicCameraBounds();
        CreateWalls();
    }

    /// <summary>
    /// Sets teh values of the cameraBounds array.
    /// </summary>
    private void CalculateOrthographicCameraBounds()
    {
        float ortho = mainCamera.orthographicSize;

        CameraBounds[0] = new Vector3(-ortho * mainCamera.aspect, ortho, 0f);
        CameraBounds[1] = new Vector3(ortho * mainCamera.aspect, ortho, 0f);
        CameraBounds[2] = new Vector3(ortho * mainCamera.aspect, -ortho, 0f);
        CameraBounds[3] = new Vector3(-ortho * mainCamera.aspect, -ortho, 0f);
    }

    /// <summary>
    /// Creates the invisible primitive cubes that serve as the walls in the game
    /// </summary>
    private void CreateWalls()
    {
        float halfWall = wallThickness / 2;

        GameObject leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftWall.transform.localScale = new Vector3(wallThickness, mainCamera.orthographicSize * 2, wallDepth);
        leftWall.transform.position = new Vector3(cameraBounds[0].x - halfWall, 0f, 0f);
        leftWall.GetComponent<MeshRenderer>().enabled = false;
        leftWall.AddComponent<BoxCollider>();
        leftWall.name = "Left Wall";

        GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightWall.transform.localScale = new Vector3(wallThickness, mainCamera.orthographicSize * 2, wallDepth);
        rightWall.transform.position = new Vector3(cameraBounds[1].x + halfWall, 0f, 0f);
        rightWall.GetComponent<MeshRenderer>().enabled = false;
        rightWall.AddComponent<BoxCollider>();
        rightWall.name = "Right Wall";

        GameObject topWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topWall.transform.localScale = new Vector3(mainCamera.orthographicSize * 2 * mainCamera.aspect, wallThickness, wallDepth);
        topWall.transform.position = new Vector3(0f, cameraBounds[0].y + halfWall, 0f);
        topWall.GetComponent<MeshRenderer>().enabled = false;
        topWall.AddComponent<BoxCollider>();
        topWall.name = "Top Wall";

        GameObject bottomWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomWall.transform.localScale = new Vector3(mainCamera.orthographicSize * 2 * mainCamera.aspect, wallThickness, wallDepth);
        bottomWall.transform.position = new Vector3(0f, cameraBounds[2].y - halfWall, 0f);
        bottomWall.GetComponent<MeshRenderer>().enabled = false;
        bottomWall.AddComponent<BoxCollider>();
        bottomWall.name = "Bottom Wall";

        GameObject walls = new GameObject();
        walls.name = "Walls";
        leftWall.transform.SetParent(walls.transform);
        rightWall.transform.SetParent(walls.transform);
        topWall.transform.SetParent(walls.transform);
        bottomWall.transform.SetParent(walls.transform);
    }
}
