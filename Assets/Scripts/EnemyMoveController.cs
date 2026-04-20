using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float _moveSpeed; // Speed at which the enemy moves

    private List<Transform> _waypointList;
    private int _targetIndex = 0;
    private float _distanceToNextWaypoint;
    private Vector3 _startPosition;

    void Awake()
    {
        _moveSpeed = 1f; // Set a default move speed
    }

    void Start()
    {
        // 시작 위치로 이동
        transform.position = _waypointList[_targetIndex].position;
        _startPosition = transform.position;
        _distanceToNextWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_waypointList == null)
        {
            return;
        }

        float step = _moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(
            transform.position,              // 현재 위치
            _waypointList[_targetIndex + 1].position, // 목표 위치
            step                             // 이번 프레임에 이동할 최대 거리
        );

        if (Vector3.Distance(_waypointList[_targetIndex + 1].position, transform.position) <= 0.1f)
        {
            _targetIndex++;
        }

        if (_targetIndex == _waypointList.Count - 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetRoute(List<Transform> routePointList)
    {
        _waypointList = routePointList;
    }

}
