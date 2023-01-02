using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateFinalSteps : MonoBehaviour
{
    [SerializeField] CreatePokemonMenu creationMenu;
    [Header("References")]
    public Image spriteImage;
    public TMP_InputField nicknameInput;
    public Button createPokemonButton;
    public TMP_Text speciesText;
    public GameObject shinyOptionsObject;
    public GameObject shinyChooseableObject;
    public Sprite buttonNormal;
    public Sprite buttonSelected;
    public Button shinyButton;
    public Button tribalButton;
    public Image tribalImage;

    [Header("Attributes")]
    private PokemonObject newPokemon;
    private bool isShiny;
    private bool isTribal;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        isShiny = false;
        isTribal = false;
        this.newPokemon = creationMenu.newPokemon;
        shinyOptionsObject.SetActive(true);
        shinyChooseableObject.SetActive(false);
        createPokemonButton.interactable = false;
        setPokemonSprite();
        nicknameInput.text = PokemonObject.toUpperString(newPokemon.pokemonData.speciesName);
        speciesText.text = PokemonObject.toUpperString(newPokemon.pokemonData.speciesName);

        if (creationMenu.isRandomSpecies) 
        {
            onShinyOptionsRandomClick();
            onCreatePokemonClick();
        }
    }

    public void onShinyChooseClick()
    {
        if (isShiny)
        {
            shinyButton.image.sprite = buttonNormal;
            isShiny = false;
        }
        else
        {
            shinyButton.image.sprite = buttonSelected;
            isShiny = true;
        }
        setPokemonSprite();
    }

    public void onTribalChooseclick()
    {
        if (isTribal)
        {
            tribalButton.image.sprite = buttonNormal;
            isTribal = false;
        }
        else
        {
            tribalButton.image.sprite = buttonSelected;
            isTribal = true;
        }
        setPokemonSprite();
    }

    public void setPokemonSprite()
    {
        if (isShiny) spriteImage.sprite = newPokemon.pokemonData.shiny;
        else spriteImage.sprite = newPokemon.pokemonData.sprite;
        if (isTribal) tribalImage.gameObject.SetActive(true);
        else tribalImage.gameObject.SetActive(false);
    }

    public void onShinyOptionsSelectClick()
    {
        shinyOptionsObject.SetActive(false);
        shinyChooseableObject.SetActive(true);
        createPokemonButton.interactable = true;
    }

    public void onShinyOptionsRandomClick()
    {
        shinyOptionsObject.SetActive(false);
        createPokemonButton.interactable = true;

        isShiny = Random.Range(0, 20) == 0;

        isTribal = Random.Range(0, 100) == 0;
        setPokemonSprite();
    }

    public void onCreatePokemonClick()
    {
        newPokemon.isShiny = isShiny;
        newPokemon.isTribal = isTribal;
        newPokemon.nickname = (nicknameInput.text.ToUpper() == newPokemon.pokemonData.speciesName.ToUpper()) ? "" : nicknameInput.text;
        creationMenu.finish(newPokemon);
    }
}
