using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class UIScoreController : MonoBehaviour
{
    private static UIScoreController instance;
    public static UIScoreController Instance {
        get { return instance; }
    }
    [SerializeField]
    private int badScore, goodScore, developerScore;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private int score;
    [SerializeField]
    float scoreMult;
    float startTime;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        UpdateScore();
        startTime = Time.time;
    }
    private void Update() {
        scoreMult = CurrentScoreMult();
        UpdateScore();
    }

    public float CurrentScoreMult() {
        return  math.log2(Time.time - startTime + 1) + 1;
    }
    public bool isLoseScore() {
        if (score <= 0) return true;
        return false;
    }
    public bool isBadScore() {
        if (score <= 0) return false;
        if (score <= badScore) return true;
        return false;
    }
    public bool isNuetralScore() {
        if (score < goodScore && score > badScore) return true;
        return false;
    }
    public bool isGoodScore() {
        if (score >= developerScore) return false;
        if (score >= goodScore) { Debug.Log(score); return true; }
        return false;
    }
    public bool isDeveloperScore() {
        if (score >= developerScore) return true;
        return false;
    }
    public void AddScore(int change) {
        score += (int) ((float) change * scoreMult);
    }
    public void AddScoreNoMult(int change) {
        score += change;
    }
    public void UpdateScore() {
        scoreText.text = ""+score.ToString("00000");
    }
    public int getDeveloperScore() {
        return developerScore;
    }
}
