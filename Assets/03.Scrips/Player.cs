using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    public CharacterController CharCtrl { get; set; }
    private float _curJumpPower;

    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityMultipl;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    public bool IsGround { get; set; }

    private void Awake()
    {
        CharCtrl = GetComponent<CharacterController>();
        Debug.LogWarning($"Failed GetComponent CharacterComponent from {this.name} gameObject");
    }

    public void Jump()
    {
        if (IsGround) _curJumpPower = _jumpPower;
    }

    public void Move(Vector2 inputValue)
    {
        _gravityMultipl = Mathf.Clamp(_gravityMultipl, 0, 1); // 배율 0 부터 1 사이
        Vector3 moveDir = Vector3.right * inputValue.x + Vector3.forward * inputValue.y;
        _curJumpPower -= _gravity * Time.deltaTime;
        _curJumpPower = Mathf.Clamp(_curJumpPower, -1 * _gravity * _gravityMultipl, _jumpPower);

        IsGround = (CharCtrl.Move((moveDir + Vector3.up * _curJumpPower) * _speed * Time.deltaTime) & CollisionFlags.Below) != 0;
    }
}
