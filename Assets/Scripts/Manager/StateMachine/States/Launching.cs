using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launching : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        StateMachine.instance.SwitchGameState(GameStates.Menu);
    }

    public override void TearDown()
    {

    }

}
