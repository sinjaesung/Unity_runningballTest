using System.Collections;
using UnityEngine;
using TMPro;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; //���̵� �Ǵ� �ð�
    private TextMeshProUGUI textFade; //���̵� ȿ���� ���Ǵ� TMPro

    private void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();

        //Fadeȿ���� in->out ���� �ݺ��Ѵ�.
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
