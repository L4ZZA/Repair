using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image image;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteRepair;
    [SerializeField] private Sprite spriteDamage;


    public void WeaponRepairSelected()
    {
        ChangeImage(image, spriteRepair);
    }


    public void WeaponDamageSelected()
    {
        ChangeImage(image, spriteDamage);
    }


    private void ChangeImage(Image _image, Sprite _sprite)
    {
        if (image != null)
        {
            if (_sprite != null)

            _image.sprite = _sprite;
        }
    }

}
