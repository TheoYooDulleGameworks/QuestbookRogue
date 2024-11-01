using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image transitionImage;

    [SerializeField] private RectTransform selectsScene;
    [SerializeField] private RectTransform contentsScene;
    [SerializeField] private RectTransform rollDicePanel;
    [SerializeField] private PlayerDiceSO playerDiceData;

    private void Awake()
    {
        if (!selectsScene.gameObject.activeSelf)
        {
            selectsScene.gameObject.SetActive(true);
        }
        if (contentsScene.gameObject.activeSelf)
        {
            contentsScene.gameObject.SetActive(false);
        }
        if (rollDicePanel.gameObject.activeSelf)
        {
            rollDicePanel.gameObject.SetActive(false);
        }

        ResetContentsScene();
        ResetRollDicePanel();
    }

    public void TransitionToContents(SelectionData _selectionData)
    {
        SetContentsScene(_selectionData);
        SetDicePanel();
        StartCoroutine(FadeAndContents());
    }

    public void TransitionToSelects()
    {
        StartCoroutine(FadeAndSelects());
        ResetContentsScene();
        ResetRollDicePanel();

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
        rollDicePanel.gameObject.SetActive(true);

        InfoTabController.Instance.HandleSkillTabActivate();

        yield return StartCoroutine(FadeOut());
    }

    private void SetContentsScene(SelectionData _selectionData)
    {
        for (int i = 0; i < _selectionData.questContents.Count; i++)
        {
            GameObject content = Instantiate(_selectionData.questContents[i].contentTemplate);
            content.transform.SetParent(contentsScene, false);
            content.name = $"content_({i + 1})";

            content.GetComponent<IContent>().SetContentComponents(_selectionData.questContents[i]);
        }
    }

    private void ResetContentsScene()
    {
        List<IContent> completedContents = new List<IContent>(contentsScene.GetComponentsInChildren<IContent>());
        foreach (var content in completedContents)
        {
            content.DestroyContent();
        }
    }

    private void SetDicePanel()
    {
        int StrNormalDiceAmount = playerDiceData.StrNormalDice.Value;
        int DexNormalDiceAmount = playerDiceData.DexNormalDice.Value;
        int IntNormalDiceAmount = playerDiceData.IntNormalDice.Value;
        int WilNormalDiceAmount = playerDiceData.WilNormalDice.Value;
        int StrAdvancedDiceAmount = playerDiceData.StrAdvancedDice.Value;
        int DexAdvancedDiceAmount = playerDiceData.DexAdvancedDice.Value;
        int IntAdvancedDiceAmount = playerDiceData.IntAdvancedDice.Value;
        int WilAdvancedDiceAmount = playerDiceData.WilAdvancedDice.Value;

        List<GameObject> dicePrefabs = new List<GameObject>();

        for (int i = 0; i < StrNormalDiceAmount; i++)
        {
            GameObject StrNormalDicePrefab = Instantiate(playerDiceData.StrNormalDice_Roll);
            StrNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(StrNormalDicePrefab);
        }

        for (int i = 0; i < DexNormalDiceAmount; i++)
        {
            GameObject DexNormalDicePrefab = Instantiate(playerDiceData.DexNormalDice_Roll);
            DexNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(DexNormalDicePrefab);
        }

        for (int i = 0; i < IntNormalDiceAmount; i++)
        {
            GameObject IntNormalDicePrefab = Instantiate(playerDiceData.IntNormalDice_Roll);
            IntNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(IntNormalDicePrefab);
        }

        for (int i = 0; i < WilNormalDiceAmount; i++)
        {
            GameObject WilNormalDicePrefab = Instantiate(playerDiceData.WilNormalDice_Roll);
            WilNormalDicePrefab.transform.SetParent(rollDicePanel, false);
            dicePrefabs.Add(WilNormalDicePrefab);
        }

        for (int i = 0; i < dicePrefabs.Count; i++)
        {
            switch (i)
            {
                case int n when n >= 0 && n <= 11:
                    dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-292 + (i * 100), -470);
                    break;
                case int n when n > 11 && n <= 23:
                    dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-292 + ((i - 12) * 100), -370);
                    break;
                case int n when n > 23 && n <= 35:
                    dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-292 + ((i - 24) * 100), -270);
                    break;
                case int n when n > 35 && n <= 47:
                    dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-292 + ((i - 36) * 100), -170);
                    break;
                case int n when n > 47 && n <= 59:
                    dicePrefabs[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-292 + ((i - 48) * 100), -70);
                    break;
            }
        }
    }

    private void ResetRollDicePanel()
    {
        List<RollDice> previousRollDices = new List<RollDice>(rollDicePanel.GetComponentsInChildren<RollDice>());
        foreach (var rollDices in previousRollDices)
        {
            rollDices.DestroyRollDice();
        }
    }

    private IEnumerator FadeAndSelects()
    {
        List<Quest> selectableQuests = new List<Quest>(selectsScene.GetComponentsInChildren<Quest>());
        foreach (var quest in selectableQuests)
        {
            quest.DeActivateQuestCard();
        }

        yield return StartCoroutine(FadeIn());

        contentsScene.gameObject.SetActive(false);
        rollDicePanel.gameObject.SetActive(false);
        selectsScene.gameObject.SetActive(true);

        InfoTabController.Instance.HandleDiceTabActivate();

        yield return StartCoroutine(FadeOut());
    }



    // TRANSITIONS //

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