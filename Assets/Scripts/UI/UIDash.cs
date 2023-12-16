using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using UnityEngine.UI;
using System;

public class UIDash : MonoBehaviour
{
    [SerializeField]
    private BoolVariable _canDash;
    [SerializeField]
    private BoolVariable _isDashing;
    [SerializeField]
    private FloatVariable _dashCooldown;
    [SerializeField]
    private Slider _dashSlider;

    private void OnEnable()
    {
        _canDash.AddListener(HandleCanDashChange);
        _isDashing.AddListener(HandleIsDashingChange);
        _canDash.BaseValue = true;
        _canDash.Raise();

        HandleCanDashChange(_canDash);
    }

    private void OnDisable()
    {
        _canDash.RemoveListener(HandleCanDashChange);
        _isDashing.RemoveListener(HandleIsDashingChange);
    }

    private void HandleCanDashChange(bool canDash)
    {
        if (canDash)
        {
            _dashSlider.value = 1f;
        }
        else
        {
            _dashSlider.value = 0f;
        }
    }

    private void HandleIsDashingChange(bool isDashing)
    {
        if (isDashing)
        {
            // do nothing
            return;
        }

        if (_canDash)
        {
            // something went wrong but we are not in a cooldown right now
            return;
        }

        // dashing has finished, cooldown begins
        StartCoroutine(AnimateCooldown(_dashCooldown));

    }

    private IEnumerator AnimateCooldown(float timeToAnimate)
    {
        float timeSinceStart = 0f;

        while (timeSinceStart < timeToAnimate)
        {
            float progress = Mathf.Clamp01(timeSinceStart / timeToAnimate);
            _dashSlider.value = progress;
            yield return new WaitForEndOfFrame();
            timeSinceStart += Time.deltaTime;
        }
    }
}
