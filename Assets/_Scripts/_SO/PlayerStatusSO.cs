using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusSO", menuName = "Scriptable Objects/PlayerAssets/PlayerStatusSO")]
public class PlayerStatusSO : ScriptableObject
{
    public StatusValue Lv;
    public StatusValue currentXp;
    public StatusValue currentHp;
    public StatusValue maxHp;
    public StatusValue currentArmor;

    public StatusValue Gold;
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

    public void SetClampedValue(int newValue, int min, int max)
    {
        value = Mathf.Clamp(newValue, min, max);
        OnValueChanged?.Invoke();
    }

    public void AddClampedValue(int amount, int min, int max)
    {
        SetClampedValue(value + amount, min, max);
    }

    public void RemoveClampedValue(int amount, int min, int max)
    {
        SetClampedValue(value - amount, min, max);
    }
}