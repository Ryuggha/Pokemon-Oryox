using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PokemonList pokemonList;



    [SerializeField] GameObject generateMenu;
    [SerializeField] GameObject partyMenu;
    [SerializeField] GameObject pcMenu;

    public void onGenerateClick()
    {
        generateMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void returnToMenu()
    {
        FileDataHandler.instance.Save();
        gameObject.SetActive(true);
        generateMenu.SetActive(false);
        partyMenu.SetActive(false);
        pcMenu.SetActive(false);
    }

    public void onPartyClick()
    {
        partyMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void onPCClick()
    {
        pcMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
