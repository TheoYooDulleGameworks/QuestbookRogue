using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatusSO", menuName = "Scriptable Objects/WorldAssets/EnemyStatusSO")]
public class EnemyStatusSO : ScriptableObject
{
    public EnemyStatusValue Lv;
    public EnemyStatusValue maxHealth;
    public EnemyStatusValue currentHealth;
    public EnemyStatusValue maxArmor;
    public EnemyStatusValue currentArmor;
    public EnemyStatusValue maxDamage;
    public EnemyStatusValue currentDamage;
}

[System.Serializable]
public class EnemyStatusValue
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