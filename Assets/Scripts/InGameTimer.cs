using System;
using TMPro;
using TowerDefenceCommons;
using UnityEngine;

public class InGameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private bool _isTimerRunning;
    private Action _timerFinishedAction;
    private float _playTimerSeconds;
    private int _lastDisplaySeconds;

    public void StartTimer(float playTimerSeconds, Action timerFinishedAction)
    {
        _isTimerRunning = true;
        _playTimerSeconds = playTimerSeconds;
        _timerFinishedAction = timerFinishedAction;

        _timerText.text = $"Remain Time Until Start: {playTimerSeconds} s";
        _lastDisplaySeconds = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isTimerRunning)
        {
            return;
        }

        _playTimerSeconds -= Time.deltaTime;

        int displayTime = Mathf.CeilToInt(_playTimerSeconds);

        if (_lastDisplaySeconds != displayTime)
        {
            _lastDisplaySeconds = displayTime;
            
            _timerText.text = $"Remain Time Until Start: {_lastDisplaySeconds} s";
        }

        if (displayTime <= 0f && _isTimerRunning)
        {
            _isTimerRunning = false;
            _timerText.text = string.Empty;

            _timerFinishedAction.Invoke();
        }
    }

}
