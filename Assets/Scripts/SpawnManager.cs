using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TowerDefenceCommons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance => _instance ??= new SpawnManager();

    [SerializeField] private InGameTimer _inGameTimer;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<Transform> _waypointListPlayer;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private int _enemyCountPerWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private E_INGAME_STATE _inGameState;
    private Dictionary<E_PLYAER_TYPE, List<Transform>> _rootPointDicToPlayerType;
    private int[][] _summonPossibleCoods;

    void Start()
    {
        _inGameState = E_INGAME_STATE.INIT;
        _enemyCountPerWave = 50;

        _rootPointDicToPlayerType = new Dictionary<E_PLYAER_TYPE, List<Transform>>();

        var targetPlayerType = E_PLYAER_TYPE.NONE;
        foreach (var wayPointList in _waypointListPlayer)
        {
            if (targetPlayerType == E_PLYAER_TYPE.NONE)
            {
                targetPlayerType = E_PLYAER_TYPE.PLAYER_A;
            }
            else if (targetPlayerType == E_PLYAER_TYPE.PLAYER_A)
            {
                targetPlayerType = E_PLYAER_TYPE.PLAYER_B;
            }

            _rootPointDicToPlayerType.Add(targetPlayerType, new List<Transform>());
            foreach (Transform point in wayPointList)
            {
                _rootPointDicToPlayerType[targetPlayerType].Add(point);
            }
        }

        _inGameTimer.StartTimer(2f, StartGame);

        InitSummonPossibleArray();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void StartGame()
    {
        _inGameState = E_INGAME_STATE.PLAY;

        StartCoroutine(SpawnEnemyRoutine());
    }

    private void InitSummonPossibleArray()
    {
        _summonPossibleCoods = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            _summonPossibleCoods[i] = new int[4];
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        for (int i = 0; i < _enemyCountPerWave; i++)
        {
            var enemyObjectA = Instantiate(_enemyPrefab, _rootPointDicToPlayerType[E_PLYAER_TYPE.PLAYER_A][0].position, Quaternion.identity);
            var enemyObjectB = Instantiate(_enemyPrefab, _rootPointDicToPlayerType[E_PLYAER_TYPE.PLAYER_B][0].position, Quaternion.identity);

            var moveControllerA = enemyObjectA.GetComponent<EnemyMoveController>();
            var moveControllerB = enemyObjectB.GetComponent<EnemyMoveController>();

            moveControllerA.SetRoute(_rootPointDicToPlayerType[E_PLYAER_TYPE.PLAYER_A]);
            moveControllerB.SetRoute(_rootPointDicToPlayerType[E_PLYAER_TYPE.PLAYER_B]);

            _enemyCounter.IncreaseEnemyCount(2);

            yield return new WaitForSeconds(0.4f);
        }
    }

}
