using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public GamePhase currentGamePhase;
    public event Action<GamePhase> OnGamePhaseChanged;

    [SerializeField] public StagePhase currentStagePhase;
    public event Action<StagePhase> OnStagePhaseChanged;

    private void Start()
    {
        UpdateGamePhase(GamePhase.None);
        UpdateStagePhase(StagePhase.None);
    }

    public void UpdateGamePhase(GamePhase newGamePhase)
    {
        currentGamePhase = newGamePhase;

        switch (newGamePhase)
        {
            case GamePhase.None:
                break;
            case GamePhase.Planning:
                break;
            case GamePhase.EventStaging:
                break;
            case GamePhase.CombatStaging:
                break;
            case GamePhase.Resolving:
                break;
            default:
                break;
        }

        OnGamePhaseChanged?.Invoke(newGamePhase);
        Debug.Log("Game Phase : " + newGamePhase);
    }

    public void UpdateStagePhase(StagePhase newStagePhase)
    {
        currentStagePhase = newStagePhase;

        switch (newStagePhase)
        {
            case StagePhase.None:
                break;
            case StagePhase.Beginning:
                break;
            case StagePhase.DiceUsing:
                break;
            case StagePhase.DiceHolding:
                break;
            case StagePhase.DiceWaiting:
                break;
            case StagePhase.Finishing:
                break;
            default:
                break;
        }

        OnStagePhaseChanged?.Invoke(newStagePhase);
        Debug.Log("Stage Phase : " + newStagePhase);
    }
}

public enum GamePhase
{
    None,
    Planning,
    EventStaging,
    CombatStaging,
    Resolving,
}

public enum StagePhase
{
    None,
    Beginning,
    DiceUsing,
    DiceHolding,
    DiceWaiting,
    Finishing,
}