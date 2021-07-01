using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : GameStateHandler
{
    public override void Setup(GameStates lastgameState)
    {
        StartMenuUIManager.instance.Enable(true);
        AdsManager.instance.ShowBanner();
    }

    public override void TearDown()
    {
        StartMenuUIManager.instance.Enable(false);
    }

}
