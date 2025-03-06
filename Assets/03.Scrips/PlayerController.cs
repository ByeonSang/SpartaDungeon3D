using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private Player_AC _inputAC;
    private CharacterController _charCtrl;
    private Animator _anim;

    public Vector2 MoveInput { get; set; }

    public StateMachine stateMachine;
    public PlayerIdleState IdleState { get; set; }
    public PlayerMoveState MoveState { get; set; }
    public PlayerJumpState JumpState { get; set; }


    private void Awake()
    {
        _inputAC = new Player_AC();
        _anim = GetComponentInChildren<Animator>();
        TryGetComponent<Player>(out _player);

        _inputAC.Player.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        _inputAC.Player.Move.canceled += context => MoveInput = Vector2.zero;
        _inputAC.Player.Jump.started += context => _player.Jump();

        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, _player, stateMachine, _anim,"Idle");
        MoveState = new PlayerMoveState(this, _player, stateMachine, _anim, "Move");
        JumpState = new PlayerJumpState(this, _player, stateMachine, _anim, "Jump");
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.Update();
        _player.Move(MoveInput);
    }

    private void OnEnable()
    {
        _inputAC.Enable();
    }

    private void OnDisable()
    {
        _inputAC.Disable();
    }
}
