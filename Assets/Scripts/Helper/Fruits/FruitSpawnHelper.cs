using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnHelper : MonoBehaviour
{
    private FruitPositionHelper positionHelper;

    private List<GameObject> spawnedFruits = new List<GameObject>();

    private void Awake()
    {
        positionHelper = new FruitPositionHelper();
    }

    public void SpawnFruits()
    {
        if (FruitAlreadySpawnedAndInvisible())
        {
            Reuse();
        }
        else
        {
            Spawn();
        }
    }

    private bool FruitAlreadySpawnedAndInvisible()
    {
        return spawnedFruits.Exists(x => !x.activeSelf && x != null);
    }

    private void Spawn()
    {

            GameObject spawnedFruit = Instantiate(getRandomFruit(), positionHelper.GetSpawnPosition(), Quaternion.identity);
            spawnedFruits.Add(spawnedFruit);
        
    }

    private void Reuse()
    {
        GameObject fruitToReuse = spawnedFruits.Find(x => !x.activeSelf && x != null);
        fruitToReuse.SetActive(true);
        fruitToReuse.transform.position = positionHelper.GetSpawnPosition();
        fruitToReuse.GetComponent<Fruit>().Reset();
    }


    GameObject getRandomFruit()
    {
        return FruitManager.instance.fruitsPrefabs[Random.Range(0, FruitManager.instance.fruitsPrefabs.Count)];
    }
}
