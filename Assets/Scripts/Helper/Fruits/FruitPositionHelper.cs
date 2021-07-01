using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPositionHelper 
{
    Vector3 lastSpawnPoint;

    private readonly float SpwanMinDistance = 1f;

    public Vector3 GetSpawnPosition()
    {
        Vector3 newSpawnPoint = new Vector3(Random.Range(ScreenHelper.ScreenLeft, ScreenHelper.ScreenRight),
                                            ScreenHelper.ScreenTop + Random.Range(1, 3));
        if(Vector3.Distance(newSpawnPoint,lastSpawnPoint) > SpwanMinDistance)
        {
            lastSpawnPoint = newSpawnPoint;
            return newSpawnPoint;
        }
        else
        {
            return GetSpawnPosition();
        }

    }
}
