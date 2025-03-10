using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger()
    {
        _player.AnimationTrigger();
    }
}
