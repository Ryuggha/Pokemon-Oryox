using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateNatures : MonoBehaviour
{
    [SerializeField] CreatePokemonMenu creationMenu;

    [Header("Attributes")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject chooseNatureObject;

    [Header("Attributes")]
    [SerializeField] GameObject content;
    public GameObject natureCardPrefab;
    public Button selectNature;
    public TMP_Dropdown property1Dropdown;
    public TMP_Dropdown property2Dropdown;

    private List<NatureData> natureList;

    PokemonNatureCard selectedNature;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        main.SetActive(true);
        chooseNatureObject.SetActive(false);
        natureList = new List<NatureData>(creationMenu.natureData);

        if (creationMenu.isRandomSpecies) onRandomClick();
    }

    public void onSelectClick()
    {
        main.SetActive(false);
        chooseNatureObject.SetActive(true);

        selectNature.interactable = false;

        selectedNature = null;
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var nature in natureList)
        {
            var card = Instantiate(natureCardPrefab, content.transform);
            card.GetComponent<PokemonNatureCard>().Initialize(nature, this);
        }
    }

    public void natureSelected(PokemonNatureCard nature)
    {
        if (selectedNature != nature)
        {
            if (selectedNature != null)
            {
                selectedNature.setSelected(false);
                selectedNature = null;
                updateNatureDropdowns(selectedNature);
            }
            selectedNature = nature;
            selectedNature.setSelected(true);
            updateNatureDropdowns(selectedNature);
        }

        if (selectedNature != null) selectNature.interactable = true;
        else selectNature.interactable = false;
    }

    public void updateNatureDropdowns(PokemonNatureCard nature)
    {
        if (nature == null)
        {
            property1Dropdown.ClearOptions();
            property2Dropdown.ClearOptions();
        }
        else
        {
            property1Dropdown.AddOptions(new List<string>(nature.getNatureData().possiblePositive));
            property2Dropdown.AddOptions(new List<string>(nature.getNatureData().possibleNegative));
        }
    }

    public void onSelectNatureClick()
    {
        creationMenu.step6(selectedNature.getNatureData(), property1Dropdown.options[property1Dropdown.value].text, property2Dropdown.options[property2Dropdown.value].text);
    }

    public void onRandomClick()
    {
        NatureData retNature = natureList[Random.Range(0, natureList.Count)];

        creationMenu.step6(retNature, retNature.possiblePositive.Length == 0? "" : retNature.possiblePositive[Random.Range(0, retNature.possiblePositive.Length)], retNature.possibleNegative.Length == 0 ? "" : retNature.possibleNegative[Random.Range(0, retNature.possibleNegative.Length)]);
    }
}
