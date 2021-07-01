using UnityEngine;

[System.Serializable]
public abstract class GameStateHandler : MonoBehaviour
{
    public GameStates state;

    public abstract void Setup(GameStates lastgameState);

    public abstract void TearDown();
}
