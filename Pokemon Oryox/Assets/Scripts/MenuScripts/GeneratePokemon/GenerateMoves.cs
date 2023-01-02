using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateMoves : MonoBehaviour, IMoveGeneratorEditor
{
    [SerializeField] CreatePokemonMenu creationMenu;

    [Header("Sections")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject chooseMoveObject;

    [Header("Attributes")]
    private PokemonObject newPokemon;
    [SerializeField] GameObject content;
    public GameObject moveCard;
    public Button selectMoves;
    public TMP_Text descriptionField;

    List<PokemonMoveCard> selectedMoves;
    PokemonMoveCard shownMove;
    Dictionary<int, List<MoveData>> allMoves;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        main.SetActive(true);
        chooseMoveObject.SetActive(false);
        this.newPokemon = creationMenu.newPokemon;
        allMoves = creationMenu.activeMoveData;

        if (creationMenu.isRandomSpecies) onRandomClick();
    }

    public void onSelectClick()
    {
        main.SetActive(false);
        chooseMoveObject.SetActive(true);

        var moveList = generateMovesList();

        selectMoves.interactable = false;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var move in moveList)
        {
            var card = Instantiate(moveCard, content.transform);
            card.GetComponent<PokemonMoveCard>().Initialize(move, this);
        }

        selectedMoves = new List<PokemonMoveCard>();
    }

    private List<MoveData> generateMovesList()
    {        
        List<MoveData> retList = new List<MoveData>(allMoves[PokemonTypeClass.getTypeIndex(pokemonType.normal)]);

        pokemonType type1 = newPokemon.type1;

        if (type1 != pokemonType.normal) retList.AddRange(allMoves[PokemonTypeClass.getTypeIndex(type1)]);
        pokemonType type2 = newPokemon.type2;
        if (type2 != type1 && type2 != pokemonType.normal && type2 != pokemonType.none) retList.AddRange(allMoves[PokemonTypeClass.getTypeIndex(type2)]);

        return retList;
    }

    public void moveSelected(PokemonMoveCard move)
    {
        if (selectedMoves.Contains(move))
        {
            if (shownMove != null)
            {
                shownMove.setSelected(true);
                shownMove = null;
            }
            move.setSelected(false);
            selectedMoves.Remove(move);
            showDescription(null);
        }
        else
        {
            if (shownMove != null) shownMove.setSelected(true);
            selectedMoves.Add(move);
            move.setSelected(true, true);
            shownMove = move;
            showDescription(move);
        }

        if (selectedMoves.Count > 0) selectMoves.interactable = true;
        else selectMoves.interactable = false;

    }

    public void showDescription(PokemonMoveCard move)
    {
        if (move == null) descriptionField.text = "";
        else descriptionField.text = move.getMoveData().description.Replace("\r", "");
    }

    public void onSelectMoveClick()
    {
        var ret = new List<MoveData>();
        foreach (PokemonMoveCard move in selectedMoves.ToArray())
        {
            ret.Add(move.getMoveData());
        }
        creationMenu.step5(ret);
    }

    public void onRandomClick()
    {
        var ret = new List<MoveData>();

        var nonNormalTypes = new List<pokemonType>();
        if (newPokemon.type1 != pokemonType.normal) nonNormalTypes.Add(newPokemon.type1);
        if (newPokemon.type2 != pokemonType.none && newPokemon.type2 != pokemonType.normal) nonNormalTypes.Add(newPokemon.type2);

        for (int i = 0; i < 4; i++)
        {
            bool done = false;
            int tries = 0;
            do
            {
                MoveData toAdd;
                if (nonNormalTypes.Count > 0 && Random.Range(0, 3) != 0)
                {
                    pokemonType selectedType = nonNormalTypes[Random.Range(0, nonNormalTypes.Count)];

                    List<MoveData> typeList = allMoves[PokemonTypeClass.getTypeIndex(selectedType)];

                    toAdd = typeList[Random.Range(0, typeList.Count)];
                }
                else
                {
                    List<MoveData> normalList = allMoves[PokemonTypeClass.getTypeIndex(pokemonType.normal)];

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

        creationMenu.step5(ret);
    }

}
