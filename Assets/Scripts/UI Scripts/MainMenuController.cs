using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private int level1;
    public void StartGame() {
        SceneController.Instance.ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
