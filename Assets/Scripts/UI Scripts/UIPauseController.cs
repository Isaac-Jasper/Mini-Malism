using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    [SerializeField]
    GameObject PauseCanvas;
    private bool isPaused = false;
    public void OnEnable(){
        Subscribe();
    }
    public void OnDisable(){
        UnSubscribe();
    }
    private void Subscribe() {
        InputManager.Instance.OnGameplayPauseStarted += Pause;
    }
    private void UnSubscribe() {
        InputManager.Instance.OnGameplayPauseStarted -= Pause;
    }
    private void Pause(float time) {
        if (!isPaused) {
            isPaused = true;
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
        } else {
            isPaused = false;
            Time.timeScale = 1;
            PauseCanvas.SetActive(false);
        }
    }
}
