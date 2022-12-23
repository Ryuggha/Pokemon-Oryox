using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class SpeciesSelectViewPort : MonoBehaviour
{
    public CreatePokemonMenu creationMenu;

    List<PokemonApiData> pokeData;

    [Header("Sections")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject selectObject;
    [SerializeField] GameObject selectTypes;

    [Header("Variables")]
    public GameObject content;
    public GameObject PrefabSpriteButton;
    [SerializeField] Button selectSpecies;
    [SerializeField] TMP_InputField pokemonRegexField;

    PokemonSpeciesCard selectedCard;

    PokemonObject newPokemon;

    [SerializeField] TMP_Dropdown type1Dropdown;
    [SerializeField] TMP_Dropdown type2Dropdown;
    [SerializeField] Image pokemonSprite;
    [SerializeField] TMP_Text pokemonName;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        this.pokeData = creationMenu.pokeData;
        main.SetActive(true);
        selectObject.SetActive(false);
        selectTypes.SetActive(false);
        pokemonRegexField.text = "";
        newPokemon = null;
        selectSpecies.interactable = false;
        selectedCard = null;
    }

    public void onSelectChoice()
    {
        main.SetActive(false);
        selectObject.SetActive(true);

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
        cardContentsUpdate(pokemonRegexField.text);
    }

    public void selectCard(PokemonSpeciesCard card)
    {
        if (selectedCard != null) selectedCard.unselect();
        selectedCard = card;
        selectSpecies.interactable = true;
    }

    public void onSelectSpeciesClick()
    {
        newPokemon = new PokemonObject(selectedCard.getPokeData());
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

        type1Dropdown.value = PokemonTypeClass.getTypeIndex(newPokemon.pokemonData.type1);
        type2Dropdown.value = PokemonTypeClass.getTypeIndex(newPokemon.pokemonData.type2) < 0 ? 22 : PokemonTypeClass.getTypeIndex(newPokemon.pokemonData.type2);

        pokemonSprite.sprite = newPokemon.pokemonData.sprite;
        pokemonName.text = PokemonObject.toUpperString(newPokemon.pokemonData.speciesName);
    }

    public void onFinishClick()
    {
        newPokemon.type1 = PokemonTypeClass.getTypeByIndex(type1Dropdown.value);
        newPokemon.type2 = PokemonTypeClass.getTypeByIndex(type2Dropdown.value);

        creationMenu.step2(newPokemon);
    }

    public void onRandomChoice()
    {
        creationMenu.isRandomSpecies = true;

        newPokemon = new PokemonObject(pokeData[Random.Range(0, pokeData.Count)]);
        newPokemon.type1 = newPokemon.pokemonData.type1;
        newPokemon.type2 = newPokemon.pokemonData.type2;

        creationMenu.step2(newPokemon);
    }


}
