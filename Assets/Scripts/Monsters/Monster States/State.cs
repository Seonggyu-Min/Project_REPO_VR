using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected BaseMonster _baseMonster;

    protected LayerMask _doorLayer = 1 << 15;
    protected float _detectDoorRange = 1f;
    protected float _doorCheckCooldown = 1f;
    protected float _lastDoorCheckTime = float.MinValue;

    protected float _forceSearchTimer = 0f;
    protected float _forceSearchCooldown = 2f;


    public State(BaseMonster baseMonster)
    {
        _baseMonster = baseMonster;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    protected virtual void TryOpenNearbyDoor()
    {
        if (Time.time - _lastDoorCheckTime < _doorCheckCooldown) return;
        _lastDoorCheckTime = Time.time;

        Collider[] doors = Physics.OverlapSphere(_baseMonster.transform.position, _detectDoorRange, _doorLayer);

        foreach (Collider doorCollider in doors)
        {
            Door door = doorCollider.GetComponent<Door>();
            if (door != null)
            {
                door.Open();
            }
        }
    }
}
