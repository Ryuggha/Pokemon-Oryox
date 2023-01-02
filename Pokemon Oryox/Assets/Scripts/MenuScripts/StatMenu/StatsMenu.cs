using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    public static StatsMenu instance;

    private PokemonObject pokemon;
    private Button selectedButton;
    private MoveData selectedMove;
    
    public Sprite idleSprite;
    public Sprite selectedSprite;

    public GameObject speciesChangeScreen;
    public GameObject moveHandlerScreen;
    public GameObject pasiveHandlerScreen;

    [Header("UI References")]
    public Image pokemonSprite;
    public GameObject tribalSprite;
    public TMP_Text pokemonNameText;
    public GameObject statsScreen;
    public GameObject moveScreen;
    public TMP_Text type1Text;
    public TMP_Text type2Text;
    public TMP_Text natureNameText;
    public TMP_Text natureTrait1;
    public TMP_Text natureTrait2;
    public TMP_Text constitutionNameText;
    public TMP_Text constitutionTraint;
    public TMP_Text movesTypesText;
    public TMP_Text movesCostText;
    public TMP_Text movesRangeText;
    public TMP_Text movesDescriptionText;
    public TMP_Text movesNotesText;
    public GameObject eraseButton;
    public GameObject confirmButton;

    [Header("MoveButtons")]
    public Button move1;
    public Button move2;
    public Button move3;
    public Button move4;
    public Button pasive1;
    public Button pasive2;
    public Button pasive3;
    public Button pasive4;
    public Button pasive5;
    public Button pasive6;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(PokemonObject pokemon)
    {
        this.pokemon = pokemon;

        eraseButton.SetActive(true);
        confirmButton.SetActive(false);

        if (pokemon.isShiny) pokemonSprite.sprite = pokemon.pokemonData.shiny;
        else pokemonSprite.sprite = pokemon.pokemonData.sprite;
        if (pokemon.isTribal) tribalSprite.SetActive(true);
        else tribalSprite.SetActive(false);

        pokemonNameText.text = pokemon.getName();

        statsScreen.SetActive(true);
        moveScreen.SetActive(true);

        type1Text.text = PokemonObject.toUpperString(PokemonTypeClass.translateEnglishSpanish(pokemon.type1.ToString()));
        type2Text.text = PokemonObject.toUpperString(PokemonTypeClass.translateEnglishSpanish(pokemon.type2.ToString()));

        natureNameText.text = pokemon.nature.natureName;
        natureTrait1.text = pokemon.natureProperty1;
        natureTrait2.text = pokemon.natureProperty2;

        constitutionNameText.text = pokemon.constitution.constitutionName;
        constitutionTraint.text = pokemon.constitution.bonus;

        selectedButton = null;
        selectedMove = null;
        updateMoves();

        moveScreen.SetActive(false);
    }

    public void movesUpdate()
    {
        updateMoves();
    }

    public void Initialize()
    {
        Initialize(this.pokemon);
    }

    private void updateMoves()
    {
        updateMoveDescription();
        if (pokemon.equipedMoves.Count >= 1)
        {
            move1.gameObject.SetActive(true);
            move1.GetComponentInChildren<TMP_Text>().text = pokemon.equipedMoves[0].moveName;
            move1.image.sprite = idleSprite;
        }
        else move1.gameObject.SetActive(false);

        if (pokemon.equipedMoves.Count >= 2)
        {
            move2.gameObject.SetActive(true);
            move2.GetComponentInChildren<TMP_Text>().text = pokemon.equipedMoves[1].moveName;
            move2.image.sprite = idleSprite;
        }
        else move2.gameObject.SetActive(false);
        if (pokemon.equipedMoves.Count >= 3)
        {
            move3.gameObject.SetActive(true);
            move3.GetComponentInChildren<TMP_Text>().text = pokemon.equipedMoves[2].moveName;
            move3.image.sprite = idleSprite;
        }
        else move3.gameObject.SetActive(false);
        if (pokemon.equipedMoves.Count >= 4)
        {
            move4.gameObject.SetActive(true);
            move4.GetComponentInChildren<TMP_Text>().text = pokemon.equipedMoves[3].moveName;
            move4.image.sprite = idleSprite;
        }
        else move4.gameObject.SetActive(false);

        if (pokemon.pasives.Count >= 1)
        {
            pasive1.gameObject.SetActive(true);
            pasive1.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[0].moveName;
            pasive1.image.sprite = idleSprite;
        }
        else pasive1.gameObject.SetActive(false);
        if (pokemon.pasives.Count >= 2)
        {
            pasive2.gameObject.SetActive(true);
            pasive2.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[1].moveName;
            pasive2.image.sprite = idleSprite;
        }
        else pasive2.gameObject.SetActive(false);
        if (pokemon.pasives.Count >= 3)
        {
            pasive3.gameObject.SetActive(true);
            pasive3.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[2].moveName;
            pasive3.image.sprite = idleSprite;
        }
        else pasive3.gameObject.SetActive(false);
        if (pokemon.pasives.Count >= 4)
        {
            pasive4.gameObject.SetActive(true);
            pasive4.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[3].moveName;
            pasive4.image.sprite = idleSprite;
        }
        else pasive4.gameObject.SetActive(false);
        if (pokemon.pasives.Count >= 5)
        {
            pasive5.gameObject.SetActive(true);
            pasive5.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[4].moveName;
            pasive5.image.sprite = idleSprite;
        }
        else pasive5.gameObject.SetActive(false);
        if (pokemon.pasives.Count >= 6)
        {
            pasive6.gameObject.SetActive(true);
            pasive6.GetComponentInChildren<TMP_Text>().text = pokemon.pasives[5].moveName;
            pasive6.image.sprite = idleSprite;
        }
        else pasive6.gameObject.SetActive(false);
    }

    public void onBackClick()
    {
        gameObject.SetActive(false);
    }

    public void onStatsClick()
    {
        statsScreen.SetActive(true);
        moveScreen.SetActive(false);
    }

    public void onMovesClick()
    {
        statsScreen.SetActive(false);
        moveScreen.SetActive(true);
    }

    public void onChangeFormClick()
    {
        speciesChangeScreen.SetActive(true);
        speciesChangeScreen.GetComponent<SpeciesChangeViewPort>().Initialize(pokemon);
    }

    public void onMoveEditClick()
    {
        moveHandlerScreen.SetActive(true);
        moveHandlerScreen.GetComponent<EditMovesScreen>().Initialize(pokemon);
    }

    public void onPasiveEditClick()
    {
        pasiveHandlerScreen.SetActive(true);
        pasiveHandlerScreen.GetComponent<EditMovesScreen>().Initialize(pokemon);
    }

    public void onMove1Click()
    {
        selectMove(false, 1);
    }
    public void onMove2Click()
    {
        selectMove(false, 2);
    }
    public void onMove3Click()
    {
        selectMove(false, 3);
    }
    public void onMove4Click()
    {
        selectMove(false, 4);
    }
    public void onPasive1Click()
    {
        selectMove(true, 1);
    }
    public void onPasive2Click()
    {
        selectMove(true, 2);
    }
    public void onPasive3Click()
    {
        selectMove(true, 3);
    }
    public void onPasive4Click()
    {
        selectMove(true, 4);
    }
    public void onPasive5Click()
    {
        selectMove(true, 5);
    }
    public void onPasive6Click()
    {
        selectMove(true, 6);
    }

    private void selectMove(bool isPasive, int index)
    {
        Button buttonPressed = selectedButton;
        MoveData movePressed = selectedMove;
        if (isPasive)
        {
            switch (index)
            {
                case 1: buttonPressed = pasive1; movePressed = pokemon.pasives[0]; break;
                case 2: buttonPressed = pasive2; movePressed = pokemon.pasives[1]; break;
                case 3: buttonPressed = pasive3; movePressed = pokemon.pasives[2]; break;
                case 4: buttonPressed = pasive4; movePressed = pokemon.pasives[3]; break;
                case 5: buttonPressed = pasive5; movePressed = pokemon.pasives[4]; break;
                case 6: buttonPressed = pasive6; movePressed = pokemon.pasives[5]; break;
                default: Debug.Log("Error: Bad index at pressing pasive button"); break;
            }
        }
        else
        {
            switch (index)
            {
                case 1: buttonPressed = move1; movePressed = pokemon.equipedMoves[0]; break;
                case 2: buttonPressed = move2; movePressed = pokemon.equipedMoves[1]; break;
                case 3: buttonPressed = move3; movePressed = pokemon.equipedMoves[2]; break;
                case 4: buttonPressed = move4; movePressed = pokemon.equipedMoves[3]; break;
                default: Debug.Log("Error: Bad index at pressing move button"); break;
            }
        }

        if (movePressed == selectedMove)
        {
            buttonPressed.GetComponent<Image>().sprite = idleSprite;
            selectedButton = null;
            selectedMove = null;
        }
        else {
            if (selectedButton != null) selectedButton.GetComponent<Image>().sprite = idleSprite;
            selectedButton = buttonPressed;
            selectedMove = movePressed;
            selectedButton.GetComponent<Image>().sprite = selectedSprite;
        }
        updateMoveDescription();
    }

    private void updateMoveDescription()
    {
        if (selectedMove == null)
        {
            movesTypesText.text = "";
            movesCostText.text = "";
            movesRangeText.text = "";
            movesDescriptionText.text = "";
            movesNotesText.text = "";
        }
        else
        {
            movesTypesText.text = PokemonObject.toUpperString(PokemonTypeClass.translateEnglishSpanish(selectedMove.type.ToString()));
            movesCostText.text = selectedMove.coste;
            movesRangeText.text = (selectedMove.range == "") ? "" : "Rango: " + selectedMove.range.Replace("\r", "");
            movesDescriptionText.text = selectedMove.description.Replace("\r", "");
            movesNotesText.text = selectedMove.notes.Replace("\r", "");
        }
    }

    public void onEraseClick()
    {
        confirmButton.SetActive(true);
        eraseButton.SetActive(false);
    }

    public void onCancelClick()
    {
        confirmButton.SetActive(false);
        eraseButton.SetActive(true);
    }

    public void onConfirmClick()
    {
        confirmButton.SetActive(false);
        eraseButton.SetActive(true);

        PCController.instance.delete(pokemon);

        onBackClick();
    }
}
