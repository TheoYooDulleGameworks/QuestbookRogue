using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image transitionImage;

    [SerializeField] private RectTransform selectsScene;
    [SerializeField] private RectTransform contentsScene;

    public void TransitionToContents(SelectionData _selectionData)
    {
        SetContentsScene(_selectionData);
        StartCoroutine(FadeAndContents());
    }

    public void TransitionToSelects()
    {
        // SetSelectsScene();
        StartCoroutine(FadeAndSelects());
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.ActivateQuestCard();
        }
    }

    private IEnumerator FadeAndContents()
    {
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.DeActivateQuestCard();
        }

        yield return StartCoroutine(FadeIn());

        selectsScene.gameObject.SetActive(false);
        contentsScene.gameObject.SetActive(true);

        yield return StartCoroutine(FadeOut());
    }

    private void SetContentsScene(SelectionData _selectionData)
    {

    }

    private void SetSelectsScene()
    {

    }



    // TRANSITIONS //

    private IEnumerator FadeAndSelects()
    {
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.DeActivateQuestCard();
        }

        yield return StartCoroutine(FadeIn());

        contentsScene.gameObject.SetActive(false);
        selectsScene.gameObject.SetActive(true);

        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;

        Color color = transitionImage.color;
        color.a = 0;
        transitionImage.color = color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / duration);
            transitionImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        transitionImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;

        Color color = transitionImage.color;
        color.a = 1;
        transitionImage.color = color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / duration);
            transitionImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        transitionImage.color = color;
    }
}