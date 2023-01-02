using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpeciesChangeViewPort : MonoBehaviour, IViewPortController
{

    StatsMenu statsMenu;
    List<PokemonApiData> pokeData;

    [Header("Sections")]
    [SerializeField] GameObject selectObject;
    [SerializeField] GameObject selectTypes;

    [Header("Variables")]
    public GameObject content;
    public GameObject PrefabSpriteButton;
    [SerializeField] Button selectSpecies;
    [SerializeField] TMP_InputField pokemonRegexField;

    PokemonSpeciesCard selectedCard;

    PokemonObject pokemon;

    [SerializeField] TMP_Dropdown type1Dropdown;
    [SerializeField] TMP_Dropdown type2Dropdown;
    [SerializeField] Image pokemonSprite;
    [SerializeField] TMP_Text pokemonName;

    public void Initialize(PokemonObject pokemon)
    {
        if (statsMenu == null) statsMenu = FindObjectOfType<StatsMenu>();

        this.pokeData = Data.instance.pokeData;
        selectObject.SetActive(true);
        selectTypes.SetActive(false);
        pokemonRegexField.text = "";
        this.pokemon = pokemon;
        selectSpecies.interactable = false;
        selectedCard = null;

        cardContentsUpdate();
    }

    private void cardContentsUpdate(string regex)
    {
        selectSpecies.interactable = false;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var card in pokeData)
        {
            if (card.speciesName.ToUpper().Contains(regex.ToUpper()))
            {
                var button = Instantiate(PrefabSpriteButton, content.transform);
                button.GetComponent<PokemonSpeciesCard>().Initialize(card, this);
            }
        }
    }

    private void cardContentsUpdate()
    {
        cardContentsUpdate("");
    }

    public void onRegexCheckClick()
    {
        cardContentsUpdate(pokemonRegexField.text.Replace("\r", ""));
    }

    public void selectCard(PokemonSpeciesCard card)
    {
        if (selectedCard != null) selectedCard.unselect();
        selectedCard = card;
        selectSpecies.interactable = true;
    }

    public void onSelectSpeciesClick()
    {
        pokemon.pokemonData = selectedCard.getPokeData();
        selectObject.SetActive(false);
        selectedCard = null;
        selectTypes.SetActive(true);

        var options = new List<string>();
        foreach (var type in Enum.GetNames(typeof(pokemonType)))
        {
            options.Add(PokemonObject.toUpperString(PokemonTypeClass.translateEnglishSpanish(type)));
        }

        type1Dropdown.ClearOptions();
        type2Dropdown.ClearOptions();

        type1Dropdown.AddOptions(options);
        type1Dropdown.options.RemoveAt(0);
        options.Add(" - ");
        type2Dropdown.AddOptions(options);
        type2Dropdown.options.RemoveAt(0);

        type1Dropdown.value = PokemonTypeClass.getTypeIndex(pokemon.pokemonData.type1);
        type2Dropdown.value = PokemonTypeClass.getTypeIndex(pokemon.pokemonData.type2) < 0 ? 22 : PokemonTypeClass.getTypeIndex(pokemon.pokemonData.type2);

        pokemonSprite.sprite = pokemon.pokemonData.sprite;
        pokemonName.text = PokemonObject.toUpperString(pokemon.pokemonData.speciesName);
    }

    public void onFinishClick()
    {
        pokemon.type1 = PokemonTypeClass.getTypeByIndex(type1Dropdown.value);
        pokemon.type2 = PokemonTypeClass.getTypeByIndex(type2Dropdown.value);

        FileDataHandler.instance.Save();
        statsMenu.Initialize();
        PCController.instance.viewBox();
        gameObject.SetActive(false);
    }

    public void onGoBackClick()
    {
        gameObject.SetActive(false);
    }
}
