using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    // 각각의 다른 점프대가 있을 수 있으니 따로 분리를 시켜
    // 점프 파워를 다르게 줄 수 있도록 하였습니다.

    [SerializeField] private float _jumpForce;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask whatIsPlayer;

    private BoxCollider _collider;

    private void Start()
    {
        _collider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        if(Physics.BoxCast(transform.position,_collider.size, Vector3.up, Quaternion.identity, 0.5f,whatIsPlayer))
            _player.Jump(_jumpForce);
    }
}
