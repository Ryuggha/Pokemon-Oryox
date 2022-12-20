using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePokemonMenu : MonoBehaviour
{

    [SerializeField] MainMenu mainMenu;

    [SerializeField] GameObject[] steps;

    int activeStep;

    [Header("Data")]
    public List<PokemonApiData> pokeData;
    public List<NatureData> natureData;
    public List<ConstitutionData> constitutionData;
    public List<MoveData> moveRawData;

    public Dictionary<int, List<MoveData>> activeMoveData;
    public Dictionary<int, List<MoveData>> pasiveData;

    [Header("New Pokemon Data")]
    public PokemonObject newPokemon;
    public int numberOfPassives;

    public bool isRandomSpecies;

    private void OnEnable()
    {
        resetDefault();
        InitializeMaps();
    }

    private void InitializeMaps()
    {
        if (activeMoveData == null)
        {
            activeMoveData = new Dictionary<int, List<MoveData>>();
            pasiveData = new Dictionary<int, List<MoveData>>();

            foreach (var move in moveRawData)
            {
                if (move.isPasive)
                {
                    if (!pasiveData.ContainsKey(PokemonTypeClass.getTypeIndex(move.type))) pasiveData[PokemonTypeClass.getTypeIndex(move.type)] = new List<MoveData>();
                    pasiveData[PokemonTypeClass.getTypeIndex(move.type)].Add(move);
                }
                else
                {
                    if (!activeMoveData.ContainsKey(PokemonTypeClass.getTypeIndex(move.type))) activeMoveData[PokemonTypeClass.getTypeIndex(move.type)] = new List<MoveData>();
                    activeMoveData[PokemonTypeClass.getTypeIndex(move.type)].Add(move);
                }
            }
        }
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
        mainMenu.finishedPokemonCreation(pokemon);
    }
}
