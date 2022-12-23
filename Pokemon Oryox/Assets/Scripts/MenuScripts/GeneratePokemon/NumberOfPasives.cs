using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberOfPasives : MonoBehaviour
{
    [SerializeField] CreatePokemonMenu menu;

    public Slider slider;
    public TMP_Text monitor;

    PokemonObject pokemon;

    public void Initialize(PokemonObject pokemon)
    {
        this.pokemon = pokemon;
        slider.value = 1;
    }

    public void onSliderUpdate ()
    {
        monitor.text = slider.value.ToString();
    }

    public void onAcceptButton()
    {
        menu.step3((int) slider.value);
    }
}
