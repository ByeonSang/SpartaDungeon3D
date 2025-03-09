using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Interaction : MonoBehaviour
{
    public Action<ItemData> OnDectedInteraction;
    public Action OnCancelInteraction;

    [SerializeField] private LayerMask InteractionLayerMask;
    [SerializeField] private Transform _checkInteractionTrans;
    private Collider _curCollider;
    private float _minDist;

    private Player _player;

    private void Start()
    {
        _minDist = float.MaxValue;
        TryGetComponent<Player>(out _player);
    }

    public Collider CheckField()
    {
        Collider[] colliders = Physics.OverlapSphere(_checkInteractionTrans.position, 1f, InteractionLayerMask);
        int length = colliders.Length;
        if (length > 0)
        {
            foreach (var item in colliders)
            {
                float dist = Vector3.Distance(transform.position, item.transform.position);
                if (dist < _minDist && item != _curCollider)
                {
                    _minDist = dist;
                    _curCollider = item;
                    OnDectedInteraction?.Invoke(item.GetComponent<FieldItem>().Data);
                }
            }
        }
        else if(_curCollider != null)
        {
            _curCollider = null;
            _minDist = float.MaxValue;
            OnCancelInteraction?.Invoke();
        }

        return _curCollider;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_checkInteractionTrans.position, 1f);
    }
}
