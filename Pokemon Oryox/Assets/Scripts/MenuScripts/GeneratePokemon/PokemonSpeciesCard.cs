using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSpeciesCard : MonoBehaviour
{
    private PokemonApiData pokeData;
    private SpeciesSelectViewPort controller;

    public Image buttonSprite;
    public Image pokemonSprite;

    [SerializeField] Sprite PinkSprite;
    [SerializeField] Sprite OrangeSprite;

    public void Initialize(PokemonApiData pokeData, SpeciesSelectViewPort controller)
    {
        this.controller = controller;

        this.pokeData = pokeData;

        pokemonSprite.sprite = pokeData.sprite;
    }

    public void onClick()
    {
        controller.selectCard(this);
        buttonSprite.sprite = OrangeSprite;
    }

    public void unselect()
    {
        buttonSprite.sprite = PinkSprite;
    }

    public PokemonApiData getPokeData ()
    {
        return pokeData;
    }
}
