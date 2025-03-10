using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private void Start()
    {
        UIManager.Instance.Hpbar = SetBar;
    }

    private void OnDestroy()
    {
        UIManager uiManager = UIManager.Instance;
        if (uiManager != null)
            uiManager.Hpbar = null;
    }

    private void SetBar()
    {
        Player player = GameManager.Instance.player;

        _fill.fillAmount = player.curHealth / player.maxHealth;
    }
}
