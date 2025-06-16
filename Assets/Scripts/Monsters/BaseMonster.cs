using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseMonster : MonoBehaviour
{
    protected Animator _animator;

    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }
    [SerializeField] protected Transform player;

    [SerializeField] protected Player _player;
    public Transform Player { get { return player; } }

    protected State _currentState;
    public State CurrentState { get { return _currentState; } }
    protected StateMachine _stateMachine;
    public StateMachine StateMachine { get { return _stateMachine; } }

    [SerializeField] protected Transform[] patrolPoints;
    public Transform[] PatrolPoints { get { return patrolPoints; } }



    [SerializeField] protected float _patrolMoveSpeed;
    public float PatrolMoveSpeed { get { return _patrolMoveSpeed; }}

    [SerializeField] protected float _traceMoveSpeed;
    public float TraceMoveSpeed { get { return _traceMoveSpeed; }}

    [SerializeField] protected LayerMask _playerLayer;

    protected bool _isCollided;



    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine();

        _isCollided = false;
    }

    protected virtual void Start()
    {
        _stateMachine.ChangeState(new PatrolState(this, patrolPoints));
    }

    protected virtual void Update()
    {
        _stateMachine.Update();

        if (_isCollided)
        {
            _player.Die();
        }
    }

    public virtual void PlayAnimation(string stateName)
    {
        _animator?.Play(stateName);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) == 0) return;

        _isCollided = true;

        _stateMachine.ChangeState(new AttackState(this));
    }
}
