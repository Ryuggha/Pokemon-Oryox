using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonConstitutionCard : MonoBehaviour
{
    private ConstitutionData constitutionData;
    private GenerateConstitution controller;

    public Image buttonSprite;
    public TMP_Text nameField;

    [SerializeField] Sprite normalColor;
    [SerializeField] Sprite selectedColor;

    public void Initialize(ConstitutionData constitutionData, GenerateConstitution controller)
    {
        this.constitutionData = constitutionData;
        this.controller = controller;

        nameField.text = constitutionData.constitutionName;
    }

    public void setSelected(bool selected)
    {
        if (selected)
        {
            buttonSprite.sprite = selectedColor;
        }
        else buttonSprite.sprite = normalColor;
    }

    public void onClick()
    {
        controller.constitutionSelected(this);
    }

    public ConstitutionData getConstitutionData()
    {
        return constitutionData;
    }
}
