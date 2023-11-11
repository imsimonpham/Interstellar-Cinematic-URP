using UnityEngine;
using UnityEngine.UI;

// ATTEMPT TO MAKE A OVERWATCH-LIKE UI

public class Draft : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    private float  _maxHP;
    [SerializeField] private Enemy _enemy;
    private int _maxHPCount = 20;
    private int lastActiveindex;
    private float hpReducedPct;
    private float maxHealthPerPoint;
    private float carriedOverhpReducedPct;
    [SerializeField] private Bullet _bullet;
    
    
}

 