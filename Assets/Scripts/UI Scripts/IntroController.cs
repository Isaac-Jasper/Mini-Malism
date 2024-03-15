using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    CanvasGroup MalismCanvasGroup;
    [SerializeField]
    CanvasGroup DefinitionCanvasGroup;
    [SerializeField]
    float startDelay, fadeInSpeed, delay2, secondFadeSpeed, delay3, fadeOutSpeed, delay4;
     
    public void Start() {
        StartCoroutine(IntroCutscene());
    }

    private IEnumerator IntroCutscene() {
        yield return new WaitForSeconds(startDelay);

        while (MalismCanvasGroup.alpha < 1) {
            MalismCanvasGroup.alpha = MalismCanvasGroup.alpha += fadeInSpeed * Time.deltaTime;
            yield return null;
        }
        MalismCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(delay2);

        while (DefinitionCanvasGroup.alpha < 1) {
            DefinitionCanvasGroup.alpha = DefinitionCanvasGroup.alpha += secondFadeSpeed * Time.deltaTime;
            yield return null;
        }
        DefinitionCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(delay3);

        while (MalismCanvasGroup.alpha > 0) {
            MalismCanvasGroup.alpha = MalismCanvasGroup.alpha -= fadeOutSpeed * Time.deltaTime;
            DefinitionCanvasGroup.alpha = DefinitionCanvasGroup.alpha -= fadeOutSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(delay4);

        MalismCanvasGroup.alpha = 0;
        DefinitionCanvasGroup.alpha = 0;

        SceneManager.LoadScene(1);
    }
}
