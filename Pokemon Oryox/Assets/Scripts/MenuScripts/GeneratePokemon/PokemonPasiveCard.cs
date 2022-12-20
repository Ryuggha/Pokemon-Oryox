using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokemonPasiveCard : MonoBehaviour
{
    private MoveData moveData;
    private GeneratePasives controller;

    public Image buttonSprite;
    public TMP_Text nameField;

    [SerializeField] Sprite normalColor;
    [SerializeField] Sprite selectedColor;
    [SerializeField] Sprite showingColor;

    public void Initialize(MoveData moveData, GeneratePasives controller)
    {
        this.moveData = moveData;
        this.controller = controller;

        nameField.text = moveData.moveName;
    }

    public void setSelected(bool selected, bool showing)
    {
        if (selected)
        {
            if (showing) buttonSprite.sprite = showingColor;
            else buttonSprite.sprite = selectedColor;
        } 
        else buttonSprite.sprite = normalColor;
    }

    public void setSelected(bool selected)
    {
        setSelected(selected, false);
    }

    public void onClick()
    {
        controller.pasiveSelected(this);
    }

    public MoveData getMoveData ()
    {
        return moveData;
    }
}
