using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    // ������ �ٸ� �����밡 ���� �� ������ ���� �и��� ����
    // ���� �Ŀ��� �ٸ��� �� �� �ֵ��� �Ͽ����ϴ�.

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
