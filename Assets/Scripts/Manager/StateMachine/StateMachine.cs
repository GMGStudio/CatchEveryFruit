using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine instance;

    [SerializeField]
    private List<GameStateHandler> gameStateHandlers;

    public GameStates currentGameState { get; private set; }

    private GameStateHandler currentStateHandler;

    private void Awake()
    {
        SingletonPattern();
    }

    private void Start()
    {
        SetStateHandler(GameStates.Launching);
        currentStateHandler.Setup(GameStates.Launching);
    }


    public void SwitchGameState(GameStates stateToSwitch)
    {
        if(stateToSwitch == currentGameState) { Debug.LogWarning("O.o"); return; }
        GameStates lastgameState = currentGameState;

        currentStateHandler.TearDown();
        currentGameState = stateToSwitch;

        SetStateHandler(stateToSwitch);

        currentStateHandler.Setup(lastgameState);
    }

    private void SetStateHandler(GameStates stateToSwitch)
    {
        currentStateHandler = gameStateHandlers.Find(x => x.state == stateToSwitch);
    }

    private void SingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
