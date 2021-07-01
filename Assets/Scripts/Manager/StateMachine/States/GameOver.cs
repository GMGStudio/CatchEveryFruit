using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        FruitManager.instance.HideFruits();
        GameOverUIManager.instance.Enable(true);
        HighScoreManager.instance.SetHighScorePerhabs();
        AudioManager.instance.PlaySound(SoundEffects.GameOver);
        AdsManager.instance.ShowInterstitialIfNeeded();
    }

    public override void TearDown()
    {
        FruitManager.instance.ResetFallingSpeed();
        GameOverUIManager.instance.Enable(false);
    }


}
