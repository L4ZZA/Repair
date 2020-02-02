using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeartIconController : MonoBehaviour
{
    [Header("Cross")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteDamage;
    [SerializeField] private Sprite spriteRepair;

    [Header("Slider")]
    [SerializeField] private Slider slider;


    private void Awake()
    {
        ChangeSlider(slider, 100);
    }


    public void ChangeIconToDamage()
    {
        ChangeSprite(spriteRenderer, spriteDamage);
    }


    public void ChangeIconToRepair()
    {
        ChangeSprite(spriteRenderer, spriteRepair);
    }


    public void ChangeHealthSlider(int _health, int _maxHealth)
    {
        Debug.Log("Health: " + _health);

        float health = (float) _health;
        float maxHealth = (float)_maxHealth;

        float sliderValue = (health / maxHealth) * 100;

        Debug.Log("Value: " + sliderValue);

        ChangeSlider(slider, Convert.ToInt32(sliderValue));

        
    }


    private void ChangeSlider(Slider _slider, int _value)
    {
        if (slider != null)
        {
            if (_value >= 0)
            {
                slider.value = _value;
            }
        }

        else Debug.LogWarning(this + "slider not assigned. Can't change.");
    }


    private void ChangeSprite(SpriteRenderer _spriteRenderer, Sprite _sprite)
    {
        if (_spriteRenderer != null)
        {
            if (_sprite != null)
            {
                _spriteRenderer.sprite = _sprite;
            }

            else Debug.LogWarning(this + "sprite not assigned. Can't change.");

        }

        else Debug.LogWarning(this + "spriteRenderer not assigned. Can't change.");
    }
}
