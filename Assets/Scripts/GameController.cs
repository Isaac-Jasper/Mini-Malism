using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isTerminal = false;

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
        if (CharacterTracker.Instance.IsEvilsPurged()) {
            StartCoroutine(Win());
        }
    }

    public bool IsTerminal() {
        return isTerminal;
    }

    public IEnumerator Win() {
        yield return new WaitForSeconds(0.1f);
        isTerminal = true;
        WinScreenController.Instance.OnWin();
    }
    public void LoseNegative() {
        isTerminal = true;
        LoseScreenController.Instance.OnNegativeScore();
    }
    public void LoseGreenDeath() {
        isTerminal = true;
        LoseScreenController.Instance.OnDestroyGreen();
    }
}
