using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratePasives : MonoBehaviour
{
    [SerializeField] CreatePokemonMenu creationMenu;

    [Header("Sections")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject choosePasiveObject;

    [Header("Attributes")]
    private PokemonObject newPokemon;
    [SerializeField] GameObject content;
    public GameObject pasiveCard;
    private int numberOfPasives;
    public Button selectPasives;
    public TMP_Text descriptionField;
    private Dictionary<int, List<MoveData>> allPasives;

    List<PokemonPasiveCard> selectedPasives;
    PokemonPasiveCard shownPasive;


    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        main.SetActive(true);
        choosePasiveObject.SetActive(false);
        this.newPokemon = creationMenu.newPokemon;
        this.numberOfPasives = creationMenu.numberOfPassives;
        allPasives = creationMenu.pasiveData;

        if (creationMenu.isRandomSpecies) onRandomClick(); 
    }

    public void onSelectClick()
    {
        main.SetActive(false);
        choosePasiveObject.SetActive(true);

        var pasiveList = generatePasiveList();

        selectPasives.interactable = false;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var pasive in pasiveList)
        {
            var card = Instantiate(pasiveCard, content.transform);
            card.GetComponent<PokemonPasiveCard>().Initialize(pasive, this);
        }

        selectedPasives = new List<PokemonPasiveCard>();
    }

    private List<MoveData> generatePasiveList()
    {
        List<MoveData> retList = new List<MoveData>(allPasives[PokemonTypeClass.getTypeIndex(pokemonType.normal)]);

        pokemonType type1 = newPokemon.type1;

        if (type1 != pokemonType.normal) retList.AddRange(allPasives[PokemonTypeClass.getTypeIndex(type1)]);
        pokemonType type2 = newPokemon.type2;
        if (type2 != type1 && type2 != pokemonType.normal && type2 != pokemonType.none) retList.AddRange(allPasives[PokemonTypeClass.getTypeIndex(type2)]);

        return retList;
    }

    public void pasiveSelected (PokemonPasiveCard pasive)
    {
        if (selectedPasives.Contains(pasive))
        {
            if (shownPasive != null)
            {
                shownPasive.setSelected(true);
                shownPasive = null;
            }
            pasive.setSelected(false);
            selectedPasives.Remove(pasive);
            showDescription(null);
        }
        else if (selectedPasives.Count < numberOfPasives)
        {
            if (shownPasive != null) shownPasive.setSelected(true);
            selectedPasives.Add(pasive);
            pasive.setSelected(true, true);
            shownPasive = pasive;
            showDescription(pasive);
        }

        if (selectedPasives.Count > 0) selectPasives.interactable = true;
        else selectPasives.interactable = false;
        
    }

    public void showDescription(PokemonPasiveCard pasive)
    {
        if (pasive == null) descriptionField.text = "";
        else descriptionField.text = pasive.getMoveData().description.Replace("\r", "");
    }

    public void onSelectPasiveClick()
    {
        var ret = new List<MoveData>();
        foreach (PokemonPasiveCard move in selectedPasives.ToArray())
        {
            ret.Add(move.getMoveData());
        }
        creationMenu.step4(ret);
    }

    public void onRandomClick()
    {
        var ret = new List<MoveData>();

        var nonNormalTypes = new List<pokemonType>();
        if (newPokemon.type1 != pokemonType.normal) nonNormalTypes.Add(newPokemon.type1);
        if (newPokemon.type2 != pokemonType.none && newPokemon.type2 != pokemonType.normal) nonNormalTypes.Add(newPokemon.type2);

        for (int i = 0; i < numberOfPasives; i++)
        {
            bool done = false;
            int tries = 0;
            do
            {
                MoveData toAdd;
                if (nonNormalTypes.Count > 0 && Random.Range(0, 3) != 0)
                {
                    pokemonType selectedType = nonNormalTypes[Random.Range(0, nonNormalTypes.Count)];

                    List<MoveData> typeList = allPasives[PokemonTypeClass.getTypeIndex(selectedType)];

                    toAdd = typeList[Random.Range(0, typeList.Count)];
                }
                else
                {
                    List<MoveData> normalList = allPasives[PokemonTypeClass.getTypeIndex(pokemonType.normal)];
                    
                    toAdd = normalList[Random.Range(0, normalList.Count)];
                }

                if (!ret.Contains(toAdd))
                {
                    ret.Add(toAdd);
                    done = true;
                }
                tries++;
            } while (!done && tries < 1000);
        }

        creationMenu.step4(ret);
    }
}
