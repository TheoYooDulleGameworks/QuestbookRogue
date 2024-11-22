using System.Collections.Generic;
using UnityEngine;

public class DiceTabUI : MonoBehaviour
{
    [Header("Datasets")]
    public PlayerDiceSO playerDiceData;
    public GameObject uiDicePrefab;

    [Header("Components")]
    [SerializeField] private RectTransform StrDiceGroup;
    [SerializeField] private RectTransform AgiDiceGroup;
    [SerializeField] private RectTransform IntDiceGroup;
    [SerializeField] private RectTransform WilDiceGroup;
    [SerializeField] private RectTransform SpecialDiceGroup;

    private void Start()
    {
        if (playerDiceData != null)
        {
            SetPlayerDices();
        }

        playerDiceData.StrDice.OnValueChanged += UpdateStrGroup;
        playerDiceData.AgiDice.OnValueChanged += UpdateAgiGroup;
        playerDiceData.IntDice.OnValueChanged += UpdateIntGroup;
        playerDiceData.WilDice.OnValueChanged += UpdateWilGroup;
    }

    public void SetPlayerDices()
    {
        List<UIDice> previousUIDices = new List<UIDice>();

        foreach (UIDice uiDice in StrDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in AgiDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in IntDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in WilDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in SpecialDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in previousUIDices)
        {
            uiDice.DestroyUIDice();
        }
        int StrDiceAmount = playerDiceData.StrDice.Value;
        int AgiDiceAmount = playerDiceData.AgiDice.Value;
        int IntDiceAmount = playerDiceData.IntDice.Value;
        int WilDiceAmount = playerDiceData.WilDice.Value;

        for (int i = 0; i < StrDiceAmount; i++)
        {
            GameObject StruiDicePrefab = Instantiate(uiDicePrefab);
            StruiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StruiDicePrefab.name = $"Str  Dice _({i + 1})";
            StruiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrDice_UI;
            StruiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < AgiDiceAmount; i++)
        {
            GameObject AgiuiDicePrefab = Instantiate(uiDicePrefab);
            AgiuiDicePrefab.transform.SetParent(AgiDiceGroup, false);
            AgiuiDicePrefab.name = $"Agi  Dice _({i + 1})";
            AgiuiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.AgiDice_UI;
            AgiuiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < IntDiceAmount; i++)
        {
            GameObject IntuiDicePrefab = Instantiate(uiDicePrefab);
            IntuiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntuiDicePrefab.name = $"Int  Dice _({i + 1})";
            IntuiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntDice_UI;
            IntuiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < WilDiceAmount; i++)
        {
            GameObject WiluiDicePrefab = Instantiate(uiDicePrefab);
            WiluiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WiluiDicePrefab.name = $"Wil  Dice _({i + 1})";
            WiluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilDice_UI;
            WiluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateStrGroup()
    {
        foreach (UIDice uiDice in StrDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int StrDiceAmount = playerDiceData.StrDice.Value;

        for (int i = 0; i < StrDiceAmount; i++)
        {
            GameObject StruiDicePrefab = Instantiate(uiDicePrefab);
            StruiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StruiDicePrefab.name = $"Str  Dice _({i + 1})";
            StruiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrDice_UI;
            StruiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateAgiGroup()
    {
        foreach (UIDice uiDice in AgiDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int AgiDiceAmount = playerDiceData.AgiDice.Value;

        for (int i = 0; i < AgiDiceAmount; i++)
        {
            GameObject AgiuiDicePrefab = Instantiate(uiDicePrefab);
            AgiuiDicePrefab.transform.SetParent(AgiDiceGroup, false);
            AgiuiDicePrefab.name = $"Agi  Dice _({i + 1})";
            AgiuiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.AgiDice_UI;
            AgiuiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateIntGroup()
    {
        foreach (UIDice uiDice in IntDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int IntDiceAmount = playerDiceData.IntDice.Value;

        for (int i = 0; i < IntDiceAmount; i++)
        {
            GameObject IntuiDicePrefab = Instantiate(uiDicePrefab);
            IntuiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntuiDicePrefab.name = $"Int  Dice _({i + 1})";
            IntuiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntDice_UI;
            IntuiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateWilGroup()
    {
        foreach (UIDice uiDice in WilDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int WilDiceAmount = playerDiceData.WilDice.Value;

        for (int i = 0; i < WilDiceAmount; i++)
        {
            GameObject WiluiDicePrefab = Instantiate(uiDicePrefab);
            WiluiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WiluiDicePrefab.name = $"Wil  Dice _({i + 1})";
            WiluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilDice_UI;
            WiluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }
}

