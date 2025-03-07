using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(PlayerController _playerCtrl, Player _player, StateMachine _stateMachine, Animator _anim, string _animName) : base(_playerCtrl, _player, _stateMachine, _anim, _animName)
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
        anim.SetFloat("dirX", playerCtrl.MoveInput.x);
        anim.SetFloat("dirY", playerCtrl.MoveInput.y);

        if (playerCtrl.MoveInput.magnitude == 0)
            stateMachine.ChangeState(playerCtrl.IdleState);
    }
}
