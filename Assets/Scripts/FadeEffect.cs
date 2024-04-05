using System.Collections;
using UnityEngine;
using TMPro;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; //페이드 되는 시간
    private TextMeshProUGUI textFade; //페이드 효과에 사용되는 TMPro

    private void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();

        //Fade효과를 in->out 무한 반복한다.
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0)); //fade in

            yield return StartCoroutine(Fade(0, 1)); //fade out
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        }
    }
}
