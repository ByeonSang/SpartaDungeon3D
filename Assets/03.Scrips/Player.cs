using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Player_AC _inputAC;

    private Vector2 _moveInput;
    private Vector2 _aimInput;

    [SerializeField] private float gravity;

    private void Awake()
    {
        _inputAC = new Player_AC();

        _inputAC.Player.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
        _inputAC.Player.Move.canceled += context => _moveInput = Vector2.zero;
    }

    private void Update()
    {
        // ม฿ทย
        transform.position -= Vector3.up * gravity;
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
