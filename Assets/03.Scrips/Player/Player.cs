using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Interaction _interaction;
    public CharacterController CharCtrl { get; set; }
    private Camera cam;
    public Animator Anim { get; private set; }

    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _weaponAttach;
    [SerializeField] private GameObject _granade;

    [Header("StatInfo")]
    private float _curHealth;
    public float _maxHealth;
    public bool IsDead { get; private set; }

    [Header("Gravity")]
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityMultipl;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    private float _curJumpPower;

    [Header("TargetAim")]
    [SerializeField] private LayerMask aimLayerMask;

    public bool HasGranade { get; set; }


    private Collider _curDetected { get; set; }

    public bool IsGround { get; private set; }

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        CharCtrl = GetComponent<CharacterController>();
    }

    private void Start()
    {
        GameManager.Instance.player = this;
        TryGetComponent<Interaction>(out _interaction);
        _weaponAttach.SetActive(false);

        _curHealth = _maxHealth;
        IsDead = false;
    }

    private void Update()
    {
        _curDetected = _interaction.CheckField(); // 상호작용 처리
    }

    public void Jump(float JumpForce = 1f)
    {
        // TODO::
        if (IsGround) _curJumpPower = _jumpPower * JumpForce;
    }

    public void SetGravity()
    {
        IsGround = (CharCtrl.Move(Vector3.up * _curJumpPower * Time.deltaTime) & CollisionFlags.Below) != 0;

        if(!IsGround) _curJumpPower -= _gravity * Time.deltaTime;
    }

    public void Move(Vector2 inputValue)
    {
        Vector3 moveDir = Vector3.right * inputValue.x + Vector3.forward * inputValue.y;
        CharCtrl.Move(moveDir * _speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 newPosition)
    {
        Ray ray = cam.ScreenPointToRay(newPosition);
        
        if(Physics.Raycast(ray, out var hitinfo, Mathf.Infinity, aimLayerMask))
        {
            Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPos = new Vector2(hitinfo.point.x, hitinfo.point.z);
            Vector2 dir = targetPos - playerPos;
            dir.Normalize();
            _model.transform.forward = new Vector3(dir.x, 0, dir.y);
        }
    }

    public void OnInteraction()
    {
        if (_curDetected != null)
        {
            _curDetected.gameObject.SetActive(false);
            _weaponAttach.SetActive(true);
            HasGranade = true;
        }
    }

    public void Fire()
    {
        if (!HasGranade) return;

        _weaponAttach.SetActive(false);
        GameObject obj = Instantiate(_granade,transform.position + Vector3.up, Quaternion.identity);
        Granade gr = obj.GetComponent<Granade>();
        gr.Fire(_model.transform.forward);
        HasGranade = false;
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        if(_curHealth <= 0)
        {
            _curHealth = 0;
            IsDead = true;
            Anim.SetTrigger("Die");
        }
        else if(_curHealth > _maxHealth)
        {
            _curHealth = _maxHealth;
        }
        else
        {
            Anim.SetTrigger("Hit");
        }
    }
}
