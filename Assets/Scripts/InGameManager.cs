using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TowerDefenceCommons;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private InGameTimer _inGameTimer;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemyRoot;
    [SerializeField] private int _enemyCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private E_INGAME_STATE _inGameState;
    private List<Transform> _rootPointList;
    void Start()
    {
        _inGameState = E_INGAME_STATE.INIT;
        _enemyCount = 10;
        _rootPointList = new List<Transform>();
        foreach (Transform point in _enemyRoot)
        {
            _rootPointList.Add(point);
        }

        _inGameTimer.StartTimer(2f, StartGame);
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

    private IEnumerator SpawnEnemyRoutine()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            var enemyObject = Instantiate(_enemyPrefab, _rootPointList[0].position, Quaternion.identity);

            var moveController = enemyObject.GetComponent<EnemyMoveController>();

            moveController.SetRoute(_rootPointList);

            yield return new WaitForSeconds(0.4f);
        }
    }

}
