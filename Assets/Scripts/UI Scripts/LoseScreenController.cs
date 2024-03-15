using TMPro;
using UnityEngine;

public class LoseScreenController : MonoBehaviour
{
    private static LoseScreenController instance;
    public static LoseScreenController Instance {
        get { return instance; }
    }
    [SerializeField]
    string destroyGreenLoseText, destroyGreenLoseText2, negativeLoseText, negativeLoseText2;
    [SerializeField]
    GameObject LoseCanvas;
    [SerializeField]
    TMP_Text LoseText, LoseText2;
    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }
    public void OnDestroyGreen() {
        LoseText.text = destroyGreenLoseText;
        LoseText2.text = destroyGreenLoseText2;
        Lose();
    }
    public void OnNegativeScore() {
        LoseText.text = negativeLoseText;
        LoseText2.text = negativeLoseText2;
        Lose();
    }
    private void Lose() {
        InputManager.Instance.LockedInput = true;
        LoseCanvas.SetActive(true);
    }
    public void RestartLevel() {
        SceneController.Instance.RestartScene();
    }
}
