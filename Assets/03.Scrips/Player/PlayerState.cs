using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerState
{
    protected PlayerController playerCtrl;
    protected Player player;
    protected StateMachine stateMachine;
    protected Animator anim;
    protected string animName;

    public PlayerState(PlayerController _playerCtrl, Player _player, StateMachine _stateMachine, Animator _anim, string _animName)
    {
        playerCtrl = _playerCtrl;
        player = _player;
        stateMachine = _stateMachine;
        anim = _anim;
        animName = _animName;
    }

    public virtual void Enter()
    {
        anim.SetBool(animName, true);
    }

    public virtual void Update()
    {
        if (GameManager.Instance.player.IsDead) return;
        player.Move(playerCtrl.MoveInput);
        player.SetGravity();
        player.SetDirection(playerCtrl.MousePos);
    }

    public virtual void Exit()
    {
        anim.SetBool(animName, false);
    }
}
