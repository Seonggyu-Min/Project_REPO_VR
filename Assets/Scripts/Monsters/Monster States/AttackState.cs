using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(BaseMonster baseMonster) : base(baseMonster) { }

    public override void Enter()
    {
        _baseMonster.Agent.isStopped = true;
        // 소리 or 애니메이션 및 시네머신 조절
    }
    public override void Execute()
    {
        // 소리 추가

    }
    public override void Exit()
    {
    }
}
