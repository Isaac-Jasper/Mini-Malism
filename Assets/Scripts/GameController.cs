using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance {
        get { return instance; }
    }


    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }

    public void CheckIfWin() {
        Debug.Log(CharacterTracker.Instance.CountEvils());
        if (CharacterTracker.Instance.IsEvilsPurged()) {
            Debug.Log("win");
            Win();
        }
    }

    public void Win() {
        WinScreenController.Instance.OnWin();
    }
}
