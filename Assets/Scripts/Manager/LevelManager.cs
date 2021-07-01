using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string levelStringText = "Level: ";
    public static LevelManager instance;

    [SerializeField]
    private GameObject LevelText;

    private Fruits fruitToCatch;

    private GameObject fruitToShow;

    private Vector3 LevelFruitSize = new Vector3(.6f, .6f, 6f);

    private TextMeshProUGUI levelTexttmpro;

    private int currentLevel;
    private int FruitsPerLevel = 10;

    private void Awake()
    {
        SingletonPattern();
        currentLevel = 1;
        levelTexttmpro = LevelText.GetComponent<TextMeshProUGUI>();
    }

    public void LevelStart()
    {
        fruitToCatch = GetRandomFruit();
        AudioManager.instance.PlaySound(SoundEffects.LevelChange);
        ShowLevelChange();
        FruitManager.instance.SpawnStartFruits();
    }

    public void FruitCatch(Fruits fruitType)
    {
        if (fruitType == fruitToCatch)
        {
            ScoreManager.instance.AddScore();
            AudioManager.instance.PlaySound(SoundEffects.Catch);
            SetNextLevelPerhabs();
        }
        else
        {
            StateMachine.instance.SwitchGameState(GameStates.GameOver);
        }
    }

    private void SetNextLevelPerhabs()
    {
        if (ScoreManager.instance.GetScore() % FruitsPerLevel == 0)
        {
            FruitManager.instance.HideFruits();
            FruitManager.instance.currentFallingSpeed += .2f;
            currentLevel++;
            LevelStart();
        }
    }

    private void ShowLevelChange()
    {
        levelTexttmpro.SetText(levelStringText + currentLevel);
        LeanTween.scale(InstantiateCurrentFruit(), LevelFruitSize, .5f)
                 .setEaseInBack()
                 .setOnComplete(() => StartCoroutine(HideLevelChangeAfterTime()));
        LeanTween.scale(LevelText, Vector3.one, .5f).setEaseInBack();
    }

    IEnumerator HideLevelChangeAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        HideLevelChange();
    }

    private void HideLevelChange()
    {
        LeanTween.scale(fruitToShow, Vector3.zero, .5f).setEaseOutBack();
        LeanTween.scale(LevelText, Vector3.zero, .5f).setEaseOutBack();
    }

    private GameObject InstantiateCurrentFruit()
    {
        GameObject fruitToShowPrefab = FruitManager.instance.GetPrefabByType(fruitToCatch);
        fruitToShow = Instantiate(fruitToShowPrefab, Vector3.zero, Quaternion.identity);
        fruitToShow.GetComponent<Fruit>().enabled = false;
        return fruitToShow;
    }

    Fruits GetRandomFruit()
    {
        Array values = Enum.GetValues(typeof(Fruits));
        System.Random random = new System.Random();
        return (Fruits)values.GetValue(random.Next(values.Length));
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
