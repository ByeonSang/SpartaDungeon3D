using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private Player_AC _inputAC;

    public Vector2 MoveInput { get; set; }
    public Vector2 MousePos { get; set; }

    public StateMachine stateMachine;
    public PlayerIdleState IdleState { get; set; }
    public PlayerMoveState MoveState { get; set; }
    public PlayerJumpState JumpState { get; set; }


    private void Awake()
    {
        _inputAC = new Player_AC();
        TryGetComponent<Player>(out _player);

        _inputAC.Player.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        _inputAC.Player.Move.canceled += context => MoveInput = Vector2.zero;
        _inputAC.Player.Jump.started += context => _player.Jump();
        _inputAC.Player.Fire.started += context => _player.Fire();
        _inputAC.Player.Interaction.started += context => _player.OnInteraction();
        _inputAC.Player.MouseDelta.performed += context => MousePos = context.ReadValue<Vector2>();

        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, _player, stateMachine, _player.Anim,"Idle");
        MoveState = new PlayerMoveState(this, _player, stateMachine, _player.Anim, "Move");
        JumpState = new PlayerJumpState(this, _player, stateMachine, _player.Anim, "Jump");
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.CurrentState.Update(); // 局聪皋捞记 贸府
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
