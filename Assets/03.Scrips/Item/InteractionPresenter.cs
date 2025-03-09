using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPresenter : MonoBehaviour
{
    private Interaction _interaction;
    public InteractionUI interactionUI;

    private void Start()
    {
        if(TryGetComponent<Interaction>(out _interaction))
        {
            _interaction.OnDectedInteraction = UpdateUI;
            _interaction.OnCancelInteraction = CancelUI;
        }
    }

    private void OnDestroy()
    {
        _interaction.OnDectedInteraction = null;
        _interaction.OnCancelInteraction = null;
    }

    private void UpdateUI(ItemData data)
    {
        interactionUI.SetText(data);
    }

    private void CancelUI()
    {
        interactionUI.StopAnimation();
    }
}
