using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeOverlay;
    public float fadeSpeed = 1f;

    public IEnumerator FadeOut()
    {
        while (fadeOverlay.color.a < 1)
        {
            Color newColor = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, fadeOverlay.color.a + (fadeSpeed * Time.deltaTime));
            fadeOverlay.color = newColor;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1); // 2 seconds delay

        while (fadeOverlay.color.a > 0)
        {
            Color newColor = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, fadeOverlay.color.a - (fadeSpeed * Time.deltaTime));
            fadeOverlay.color = newColor;
            yield return null;
        }
    }

}
