using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateConstitution : MonoBehaviour
{
    [SerializeField] CreatePokemonMenu creationMenu;

    [Header("Attributes")]
    [SerializeField] GameObject main;
    [SerializeField] GameObject chooseConstitutionObject;

    [Header("Attributes")]
    [SerializeField] GameObject content;
    public GameObject constitutionCardPrefab;
    public Button selectConstitution;
    public TMP_Text descriptionField;

    private List<ConstitutionData> constitutionList;

    PokemonConstitutionCard selectedConstitution;

    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        main.SetActive(true);
        chooseConstitutionObject.SetActive(false);
        constitutionList = new List<ConstitutionData>(creationMenu.constitutionData);

        if (creationMenu.isRandomSpecies) onRandomClick();
    }

    public void onSelectClick()
    {
        main.SetActive(false);
        chooseConstitutionObject.SetActive(true);

        selectConstitution.interactable = false;

        selectedConstitution = null;
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }

        foreach (var constitution in constitutionList)
        {
            var card = Instantiate(constitutionCardPrefab, content.transform);
            card.GetComponent<PokemonConstitutionCard>().Initialize(constitution, this);
        }
    }

    public void constitutionSelected(PokemonConstitutionCard constitution)
    {
        if (selectedConstitution != constitution)
        {
            if (selectedConstitution != null)
            {
                selectedConstitution.setSelected(false);
                selectedConstitution = null;
                updateDescriptionText(selectedConstitution);
            }
            selectedConstitution = constitution;
            selectedConstitution.setSelected(true);
            updateDescriptionText(selectedConstitution);
        }

        if (selectedConstitution != null) selectConstitution.interactable = true;
        else selectConstitution.interactable = false;
    }

    public void updateDescriptionText(PokemonConstitutionCard constitution)
    {
        if (constitution == null)
        {
            descriptionField.text = "";
        }
        else
        {
            descriptionField.text = constitution.getConstitutionData().bonus;
        }
    }

    public void onSelectConstitutionClick()
    {
        creationMenu.step7(selectedConstitution.getConstitutionData());
    }

    public void onRandomClick()
    {
        creationMenu.step7(constitutionList[Random.Range(0, constitutionList.Count)]);
    }
}
