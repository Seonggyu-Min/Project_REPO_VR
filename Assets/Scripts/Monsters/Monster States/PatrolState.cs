using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private Transform[] _patrolPoints;
    private int _currentIndex;

    public PatrolState(BaseMonster baseMonster, Transform[] patrolPoints) : base(baseMonster)
    {
        _currentIndex = 0;
        _patrolPoints = patrolPoints;
        // 家府 犁积
    }

    public override void Enter()
    {
        _baseMonster.Agent.isStopped = false;
        _baseMonster.Agent.speed = _baseMonster.PatrolMoveSpeed;
        _baseMonster.Agent.SetDestination(_patrolPoints[_currentIndex].position);
        _forceSearchTimer = 0f;
    }

    public override void Execute()
    {
        if (_baseMonster.Agent.pathPending)
            return;

        if (_baseMonster.Agent.remainingDistance > _baseMonster.Agent.stoppingDistance &&
            _baseMonster.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            TryOpenNearbyDoor();

            _forceSearchTimer -= Time.deltaTime;

            if (_forceSearchTimer <= 0f)
            {
                _baseMonster.Agent.SetDestination(_baseMonster.PatrolPoints[_currentIndex].position);
                _forceSearchTimer = _forceSearchCooldown;
            }

            return;
        }

        if (_baseMonster.Agent.remainingDistance < 0.5f)
        {
            _currentIndex = (_currentIndex + 1) % _baseMonster.PatrolPoints.Length;
            _baseMonster.Agent.SetDestination(_baseMonster.PatrolPoints[_currentIndex].position);
        }
    }

    public override void Exit()
    {
        _baseMonster.Agent.isStopped = true;
        // 家府 吝窜
    }
}
