using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    private static WinScreenController instance;
    public static WinScreenController Instance {
        get { return instance; }
    }
    [SerializeField]
    GameObject WinCanvas;
    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }
    public void OnWin() {
        InputManager.Instance.LockedInput = true;
        WinCanvas.SetActive(true);
    }
    public void NextLevel() {
        SceneController.Instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
