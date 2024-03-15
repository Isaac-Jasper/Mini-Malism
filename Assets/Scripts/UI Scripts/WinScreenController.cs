using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    private static WinScreenController instance;
    public static WinScreenController Instance {
        get { return instance; }
    }
    [SerializeField]
    string goodWinText, badWinText, neutralText, developerWinText;
    [SerializeField]
    GameObject WinCanvas;
    [SerializeField]
    TMP_Text winText, highscoreText;
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
        if (UIScoreController.Instance.isLoseScore()) {
            GameController.Instance.LoseNegative();
            return;
        }
        if (UIScoreController.Instance.isBadScore()) winText.text = badWinText;
        if (UIScoreController.Instance.isNuetralScore()) winText.text = neutralText;
        if (UIScoreController.Instance.isGoodScore()) winText.text = goodWinText;
        if (UIScoreController.Instance.isDeveloperScore()) winText.text = developerWinText;
        highscoreText.text = highscoreText.text +" "+UIScoreController.Instance.getDeveloperScore().ToString("00000");
        WinCanvas.SetActive(true);
    }
    public void NextLevel() {
        SceneController.Instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel() {
        SceneController.Instance.RestartScene();
    }
}
