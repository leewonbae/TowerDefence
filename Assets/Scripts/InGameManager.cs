using TowerDefenceCommons;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private InGameTimer _inGameTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private E_INGAME_STATE _inGameState;

    void Start()
    {
        _inGameState = E_INGAME_STATE.INIT;

        _inGameTimer.StartTimer(5f, StartGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void StartGame()
    {
        _inGameState = E_INGAME_STATE.PLAY;
    }

}
