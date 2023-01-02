using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PCController : MonoBehaviour
{
    public static PCController instance;

    [SerializeField] MainMenu mainMenu;

    [Header("References")]
    [SerializeField] TMP_Text boxNameText;
    [SerializeField] UISnapper[] boxGrid;
    [SerializeField] UISnapper[] partyGrid;
    public GameObject pokemonSprite;
    
    private Dictionary<int, PokemonObject> pokeList;
    private int boxNumber;
    private int lastBoxNumber;

    private List<Draggable> uiElementList;

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

    private void OnEnable()
    {
        resetDefault();
    }

    private void OnDisable()
    {
        destroyPokemonUIElements();
    }

    public void resetDefault()
    {
        uiElementList = new List<Draggable>();
        pokeList = PokemonList.instance.allMyPokemon;

        int maxIndex = 0;
        foreach (var index in pokeList.Keys)
        {
            if (index > maxIndex) maxIndex = index;
        }
        lastBoxNumber = maxIndex / 30;
        lastBoxNumber++;
        if (lastBoxNumber < 5) lastBoxNumber = 5;

        for (int i = -6; i < 30; i++)
        {
            if (i < 0)
            {
                partyGrid[i + 6].index = i;
            }
            else boxGrid[i].index = i;
        }

        viewBox();
    }

    public void viewBox()
    {
        
        boxNameText.text = $"Caja {boxNumber + 1}";

        destroyPokemonUIElements();

        viewParty();

        for (int i = 0; i < 30; i++)
        {
            createPokemonUIElement(i, false);
        }
    }

    private void destroyPokemonUIElements()
    {
        foreach (var slot in partyGrid)
        {
            slot.GetComponentInChildren<TMP_Text>().text = "";
        }
        for (int i = 0; i < uiElementList.Count; i++)
        {
            GameObject.Destroy(uiElementList[i].gameObject);
        }
        uiElementList.Clear();
    }

    public void viewParty()
    {
        for (int i = -6; i < 0; i++)
        {
            createPokemonUIElement(i, true);
        }
    }

    public void createPokemonUIElement(int i, bool isParty)
    {
        if (!pokeList.ContainsKey((i < 0) ? i : i + (boxNumber * 30))) return;
        
        var trans = (i < 0) ? partyGrid[i + 6].transform : boxGrid[i].transform;
        var spriteObject = Instantiate(pokemonSprite, trans);

        var draggable = spriteObject.GetComponent<Draggable>();
        draggable.Initiate(pokeList[(i < 0) ? i : i + (boxNumber * 30)]);
        uiElementList.Add(draggable);
        if (isParty) draggable.setName();
    }

    public void onNextBoxClick()
    {
        boxNumber++;
        if (boxNumber > lastBoxNumber) boxNumber = 0;
        viewBox();
    }

    public void onPrevBoxClick()
    {
        boxNumber--;
        if (boxNumber < 0) boxNumber = lastBoxNumber;
        viewBox();
    }

    public void onBackClick()
    {
        FileDataHandler.instance.Save();
        mainMenu.returnToMenu();
    }

    public void pokemonMoved(int uiBefore, int uiAfter)
    {
        int before = (uiBefore < 0) ? uiBefore : uiBefore + (boxNumber * 30);
        int after = (uiAfter < 0) ? uiAfter : uiAfter + (boxNumber * 30);

        if (before == after) return;

        var aux = pokeList[before];
        pokeList.Remove(before);
        pokeList.Add(after, aux);
    }

    public void delete(PokemonObject o)
    {
        var key = pokeList.FirstOrDefault(x => x.Value == o).Key;
        pokeList.Remove(key);

        FileDataHandler.instance.Save();

        viewBox();
    }
}
