using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField] private float _powerupDuration = 5;
    [SerializeField] private GameObject _art = null;
    [SerializeField] private ParticleSystem _constantParticles = null;
    [SerializeField] private ParticleSystem _collectParticles = null;
    [SerializeField] private AudioClip _powerUpSfx;
    [SerializeField] private AudioClip _powerDownSfx;
    protected float Duration => _powerupDuration;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        // Ensure collect particles don't play on awake or self destruct
        if (_collectParticles != null && _collectParticles.gameObject.activeInHierarchy)
        {
            _collectParticles.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnCollect(other.gameObject))
        {
            StartCoroutine(PowerupCoroutine());
        }
    }

    protected abstract bool OnCollect(GameObject other);

    private IEnumerator PowerupCoroutine()
    {
        HideObject();
        ActivatePowerup();
        ActivationFeedback();
        yield return new WaitForSecondsRealtime(_powerupDuration);
        DeactivatePowerup();
        DeactivationFeedback();
        DisableObject();
    }

    protected abstract void ActivatePowerup();
    protected abstract void DeactivatePowerup();

    protected virtual void HideObject()
    {
        _collider.enabled = false;
        if (_art != null) _art.SetActive(false);
        if (_constantParticles != null)
        {
            ParticleSystem.EmissionModule emission = _constantParticles.emission;
            emission.rateOverTime = 0;
        }
    }

    protected virtual void ActivationFeedback()
    {
        if (_collectParticles != null)
        {
            Instantiate(_collectParticles, transform.position, Quaternion.identity).gameObject.SetActive(true);
        }
        AudioSource.PlayClipAtPoint(_powerUpSfx,transform.position);
    }

    protected virtual void DeactivationFeedback()
    {
        AudioSource.PlayClipAtPoint(_powerDownSfx, transform.position);
    }

    protected virtual void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public bool OnDamageVolume(int damage)
    {
        Destroy(gameObject);
        return true;
    }
}
