using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusSO", menuName = "Scriptable Objects/PlayerAssets/PlayerStatusSO")]
public class PlayerStatusSO : ScriptableObject
{
    [SerializeField] public StatusValue Lv;
    [SerializeField] public StatusValue currentXp;
    [SerializeField] public StatusValue currentHp;
    [SerializeField] public StatusValue maxHp;
    [SerializeField] public StatusValue currentSp;
    [SerializeField] public StatusValue maxSp;

    [SerializeField] public StatusValue Coin;
    [SerializeField] public StatusValue Provision;
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