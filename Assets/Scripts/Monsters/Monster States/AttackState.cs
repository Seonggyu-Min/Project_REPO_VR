using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(BaseMonster baseMonster) : base(baseMonster) { }

    public override void Enter()
    {
        _baseMonster.Agent.isStopped = true;
        // �Ҹ� or �ִϸ��̼� �� �ó׸ӽ� ����
    }
    public override void Execute()
    {
        // �Ҹ� �߰�

    }
    public override void Exit()
    {
    }
}
