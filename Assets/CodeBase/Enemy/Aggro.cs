using System.Collections;
using UnityEngine;
public class Aggro : MonoBehaviour
{
    public TriggerObserver TriggerObserver;
    public Follow Follow;
    public float Cooldown;
    private Coroutine _aggroCoroutine;
    private bool _hasAggroTarget;

    private void Start()
    {
        TriggerObserver.TriggerEnter += TriggerEnter;
        TriggerObserver.TriggerExit += TriggerExit;
        SwitchFollowOff();
    }

    private void TriggerEnter(Collider obj)
    {
        if (_hasAggroTarget) return;
        _hasAggroTarget = true;
        StopAggroCoroutine();
        SwitchFollowOn();
    }

    private void TriggerExit(Collider obj)
    {
        if (!_hasAggroTarget) return;
        _hasAggroTarget = false;
        SwitchFollowOff();
        _aggroCoroutine = StartCoroutine(SwitchFollowAfterCooldown());
    }

    private void SwitchFollowOn() =>
        Follow.enabled = true;

    private void SwitchFollowOff() =>
        Follow.enabled = false;

    private IEnumerator SwitchFollowAfterCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        SwitchFollowOff();
    }

    private void StopAggroCoroutine()
    {
        if (_aggroCoroutine == null) return;
        StopCoroutine(_aggroCoroutine);
        _aggroCoroutine = null;
    }
}