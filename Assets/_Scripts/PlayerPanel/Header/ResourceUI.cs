using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private PlayerStatusSO playerStatus;

    [SerializeField] private RectTransform coin10thRect;
    [SerializeField] private RectTransform coin1thRect;
    [SerializeField] private List<Sprite> coinNumbers = new List<Sprite>();

    [SerializeField] private RectTransform provision10thRect;
    [SerializeField] private RectTransform provision1thRect;
    [SerializeField] private List<Sprite> provisionNumbers = new List<Sprite>();

    private void Start()
    {
        playerStatus.Coin.OnValueChanged += UpdateCoinUI;
        playerStatus.Provision.OnValueChanged += UpdateProvisionUI;

        SetDefaultResources();
    }

    private void OnDestroy()
    {
        playerStatus.Coin.OnValueChanged -= UpdateCoinUI;
        playerStatus.Provision.OnValueChanged -= UpdateProvisionUI;
    }

    private void SetDefaultResources()
    {
        UpdateCoinUI();
        UpdateProvisionUI();
    }

    private void UpdateCoinUI()
    {
        int coinValue = playerStatus.Coin.Value;
        int coinValue10th = coinValue / 10;
        int coinValue1th = coinValue % 10;

        if (coinValue >= 10)
        {
            if (coin10thRect.gameObject != null)
            {
                coin10thRect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (coin10thRect.gameObject != null)
            {
                coin10thRect.gameObject.SetActive(false);
            }
        }

        switch (coinValue10th)
        {
            case 0:
                coin10thRect.GetComponent<Image>().sprite = null;
                break;
            case 1:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[1];
                break;
            case 2:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[2];
                break;
            case 3:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[3];
                break;
            case 4:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[4];
                break;
            case 5:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[5];
                break;
            case 6:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[6];
                break;
            case 7:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[7];
                break;
            case 8:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[8];
                break;
            case 9:
                coin10thRect.GetComponent<Image>().sprite = coinNumbers[9];
                break;
            default:
                break;
        }
        switch (coinValue1th)
        {
            case 0:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[0];
                break;
            case 1:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[1];
                break;
            case 2:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[2];
                break;
            case 3:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[3];
                break;
            case 4:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[4];
                break;
            case 5:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[5];
                break;
            case 6:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[6];
                break;
            case 7:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[7];
                break;
            case 8:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[8];
                break;
            case 9:
                coin1thRect.GetComponent<Image>().sprite = coinNumbers[9];
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
