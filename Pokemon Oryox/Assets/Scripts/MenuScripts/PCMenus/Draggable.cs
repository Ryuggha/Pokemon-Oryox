using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Image image;
    public Transform parentAfterDrag { get; set; }
    public PokemonObject pokemon;
    public bool nonDraggeable;

    private int beforeDragIndex;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Initiate(PokemonObject pokemon)
    {
        this.pokemon = pokemon;

        var sprite = GetComponent<Image>();
        if (pokemon.isShiny) sprite.sprite = pokemon.pokemonData.shiny;
        else sprite.sprite = pokemon.pokemonData.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (nonDraggeable) return;
        var slot = transform.parent.GetComponent<UISnapper>();

        beforeDragIndex = slot.index;

        if (slot.text != null)
        {
            slot.text.text = "";
        }

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (nonDraggeable) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (nonDraggeable) return;
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        transform.localPosition = new Vector2(0, 0);

        var slot = transform.parent.GetComponent<UISnapper>();

        PCController.instance.pokemonMoved(beforeDragIndex, slot.index);

        setName(slot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PokemonList.instance.activateStatsScreen();
        StatsMenu.instance.Initialize(pokemon);
    }

    public void setName()
    {
        setName(null);
    }

    public void setName(UISnapper slot)
    {
        if (slot == null) slot = transform.parent.GetComponent<UISnapper>();

        if (slot.text != null)
        {
            slot.text.text = pokemon.getName();
        }
    }
}
