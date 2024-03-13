using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private int level1;
    public void StartGame() {
        SceneController.Instance.ChangeScene(level1);
    }
}
