using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance {
        get { return instance; }
    }

    [SerializeField]
    private GameObject fadeObject;
    [SerializeField]
    private CanvasGroup textFadeGroup;
    [SerializeField]
    private float transitionSpeed;
    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }
    public void Start() {
        StartScene();
    }
    public void ChangeScene(int scene) {
        StopAllCoroutines();
        StartCoroutine(ChangeSceneTimed(scene));
    }
    private IEnumerator ChangeSceneTimed(int scene) {
        while (textFadeGroup.alpha < 1) {
            textFadeGroup.alpha = textFadeGroup.alpha += transitionSpeed / 5 * Time.deltaTime;
            yield return null;
        }
        textFadeGroup.alpha = 1;
        SceneManager.LoadScene(scene);
    }
    public void RestartScene() {
        StopAllCoroutines();
        StartCoroutine(RestartSceneTimed());
    }
    private IEnumerator RestartSceneTimed() {
        while (textFadeGroup.alpha < 1) {
            textFadeGroup.alpha = textFadeGroup.alpha += transitionSpeed / 5 * Time.deltaTime;
            yield return null;
        }
        textFadeGroup.alpha = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void StartScene() {
        StopAllCoroutines();
        StartCoroutine(StartSceneTimed());
    }
    private IEnumerator StartSceneTimed() {
        while (textFadeGroup.alpha > 0) {
            textFadeGroup.alpha = textFadeGroup.alpha -= transitionSpeed / 5 * Time.deltaTime;
            yield return null;
        }
        textFadeGroup.alpha = 0;
    }
}
