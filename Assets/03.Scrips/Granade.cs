using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] private float _forcePower;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _explosionPower;
    [SerializeField] private float _explosionScope;
    private Rigidbody _rigid;

    public void Fire(Vector3 direction)
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.AddForce(direction * _forcePower, ForceMode.Impulse);
        Destroy(gameObject, 4f);
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(go, 1f);
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);

        foreach(var col in colls)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Cube"))
                col.GetComponent<Rigidbody>().AddExplosionForce(_explosionPower, transform.position, _explosionScope, _explosionScope, ForceMode.Impulse);
            else if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                GameManager.Instance.player.TakeDamage(50f);
            }
        }
    }
}
