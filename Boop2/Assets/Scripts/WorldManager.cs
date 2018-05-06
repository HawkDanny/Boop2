using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldManager : MonoBehaviour
{
    //FIELDS
    public PuckManager pm;

    //The scene's main camera, must be orthographic
    public Camera mainCamera;
    private Vector3[] cameraBounds = new Vector3[4];

    //The thickness of the cube primitives that make up the walls
    public float wallThickness;
    private float wallDepth = 10f; //Maybe make this public
    public PhysicMaterial wallPM;

    //Canvas that the text is on
    public Canvas canvas;
    public TextMeshProUGUI leftTimer;
    public TextMeshProUGUI rightTimer;
    public TextMeshProUGUI title;
    public TextMeshProUGUI p1Wins;
    public TextMeshProUGUI p2Wins;

    [HideInInspector]
    public float leftTime;
    [HideInInspector]
    public float rightTime;

    //PROPERTIES

    /// <summary>
    /// Returns the bounds of the orthographic camera in order of top left, top right, bottom right, bottom left
    /// </summary>
    public Vector3[] CameraBounds { get { return cameraBounds; } }

    public void Init()
    {
        CreateWorld();
    }

    public void CreateWorld()
    {
        CalculateOrthographicCameraBounds();
        CreateWalls();
        ResizeCanvas();
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
        leftWall.GetComponent<Collider>().material = wallPM;

        GameObject rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightWall.transform.localScale = new Vector3(wallThickness, mainCamera.orthographicSize * 2, wallDepth);
        rightWall.transform.position = new Vector3(cameraBounds[1].x + halfWall, 0f, 0f);
        rightWall.GetComponent<MeshRenderer>().enabled = false;
        rightWall.AddComponent<BoxCollider>();
        rightWall.name = "Right Wall";
        rightWall.GetComponent<Collider>().material = wallPM;

        GameObject topWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        topWall.transform.localScale = new Vector3(mainCamera.orthographicSize * 2 * mainCamera.aspect, wallThickness, wallDepth);
        topWall.transform.position = new Vector3(0f, cameraBounds[0].y + halfWall, 0f);
        topWall.GetComponent<MeshRenderer>().enabled = false;
        topWall.AddComponent<BoxCollider>();
        topWall.name = "Top Wall";
        topWall.GetComponent<Collider>().material = wallPM;

        GameObject bottomWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomWall.transform.localScale = new Vector3(mainCamera.orthographicSize * 2 * mainCamera.aspect, wallThickness, wallDepth);
        bottomWall.transform.position = new Vector3(0f, cameraBounds[2].y - halfWall, 0f);
        bottomWall.GetComponent<MeshRenderer>().enabled = false;
        bottomWall.AddComponent<BoxCollider>();
        bottomWall.name = "Bottom Wall";
        bottomWall.GetComponent<Collider>().material = wallPM;

        GameObject walls = new GameObject();
        walls.name = "Walls";
        leftWall.transform.SetParent(walls.transform);
        rightWall.transform.SetParent(walls.transform);
        topWall.transform.SetParent(walls.transform);
        bottomWall.transform.SetParent(walls.transform);
    }

    private void ResizeCanvas()
    {
        float canvasWidth = cameraBounds[1].x - cameraBounds[0].x;
        float canvasHeight = cameraBounds[1].y - cameraBounds[2].y;

        //Scores
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);
        leftTimer.GetComponent<RectTransform>().sizeDelta = new Vector2(-canvasWidth / 2, 0);
        rightTimer.GetComponent<RectTransform>().sizeDelta = new Vector2(-canvasWidth / 2, 0);
        rightTimer.GetComponent<RectTransform>().anchoredPosition = new Vector2(canvasWidth / 2, 0);
    }

    public void SetTitleScreenVisibility(bool isVisible)
    {
        title.gameObject.SetActive(isVisible);
    }

    //Sets the scores to active and resets their timers
    public void SpawnTimers()
    {
        rightTime = 30f;
        leftTime = 30f;
        leftTimer.gameObject.SetActive(true);
        rightTimer.gameObject.SetActive(true);
    }

    public void UpdateTimers()
    {
        leftTimer.text = (int)leftTime + "";
        rightTimer.text = (int)rightTime + "";

        if (pm.neutralPuck.transform.position.x < -0.01)
            leftTime -= Time.deltaTime;
        else if (pm.neutralPuck.transform.position.x > 0.01)
            rightTime -= Time.deltaTime;
    }

    public void DestroyTimers()
    {
        leftTimer.gameObject.SetActive(false);
        rightTimer.gameObject.SetActive(false);
    }

    public void SetVictorScreenVisibility(bool isVisible)
    {
        if (leftTime <= 1)
            p2Wins.gameObject.SetActive(isVisible);
        else if (rightTime <= 1)
            p1Wins.gameObject.SetActive(isVisible);
    }
}