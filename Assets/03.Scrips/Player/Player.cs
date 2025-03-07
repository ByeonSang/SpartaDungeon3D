using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    public CharacterController CharCtrl { get; set; }
    private Camera cam;


    [Header("Gravity")]
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityMultipl;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    private float _curJumpPower;

    [Header("TargetAim")]
    [SerializeField] private LayerMask aimLayerMask;

    [Space]
    [SerializeField] private LayerMask itemLayerMask;
    private Collider fieldCollider;

    public bool IsGround { get; set; }

    private void Awake()
    {
        cam = Camera.main;
        CharCtrl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckFieldItem();
    }

    public void Jump()
    {
        if (IsGround) _curJumpPower = _jumpPower;
    }

    public void Move(Vector2 inputValue)
    {
        _gravityMultipl = Mathf.Clamp(_gravityMultipl, 0, 1); // ���� 0 ���� 1 ����
        Vector3 moveDir = Vector3.right * inputValue.x + Vector3.forward * inputValue.y;
        _curJumpPower -= _gravity * Time.deltaTime;
        _curJumpPower = Mathf.Clamp(_curJumpPower, -1 * _gravity * _gravityMultipl, _jumpPower);

        IsGround = (CharCtrl.Move((moveDir + Vector3.up * _curJumpPower) * _speed * Time.deltaTime) & CollisionFlags.Below) != 0;
    }

    public void SetTargetPos(Vector3 newPosition)
    {
        Ray ray = cam.ScreenPointToRay(newPosition);
        
        if(Physics.Raycast(ray, out var hitinfo, Mathf.Infinity, aimLayerMask))
        {
            GameObject model = GetComponentInChildren<Animator>().gameObject;
            Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPos = new Vector2(hitinfo.point.x, hitinfo.point.z);
            Vector2 dir = targetPos - playerPos;
            dir.Normalize();
            model.transform.forward = new Vector3(dir.x, 0, dir.y);
        }
    }

    private void CheckFieldItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, itemLayerMask);

        if(colliders.Length > 0)
        {
            float minDist = 100f;
            Collider collider = colliders[0];
            foreach (var item in colliders)
            {
                float dist = Vector3.Distance(transform.position, item.transform.position);
                if (dist  < minDist)
                {
                    minDist = dist;
                    collider = item;
                }
            }

            Debug.Log(collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
