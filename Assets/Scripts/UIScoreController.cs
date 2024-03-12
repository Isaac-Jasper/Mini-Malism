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
        scoreMult = math.log2(Time.time - startTime + 1);
        UpdateScore();
    }
    public void AddScore(int change) {
        score += (int) ((float) change * scoreMult);
    }
    public void AddScoreNoMult(int change) {
        score += change;
    }
    public void UpdateScore() {
        scoreText.text = ""+score.ToString("00000000");
    }
}
