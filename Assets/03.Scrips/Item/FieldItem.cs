using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    public ItemData Data { get => itemData; }
}
