using System;
using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterEffect());
    }

    private IEnumerator DisableAfterEffect()
    {
        yield return new WaitUntil(() => !_particle.IsAlive(false));
        gameObject.SetActive(false);
    }
}
