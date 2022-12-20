using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonNatureCard : MonoBehaviour
{
    private NatureData natureData;
    private GenerateNatures controller;

    public Image buttonSprite;
    public TMP_Text nameField;

    [SerializeField] Sprite normalColor;
    [SerializeField] Sprite selectedColor;

    public void Initialize(NatureData natureData, GenerateNatures controller)
    {
        this.natureData = natureData;
        this.controller = controller;

        nameField.text = natureData.natureName;
    }

    public void setSelected (bool selected)
    {
        if (selected)
        {
            buttonSprite.sprite = selectedColor;
        }
        else buttonSprite.sprite = normalColor;
    }

    public void onClick()
    {
        controller.natureSelected(this);
    }

    public NatureData getNatureData ()
    {
        return natureData;
    }
}
