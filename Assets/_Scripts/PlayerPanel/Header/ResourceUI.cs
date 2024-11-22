using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private PlayerStatusSO playerStatus;

    [SerializeField] private RectTransform gold10thRect;
    [SerializeField] private RectTransform gold1thRect;
    [SerializeField] private List<Sprite> goldNumbers = new List<Sprite>();

    [SerializeField] private RectTransform provision10thRect;
    [SerializeField] private RectTransform provision1thRect;
    [SerializeField] private List<Sprite> provisionNumbers = new List<Sprite>();

    private void Start()
    {
        playerStatus.Gold.OnValueChanged += UpdateGoldUI;
        playerStatus.Provision.OnValueChanged += UpdateProvisionUI;

        SetDefaultResources();
    }

    private void OnDestroy()
    {
        playerStatus.Gold.OnValueChanged -= UpdateGoldUI;
        playerStatus.Provision.OnValueChanged -= UpdateProvisionUI;
    }

    private void SetDefaultResources()
    {
        UpdateGoldUI();
        UpdateProvisionUI();
    }

    private void UpdateGoldUI()
    {
        int goldValue = playerStatus.Gold.Value;
        int goldValue10th = goldValue / 10;
        int goldValue1th = goldValue % 10;

        if (goldValue >= 10)
        {
            if (gold10thRect.gameObject != null)
            {
                gold10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (gold10thRect.gameObject != null)
            {
                gold10thRect.gameObject.SetActive(false);
            }
        }

        switch (goldValue10th)
        {
            case 0:
                gold10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[1];
                break;
            case 2:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[2];
                break;
            case 3:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[3];
                break;
            case 4:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[4];
                break;
            case 5:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[5];
                break;
            case 6:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[6];
                break;
            case 7:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[7];
                break;
            case 8:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[8];
                break;
            case 9:
                gold10thRect.GetComponent<Image>().sprite = goldNumbers[9];
                break;
            default:
                break;
        }
        switch (goldValue1th)
        {
            case 0:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[0];
                break;
            case 1:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[1];
                break;
            case 2:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[2];
                break;
            case 3:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[3];
                break;
            case 4:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[4];
                break;
            case 5:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[5];
                break;
            case 6:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[6];
                break;
            case 7:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[7];
                break;
            case 8:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[8];
                break;
            case 9:
                gold1thRect.GetComponent<Image>().sprite = goldNumbers[9];
                break;
            default:
                break;
        }
    }

    private void UpdateProvisionUI()
    {
        int provisionValue = playerStatus.Provision.Value;
        int provisionValue10th = provisionValue / 10;
        int provisionValue1th = provisionValue % 10;

        if (provisionValue >= 10)
        {
            if (provision10thRect.gameObject != null)
            {
                provision10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (provision10thRect.gameObject != null)
            {
                provision10thRect.gameObject.SetActive(false);
            }
        }

        switch (provisionValue10th)
        {
            case 0:
                provision10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[1];
                break;
            case 2:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[2];
                break;
            case 3:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[3];
                break;
            case 4:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[4];
                break;
            case 5:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[5];
                break;
            case 6:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[6];
                break;
            case 7:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[7];
                break;
            case 8:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[8];
                break;
            case 9:
                provision10thRect.GetComponent<Image>().sprite = provisionNumbers[9];
                break;
            default:
                break;
        }
        switch (provisionValue1th)
        {
            case 0:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[0];
                break;
            case 1:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[1];
                break;
            case 2:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[2];
                break;
            case 3:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[3];
                break;
            case 4:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[4];
                break;
            case 5:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[5];
                break;
            case 6:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[6];
                break;
            case 7:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[7];
                break;
            case 8:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[8];
                break;
            case 9:
                provision1thRect.GetComponent<Image>().sprite = provisionNumbers[9];
                break;
            default:
                break;
        }
    }
}
