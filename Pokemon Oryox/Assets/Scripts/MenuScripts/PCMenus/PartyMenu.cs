using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartyMenu : MonoBehaviour
{
    [SerializeField] MainMenu mainMenu;

    [Header("References")]
    [SerializeField] UISnapper[] partyGrid;
    public GameObject pokemonSprite;

    private Dictionary<int, PokemonObject> pokeList;
    private List<Draggable> uiElementList;

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

        for (int i = -6; i < 0; i++)
        {
            partyGrid[i + 6].index = i;
        }

        viewParty();
    }

    private void viewParty()
    {
        destroyPokemonUIElements();
        for (int i = -6; i < 0; i++)
        {
            if (!pokeList.ContainsKey(i)) continue;

            var trans = partyGrid[i + 6].transform;
            var spriteObject = Instantiate(pokemonSprite, trans);

            var draggable = spriteObject.GetComponent<Draggable>();
            draggable.Initiate(pokeList[i]);
            uiElementList.Add(draggable);
            draggable.setName();
            draggable.nonDraggeable = true;
        }
    }

    public void onBackClick()
    {
        mainMenu.returnToMenu();
    }

    private void destroyPokemonUIElements()
    {
        for (int i = 0; i < uiElementList.Count; i++)
        {
            GameObject.Destroy(uiElementList[i].gameObject);
        }
        foreach (var slot in partyGrid)
        {
            slot.GetComponentInChildren<TMP_Text>().text = "";
        }
        uiElementList.Clear();
    }
}
