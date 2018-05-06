using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public enum GameState {Title, Game, Finish};
    public GameState currentState;

    public InputManager im;
    public PuckManager pm;
    public WorldManager wm;

    //Keep track of the boop button here for scene transitions, because it's being kept track of in fixedupdate elsewhere
    float boopButton = 0f;
    float prevBoop;

    // Use this for initialization 
    void Start () {
        //Initialize the managers
        pm.Init();
        wm.Init();

        currentState = GameState.Title;
    }
	
	// Update is called once per frame
	void Update () {
        prevBoop = boopButton;
        boopButton = pm.boopButton;

		switch (currentState)
        {
            case GameState.Title:
                wm.SetTitleScreenVisibility(true);
                if (boopButton == 1f && prevBoop == 0f)
                {
                    wm.SpawnTimers();
                    wm.SetTitleScreenVisibility(false);
                    pm.SpawnPucks();
                    currentState = GameState.Game;
                }
                break;
            case GameState.Game:
                wm.UpdateTimers();

                if (wm.leftTime <= 1 || wm.rightTime <= 1)
                {
                    wm.DestroyTimers();
                    pm.DestroyPucks();
                    wm.SetVictorScreenVisibility(true);
                    currentState = GameState.Finish;
                }
                if (Input.GetKey(KeyCode.R)) //Used to reset the game
                {
                    wm.DestroyTimers();
                    pm.DestroyPucks();
                    wm.SetTitleScreenVisibility(true);
                    currentState = GameState.Title;
                }
                break;
            case GameState.Finish:
                if (boopButton == 1f && prevBoop == 0f)
                {
                    wm.SetVictorScreenVisibility(false);
                    wm.SetTitleScreenVisibility(true);
                    currentState = GameState.Title;
                }
                break;
        }
	}
}
