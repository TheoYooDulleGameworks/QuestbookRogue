using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusSO", menuName = "Scriptable Objects/PlayerAssets/PlayerStatusSO")]
public class PlayerStatusSO : ScriptableObject
{
    public StatusValue Lv;
    public StatusValue currentXp;
    public StatusValue currentHp;
    public StatusValue maxHp;
    public StatusValue currentSp;
    public StatusValue maxSp;

    public StatusValue Coin;
    public StatusValue Provision;
}

[System.Serializable]
public class StatusValue
{
    public event Action OnValueChanged;

    [SerializeField] private int value;

    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            OnValueChanged?.Invoke();
        }
    }

    public void AddValue(int amount)
    {
        Value += amount;
    }

    public void RemoveValue(int amount)
    {
        Value -= amount;
    }
}