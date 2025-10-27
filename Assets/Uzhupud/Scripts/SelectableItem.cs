using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectableItem : MonoBehaviour, IPointerClickHandler
{
    [Header("ID del objeto")]
    public int id;

    [Header("Highlight (opcional)")]
    public GameObject highlightObject;

    // Evento estático que emite el ID al hacer click
    public static UnityAction<int, SelectableItem> OnItemSelected;

    private bool isSelected = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Emitir el evento global
        OnItemSelected?.Invoke(id, this);
    }

    // Activar o desactivar highlight desde fuera
    public void SetHighlight(bool active)
    {
        isSelected = active;
        if (highlightObject != null)
            highlightObject.SetActive(active);
    }
}
