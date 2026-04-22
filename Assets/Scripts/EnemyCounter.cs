using System;
using TMPro;
using TowerDefenceCommons;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    private int _currentEnemyCount;

    private int _gameOverEnemyCount;
    private int _lastDisplayEnemyCount;
    private void Start()
    {
        _lastDisplayEnemyCount = -1;
        _gameOverEnemyCount = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastDisplayEnemyCount != _currentEnemyCount)
        {
            _lastDisplayEnemyCount = _currentEnemyCount;
            _enemyCountText.text = $"{_currentEnemyCount} / {_gameOverEnemyCount}";
        }
    }

    public void IncreaseEnemyCount(int count = 1)
    {
        _currentEnemyCount += count;

        _enemyCountText.text = $"{_currentEnemyCount} / {_gameOverEnemyCount}";
    }

    public void ReduceEnemyCount(int count = 1)
    {
        _currentEnemyCount -= count;

        _enemyCountText.text = $"{_currentEnemyCount} / {_gameOverEnemyCount}";
    }



}
