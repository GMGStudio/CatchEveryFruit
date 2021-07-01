using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager instance;

    [SerializeField]
    public List<GameObject> fruitsPrefabs = null;

    public float currentFallingSpeed;

    private float normalFallingSpeed = 3f;

    private FruitSpawnHelper spawnHelper;

    private readonly float spawningSpeed = 1f;

    private void Awake()
    {
        SingletonPattern();
        spawnHelper = gameObject.AddComponent<FruitSpawnHelper>();
        ResetFallingSpeed();
    }

    public void ResetFallingSpeed()
    {
        currentFallingSpeed = normalFallingSpeed;
    }

    public void SpawnStartFruits()
    {
        StartCoroutine(SpawnFruit());
    }

    private IEnumerator SpawnFruit()
    {
        while (StateMachine.instance.currentGameState == GameStates.Playing)
        {
            yield return new WaitForSeconds(spawningSpeed);
            SpawnTwoOrThreeFruits();
        }
    }

    private void SpawnTwoOrThreeFruits()
    {
        for (int i = 0; i < Random.Range(2, 3); i++)
        {
            spawnHelper.SpawnFruits();
        }
    }

    public void HideFruits()
    {
        StopAllCoroutines();
        foreach (GameObject fruit in GameObject.FindGameObjectsWithTag("Fruit"))
        {
            fruit.GetComponent<Fruit>().Hide();
        }
    }

    public GameObject GetPrefabByType(Fruits FruitToFind)
    {
        return fruitsPrefabs.Find(x => x.GetComponent<Fruit>().FruitType == FruitToFind);
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
