using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{

    public void PauseTheGame()
    {
        if(StateMachine.instance.currentGameState == GameStates.Playing)
        {
            StateMachine.instance.SwitchGameState(GameStates.Paused);
        }
    }

    public void RestartTheGame()
    {
        if (StateMachine.instance.currentGameState == GameStates.GameOver)
        {
            StateMachine.instance.SwitchGameState(GameStates.Playing);
        }
    }

    public void ToggleSoundActive()
    {
        AudioManager.instance.ToogleSoundActive();
    }
}
