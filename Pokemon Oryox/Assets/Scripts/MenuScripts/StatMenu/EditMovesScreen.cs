using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditMovesScreen : MonoBehaviour, IMoveGeneratorEditor
{
    public int menuType; // 0 - PokemonMoves, 1 - Learnable Moves, 3 - Learnable Pasives

    [Header("Attributes")]
    private PokemonObject pokemon;
    [SerializeField] GameObject content;
    public GameObject moveCard;
    public Button selectMovesButton;
    public TMP_Text descriptionField;
    public GameObject editMovesScreen;

    List<PokemonMoveCard> selectedMoves;
    PokemonMoveCard shownMove;
    Dictionary<int, List<MoveData>> allMoves;
    List<MoveData> allMovesList;

    private int maxMovesSelected;

    public void Initialize()
    {
        Initialize(pokemon);
    }

    public void Initialize(PokemonObject pokemon)
    {
        this.pokemon = pokemon;

        selectedMoves = new List<PokemonMoveCard>();

        if (menuType == 0) allMovesList = pokemon.moves;
        else if (menuType == 1) allMoves = Data.instance.activeMoveData;
        else if (menuType == 2) allMoves = Data.instance.pasiveData;

        var moveList = generateMovesList();

        selectMovesButton.interactable = false;

        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        if (menuType == 0) maxMovesSelected = 4;
        else if (menuType == 1) maxMovesSelected = 9999;
        else if (menuType == 2) maxMovesSelected = 6;

        foreach (var move in moveList)
        {
            var card = Instantiate(moveCard, content.transform).GetComponent<PokemonMoveCard>();
            card.Initialize(move, this);
            if (menuType == 0 && pokemon.equipedMoves.Contains(move)) moveSelected(card);
            if (menuType == 1 && pokemon.moves.Contains(move)) moveSelected(card);
            if (menuType == 2 && pokemon.pasives.Contains(move)) moveSelected(card);
        }

        unshowMove();
    }

    private List<MoveData> generateMovesList()
    {
        List<MoveData> retList;
        if (menuType == 0)
        {
            retList = allMovesList;
        }
        else
        {
            retList = new List<MoveData>(allMoves[PokemonTypeClass.getTypeIndex(pokemonType.normal)]);

            pokemonType type1 = pokemon.type1;
            if (type1 != pokemonType.normal) retList.AddRange(allMoves[PokemonTypeClass.getTypeIndex(type1)]);
            pokemonType type2 = pokemon.type2;
            if (type2 != type1 && type2 != pokemonType.normal && type2 != pokemonType.none) retList.AddRange(allMoves[PokemonTypeClass.getTypeIndex(type2)]);
        }
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
        else if (selectedMoves.Count < maxMovesSelected)
        {
            if (shownMove != null) shownMove.setSelected(true);
            selectedMoves.Add(move);
            move.setSelected(true, true);
            shownMove = move;
            showDescription(move);
        }

        if (selectedMoves.Count > 0) selectMovesButton.interactable = true;
        else selectMovesButton.interactable = false;

    }

    public void unshowMove()
    {
        if (shownMove != null)
        {
            shownMove.setSelected(true);
            shownMove = null;
            showDescription(null);
        }
    }

    public void showDescription(PokemonMoveCard move)
    {
        if (move == null) descriptionField.text = "";
        else descriptionField.text = move.getMoveData().description;
    }

    public void onSelectMoveClick()
    {
        if (menuType == 0)
        {
            pokemon.equipedMoves.Clear();
            foreach (PokemonMoveCard move in selectedMoves.ToArray())
            {
                pokemon.equipedMoves.Add(move.getMoveData());
            }
            StatsMenu.instance.movesUpdate();
        }
        else if (menuType == 1)
        {
            pokemon.moves.Clear();
            foreach (PokemonMoveCard move in selectedMoves.ToArray())
            {
                pokemon.moves.Add(move.getMoveData());
            }
            editMovesScreen.GetComponent<EditMovesScreen>().Initialize();
        }
        else if (menuType == 2)
        {
            pokemon.pasives.Clear();
            foreach (PokemonMoveCard move in selectedMoves.ToArray())
            {
                pokemon.pasives.Add(move.getMoveData());
            }
            StatsMenu.instance.movesUpdate();
        }

        FileDataHandler.instance.Save();
        gameObject.SetActive(false);
    }

    public void onEditMovesClick()
    {
        editMovesScreen.SetActive(true);
        editMovesScreen.GetComponent<EditMovesScreen>().Initialize(pokemon);
    }

    public void onGoBackButton()
    {
        gameObject.SetActive(false);
    }
}
