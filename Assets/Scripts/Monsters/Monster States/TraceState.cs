using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TraceState : State
{
    public TraceState(BaseMonster baseMonster) : base(baseMonster) { }

    public override void Enter()
    {
        _baseMonster.Agent.isStopped = false;
        _baseMonster.Agent.speed = _baseMonster.TraceMoveSpeed;
        _forceSearchTimer = 0f;
        // 家府 犁积
    }

    public override void Execute()
    {
        if (_baseMonster.Agent.pathPending) return;

        if (_baseMonster.Agent.remainingDistance > _baseMonster.Agent.stoppingDistance &&
            _baseMonster.Agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            TryOpenNearbyDoor();

            _forceSearchTimer -= Time.deltaTime;

            if (_forceSearchTimer <= 0f)
            {
                _baseMonster.Agent.SetDestination(_baseMonster.Player.position);
                _forceSearchTimer = _forceSearchCooldown;
            }

            return;
        }

        _baseMonster.Agent.SetDestination(_baseMonster.Player.position);
    }

    public override void Exit()
    {
        _baseMonster.Agent.isStopped = true;
        // 家府 吝窜
    }
}
