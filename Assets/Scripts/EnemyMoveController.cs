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
    [SerializeField] private Transform _root;

    private Transform[] _waypoints;
    private int _targetIndex = 1;
    private float _distanceToNextWaypoint;
    private Vector3 _startPosition;
    void Awake()
    {
        _moveSpeed = 2f; // Set a default move speed


    }

    void Start()
    {
        _waypoints = _root.GetComponentsInChildren<Transform>(false);

        // 시작 위치로 이동
        transform.position = _waypoints[_targetIndex].position;
        _startPosition = transform.position;
        _distanceToNextWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _distanceToNextWaypoint += Time.deltaTime;
        transform.position = Vector3.Lerp(_startPosition, _waypoints[_targetIndex + 1].position, _distanceToNextWaypoint);
        //Vector3 direction = (_waypoints[_targetIndex + 1].position - transform.position).normalized;

        //transform.position += direction * _moveSpeed * Time.deltaTime;

        if (Vector3.Distance(_waypoints[_targetIndex + 1].position, transform.position) < 0.5f)
        {
            _targetIndex++;
            _startPosition = transform.position;
            _distanceToNextWaypoint = 0;
            if (_targetIndex >= _waypoints.Length - 1)
            {
                _targetIndex = 1;
            }
        }
    }

}
