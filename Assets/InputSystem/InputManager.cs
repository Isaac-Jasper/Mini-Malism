using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour {
    private static InputManager instance;
    public static InputManager Instance {
        get { return instance; }
    }
    public delegate void GameplayClickStarted(Vector2 position, float time);
    public event GameplayClickStarted OnGameplayClickStarted;
    public delegate void GameplayRestartStarted(float time);
    public event GameplayRestartStarted OnGameplayRestartStarted;
    public delegate void GameplayPauseStarted(float time);
    public event GameplayPauseStarted OnGameplayPauseStarted;

    private Input mainInput;
    private Camera mainCamera;

    public bool LockedInput;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        mainCamera = Camera.main;
        mainInput = new Input();

        mainInput.Gameplay.Click.started += Gameplay_Click_Started;
        mainInput.Gameplay.Restart.started += Gameplay_Restart_Started;
        mainInput.Gameplay.Pause.started += Gameplay_Pause_Started;
    }

    /// <param name="name"></param>
    /// <returns>True if the input can be done at that location and time</returns>
    public bool CheckIfCanInput() {
        if (Time.timeScale == 0) return false;
        if (LockedInput) return false;

        return true;
    }
    public Vector2 GetClickPosition() {
        return mainInput.Gameplay.MousePosition.ReadValue<Vector2>();
    }
    public Vector2 GetClickWorldPosition() {
        return mainCamera.ScreenToWorldPoint(GetClickPosition());
    }
    private void Gameplay_Click_Started(InputAction.CallbackContext ctx) {
        //Debug.Log("Gameplat Click Started");
        if (!CheckIfCanInput()) return;
        OnGameplayClickStarted?.Invoke(GetClickWorldPosition(), (float)ctx.startTime);
    }
    private void Gameplay_Pause_Started(InputAction.CallbackContext ctx) {
        //Debug.Log("Gameplat Pause Started");
        OnGameplayPauseStarted?.Invoke((float)ctx.startTime);
    }
    private void Gameplay_Restart_Started(InputAction.CallbackContext ctx) {
        //Debug.Log("Gameplay Restart Started");
        if (!CheckIfCanInput()) return;
        OnGameplayRestartStarted?.Invoke((float)ctx.startTime);
    }

    private void OnEnable() {
        mainInput.Gameplay.Enable();
    }

    private void OnDisable() {
        mainInput.Gameplay.Disable();
    }
}