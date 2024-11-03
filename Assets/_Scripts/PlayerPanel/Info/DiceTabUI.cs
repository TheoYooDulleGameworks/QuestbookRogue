using System.Collections.Generic;
using UnityEngine;

public class DiceTabUI : MonoBehaviour
{
    [Header("Datasets")]
    public PlayerDiceSO playerDiceData;
    public GameObject uiDicePrefab;

    [Header("Components")]
    [SerializeField] private RectTransform StrDiceGroup;
    [SerializeField] private RectTransform DexDiceGroup;
    [SerializeField] private RectTransform IntDiceGroup;
    [SerializeField] private RectTransform WilDiceGroup;
    [SerializeField] private RectTransform SpecialDiceGroup;

    private void Start()
    {
        if (playerDiceData != null)
        {
            SetPlayerDices();
        }

        playerDiceData.StrNormalDice.OnValueChanged += UpdateStrGroup;
        playerDiceData.StrAdvancedDice.OnValueChanged += UpdateStrGroup;
        playerDiceData.DexNormalDice.OnValueChanged += UpdateDexGroup;
        playerDiceData.DexAdvancedDice.OnValueChanged += UpdateDexGroup;
        playerDiceData.IntNormalDice.OnValueChanged += UpdateIntGroup;
        playerDiceData.IntAdvancedDice.OnValueChanged += UpdateIntGroup;
        playerDiceData.WilNormalDice.OnValueChanged += UpdateWilGroup;
        playerDiceData.WilAdvancedDice.OnValueChanged += UpdateWilGroup;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestGetDice();
        }
    }

    public void SetPlayerDices()
    {
        List<UIDice> previousUIDices = new List<UIDice>();

        foreach (UIDice uiDice in StrDiceGroup.GetComponentsInChildren<UIDice>())
        {
            previousUIDices.Add(uiDice);
        }
        foreach (UIDice uiDice in DexDiceGroup.GetComponentsInChildren<UIDice>())
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

        int StrAdvancedDiceAmount = playerDiceData.StrAdvancedDice.Value;
        int DexAdvancedDiceAmount = playerDiceData.DexAdvancedDice.Value;
        int IntAdvancedDiceAmount = playerDiceData.IntAdvancedDice.Value;
        int WilAdvancedDiceAmount = playerDiceData.WilAdvancedDice.Value;

        int StrNormalDiceAmount = playerDiceData.StrNormalDice.Value;
        int DexNormalDiceAmount = playerDiceData.DexNormalDice.Value;
        int IntNormalDiceAmount = playerDiceData.IntNormalDice.Value;
        int WilNormalDiceAmount = playerDiceData.WilNormalDice.Value;

        for (int i = 0; i < StrAdvancedDiceAmount; i++)
        {
            GameObject StrAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            StrAdvanceduiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StrAdvanceduiDicePrefab.name = $"Str Advanced Dice _({i + 1})";
            StrAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrAdvancedDice_UI;
            StrAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < StrNormalDiceAmount; i++)
        {
            GameObject StrNormaluiDicePrefab = Instantiate(uiDicePrefab);
            StrNormaluiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StrNormaluiDicePrefab.name = $"Str Normal Dice _({i + 1})";
            StrNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrNormalDice_UI;
            StrNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < DexAdvancedDiceAmount; i++)
        {
            GameObject DexAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            DexAdvanceduiDicePrefab.transform.SetParent(DexDiceGroup, false);
            DexAdvanceduiDicePrefab.name = $"Dex Advanced Dice _({i + 1})";
            DexAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.DexAdvancedDice_UI;
            DexAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < DexNormalDiceAmount; i++)
        {
            GameObject DexNormaluiDicePrefab = Instantiate(uiDicePrefab);
            DexNormaluiDicePrefab.transform.SetParent(DexDiceGroup, false);
            DexNormaluiDicePrefab.name = $"Dex Normal Dice _({i + 1})";
            DexNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.DexNormalDice_UI;
            DexNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < IntAdvancedDiceAmount; i++)
        {
            GameObject IntAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            IntAdvanceduiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntAdvanceduiDicePrefab.name = $"Int Advanced Dice _({i + 1})";
            IntAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntAdvancedDice_UI;
            IntAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < IntNormalDiceAmount; i++)
        {
            GameObject IntNormaluiDicePrefab = Instantiate(uiDicePrefab);
            IntNormaluiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntNormaluiDicePrefab.name = $"Int Normal Dice _({i + 1})";
            IntNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntNormalDice_UI;
            IntNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < WilAdvancedDiceAmount; i++)
        {
            GameObject WilAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            WilAdvanceduiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WilAdvanceduiDicePrefab.name = $"Wil Advanced Dice _({i + 1})";
            WilAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilAdvancedDice_UI;
            WilAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < WilNormalDiceAmount; i++)
        {
            GameObject WilNormaluiDicePrefab = Instantiate(uiDicePrefab);
            WilNormaluiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WilNormaluiDicePrefab.name = $"Wil Normal Dice _({i + 1})";
            WilNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilNormalDice_UI;
            WilNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateStrGroup()
    {
        foreach (UIDice uiDice in StrDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int StrAdvancedDiceAmount = playerDiceData.StrAdvancedDice.Value;
        int StrNormalDiceAmount = playerDiceData.StrNormalDice.Value;

        for (int i = 0; i < StrAdvancedDiceAmount; i++)
        {
            GameObject StrAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            StrAdvanceduiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StrAdvanceduiDicePrefab.name = $"Str Advanced Dice _({i + 1})";
            StrAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrAdvancedDice_UI;
            StrAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < StrNormalDiceAmount; i++)
        {
            GameObject StrNormaluiDicePrefab = Instantiate(uiDicePrefab);
            StrNormaluiDicePrefab.transform.SetParent(StrDiceGroup, false);
            StrNormaluiDicePrefab.name = $"Str Normal Dice _({i + 1})";
            StrNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.StrNormalDice_UI;
            StrNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateDexGroup()
    {
        foreach (UIDice uiDice in DexDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int DexAdvancedDiceAmount = playerDiceData.DexAdvancedDice.Value;
        int DexNormalDiceAmount = playerDiceData.DexNormalDice.Value;

        for (int i = 0; i < DexAdvancedDiceAmount; i++)
        {
            GameObject DexAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            DexAdvanceduiDicePrefab.transform.SetParent(DexDiceGroup, false);
            DexAdvanceduiDicePrefab.name = $"Dex Advanced Dice _({i + 1})";
            DexAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.DexAdvancedDice_UI;
            DexAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < DexNormalDiceAmount; i++)
        {
            GameObject DexNormaluiDicePrefab = Instantiate(uiDicePrefab);
            DexNormaluiDicePrefab.transform.SetParent(DexDiceGroup, false);
            DexNormaluiDicePrefab.name = $"Dex Normal Dice _({i + 1})";
            DexNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.DexNormalDice_UI;
            DexNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateIntGroup()
    {
        foreach (UIDice uiDice in IntDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int IntAdvancedDiceAmount = playerDiceData.IntAdvancedDice.Value;
        int IntNormalDiceAmount = playerDiceData.IntNormalDice.Value;

        for (int i = 0; i < IntAdvancedDiceAmount; i++)
        {
            GameObject IntAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            IntAdvanceduiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntAdvanceduiDicePrefab.name = $"Int Advanced Dice _({i + 1})";
            IntAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntAdvancedDice_UI;
            IntAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < IntNormalDiceAmount; i++)
        {
            GameObject IntNormaluiDicePrefab = Instantiate(uiDicePrefab);
            IntNormaluiDicePrefab.transform.SetParent(IntDiceGroup, false);
            IntNormaluiDicePrefab.name = $"Int Normal Dice _({i + 1})";
            IntNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.IntNormalDice_UI;
            IntNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void UpdateWilGroup()
    {
        foreach (UIDice uiDice in WilDiceGroup.GetComponentsInChildren<UIDice>())
        {
            uiDice.DestroyUIDice();
        }

        int WilAdvancedDiceAmount = playerDiceData.WilAdvancedDice.Value;
        int WilNormalDiceAmount = playerDiceData.WilNormalDice.Value;

        for (int i = 0; i < WilAdvancedDiceAmount; i++)
        {
            GameObject WilAdvanceduiDicePrefab = Instantiate(uiDicePrefab);
            WilAdvanceduiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WilAdvanceduiDicePrefab.name = $"Wil Advanced Dice _({i + 1})";
            WilAdvanceduiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilAdvancedDice_UI;
            WilAdvanceduiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }

        for (int i = 0; i < WilNormalDiceAmount; i++)
        {
            GameObject WilNormaluiDicePrefab = Instantiate(uiDicePrefab);
            WilNormaluiDicePrefab.transform.SetParent(WilDiceGroup, false);
            WilNormaluiDicePrefab.name = $"Wil Normal Dice _({i + 1})";
            WilNormaluiDicePrefab.GetComponent<UIDice>().diceData = playerDiceData.WilNormalDice_UI;
            WilNormaluiDicePrefab.GetComponent<UIDice>().SetDiceData();
        }
    }

    private void TestGetDice()
    {
        playerDiceData.StrNormalDice.AddValue(1);
        playerDiceData.DexAdvancedDice.AddValue(1);
        playerDiceData.WilNormalDice.AddValue(1);
    }
}

