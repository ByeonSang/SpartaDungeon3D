using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private InteractionUI _interUI;

    [SerializeField] private LayerMask InteractionLayerMask;
    private Collider _curCollider;
    private float _minDist;

    private void Start()
    {
        _minDist = float.MaxValue;
    }

    public Collider CheckField()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, InteractionLayerMask);
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
                    _interUI.SetText(item.GetComponent<FieldItem>().Data);
                    _interUI.StartAnimation();
                }
            }
        }
        else if(length == 0 && _curCollider != null)
        {
            _curCollider = null;
            _minDist = float.MaxValue;
            _interUI.StopAnimation();
        }

        return _curCollider;
    }
}
