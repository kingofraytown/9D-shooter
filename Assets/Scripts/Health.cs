using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField]
    private GameInt _maxHp;
    private int _hp;

    public int MaxHp => _maxHp.value();

    public int Hp {
        get => _hp;
        private set
        {
            var isDamaged = value < _hp;
            _hp = Mathf.Clamp(value, 0, _maxHp.value());
            if (isDamaged)
            {
                Damaged?.Invoke(_hp);
            }
            else
            {
                Healed?.Invoke(_hp);
            }

            if(_hp <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    private void Awake()
    {
        _hp = _maxHp.value();
    }

    public void Damage(int amount) => Hp -= amount;

    public void Heal(int amount) => Hp += amount;

    public void HealFull() => Hp = _maxHp.value();

    public void Kill() => Hp = 0;

    public void Adjust(int value) => Hp = value;




}
