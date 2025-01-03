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
    public float delayBetweenAnnouncements = 3f;

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
            yield return new WaitForSeconds(delayBetweenAnnouncements);
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
}
