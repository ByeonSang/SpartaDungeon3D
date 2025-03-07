using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerController _playerCtrl, Player _player, StateMachine _stateMachine, Animator _anim, string _animName) : base(_playerCtrl, _player, _stateMachine, _anim, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (playerCtrl.MoveInput.magnitude != 0)
            stateMachine.ChangeState(playerCtrl.MoveState);
    }
}
