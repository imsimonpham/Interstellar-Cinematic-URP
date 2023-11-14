using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _reduceSpeed;
    private float _target = 1f;
    
    void Update()
    {
        _healthBar.fillAmount = Mathf.MoveTowards(_healthBar.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        _target = currentHealth / maxHealth;
    }
}

 