using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroductionController : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] boxers;
    public TMP_Text announcementText;
    public float delayBetweenAnnouncements = 8f;

    public Light[] spotlights;
    public float maxIntensity = 1500f;
    public float fadeDuration = 8f;

    private int currentBoxerIndex = 0;

    public void StartIntroduction()
    {
        StartCoroutine(PlayIntroduction());
    }

    IEnumerator PlayIntroduction()
    {
        // Présentation des boxeurs un par un
        foreach (GameObject boxer in boxers)
        {
            ActivateCamera(currentBoxerIndex);
            announcementText.text = "Présentation : " + boxer.name;

            yield return StartCoroutine(FadeLightIntensity(spotlights[currentBoxerIndex], 0, maxIntensity, fadeDuration));

            yield return new WaitForSeconds(delayBetweenAnnouncements - fadeDuration * 2);

            yield return StartCoroutine(FadeLightIntensity(spotlights[currentBoxerIndex], maxIntensity, 500, fadeDuration));


            currentBoxerIndex++;
        }

        // Nettoyer le texte après la présentation
        announcementText.text = "";

        DesactivateCamera();
    }

    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == index);
        }
    }

    void DesactivateCamera()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
    }

    IEnumerator FadeLightIntensity(Light spotlight, float startIntensity, float endIntensity, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            spotlight.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsed / duration);
            yield return null;
        }

        spotlight.intensity = endIntensity;
    }
}
