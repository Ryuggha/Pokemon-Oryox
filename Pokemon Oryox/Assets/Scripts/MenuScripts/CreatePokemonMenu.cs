using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePokemonMenu : MonoBehaviour
{

    [SerializeField] MainMenu mainMenu;

    [SerializeField] GameObject[] steps;

    int activeStep;

    public List<PokemonApiData> pokeData { get; private set; }
    public List<NatureData> natureData { get; private set; }
    public List<ConstitutionData> constitutionData { get; private set; }
    public List<MoveData> moveRawData { get; private set; }

    public Dictionary<int, List<MoveData>> activeMoveData;
    public Dictionary<int, List<MoveData>> pasiveData;

    [Header("New Pokemon Data")]
    public PokemonObject newPokemon;
    public int numberOfPassives;

    public bool isRandomSpecies;

    private void OnEnable()
    {
        resetDefault();
    }


    public void onGoBackClick()
    {
        mainMenu.returnToMenu();
    }

    public void resetDefault()
    {
        isRandomSpecies = false;
        newPokemon = null;
        numberOfPassives = 0;
        activeStep = 0;

        pokeData = Data.instance.pokeData;
        natureData = Data.instance.natureData;
        constitutionData = Data.instance.constitutionData;
        moveRawData = Data.instance.moveRawData;
        activeMoveData = Data.instance.activeMoveData;
        pasiveData = Data.instance.pasiveData;

        adminSteps();
    }

    private void nextStep()
    {
        activeStep++;
        adminSteps();
    }

    private void adminSteps()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i].SetActive(false);
        }
        steps[activeStep].SetActive(true);
    }

    public void step2(PokemonObject pokemon)
    { 
        this.newPokemon = pokemon;
        nextStep();
    }

    public void step3(int numberOfPassives)
    {
        this.numberOfPassives = numberOfPassives;
        nextStep();
    }

    public void step4(List<MoveData> pasives)
    {
        newPokemon.pasives = pasives;
        nextStep();
    }

    public void step5(List<MoveData> moves)
    {
        newPokemon.moves = moves;
        nextStep();
    }

    public void step6(NatureData nature, string property1, string property2)
    {
        newPokemon.nature = nature;
        newPokemon.natureProperty1 = property1;
        newPokemon.natureProperty2 = property2;
        nextStep();
    }

    public void step7(ConstitutionData constitution)
    {
        newPokemon.constitution = constitution;
        nextStep();
    }

    public void finish(PokemonObject pokemon)
    {
        resetDefault();
        for (int i = 0; i < 4; i++)
        {
            if (pokemon.moves.Count > i)
            {
                pokemon.equipedMoves.Add(pokemon.moves[i]);
            }
        }
        PokemonList.instance.addPokemon(pokemon);
        mainMenu.returnToMenu();
    }
}
