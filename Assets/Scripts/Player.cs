using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
   [SerializeField] int _maxHealth = 3;
    [SerializeField] int _treasureCount = 0;
    int _currentHealth;
    int _currentTreasure;
    TankController _tankController;

    [SerializeField] private Material _invincibilityMaterial = null;
    [SerializeField] private List<MeshRenderer> _materialsToChangeWhenInvincible = new List<MeshRenderer>();
    private List<Material> _regularMaterial;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentTreasure = _treasureCount;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void AddTreasure(int value)
    {
        _currentTreasure += value;
        Debug.Log("Treasure: " + _currentTreasure);
    }

    public void OnSetInvincible()
    {
        if (_tankController == null) return;
        _tankController.Invincible = true;
        _regularMaterial = new List<Material>(_materialsToChangeWhenInvincible.Count);
        foreach (var meshRenderer in _materialsToChangeWhenInvincible)
        {
            _regularMaterial.Add(meshRenderer.material);
            meshRenderer.material = _invincibilityMaterial;
        }
    }

    public void OnRemoveInvincible()
    {
        if (_tankController == null) return;
        _tankController.Invincible = false;
        for (int i = 0; i < _materialsToChangeWhenInvincible.Count; ++i)
        {
            _materialsToChangeWhenInvincible[i].material = _regularMaterial[i];
        }
        _regularMaterial.Clear();
    }
    public void Kill()
    {
        gameObject.SetActive(false);
        //Play particles
        //play sounds
    }
}
