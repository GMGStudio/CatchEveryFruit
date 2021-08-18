using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HighscoreServiceTests
{
    private IGMGHighscoreService highscoreService = new GMGHighscoreService();
    private string highscoreKey = "Highscore";

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteAll();
    }

    [Test]
    public void Highscore_is_inital_0()
    {
        Assert.AreEqual(0, highscoreService.highscore, "Highscore wasn't 0 it was " + highscoreService.highscore);
    }

    [Test]
    public void Highscore_is_upadting()
    {
        bool highscoreWasUpdated = highscoreService.CheckHighscoreAndUpdateIfNeeded(2);
        Assert.IsTrue(highscoreWasUpdated);
        Assert.AreEqual(2, highscoreService.highscore, "Highscore wasn't 0 it was " + highscoreService.highscore);
    }

    [Test]
    public void Highscore_is_not_upadting_if_lower()
    {
        PlayerPrefs.SetInt(highscoreKey, 5);
        Assert.AreEqual(5, highscoreService.highscore, "Highscore wasn't set correctly");

        bool highscoreWasUpdated = highscoreService.CheckHighscoreAndUpdateIfNeeded(2);
        Assert.IsFalse(highscoreWasUpdated);
    }

    [Test]
    public void Highscore_is_not_upadting_if_equal()
    {
        PlayerPrefs.SetInt(highscoreKey, 5);
        Assert.AreEqual(5, highscoreService.highscore, "Highscore wasn't set correctly");

        bool highscoreWasUpdated = highscoreService.CheckHighscoreAndUpdateIfNeeded(5);
        Assert.IsFalse(highscoreWasUpdated);
    }
}
