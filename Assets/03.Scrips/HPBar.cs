using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance.player == null);
        
        GameManager.Instance.player.Hpbar = SetBar;
    }

    private void OnDestroy()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager != null)
            gameManager.player.Hpbar = null;
    }

    private void SetBar()
    {
        Player player = GameManager.Instance.player;

        _fill.fillAmount = player.curHealth / player.maxHealth;
    }
}
