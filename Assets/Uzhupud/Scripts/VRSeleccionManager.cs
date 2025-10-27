using UnityEngine;
using UnityEngine.Events;

// MANAGER: asegura seleccion unica y emite id
public class VRSeleccionManager : MonoBehaviour
{
    public UnityEvent<string> OnSeleccionConId;

    private VRSelectable actual;

    public void Seleccionar(VRSelectable nuevo)
    {
        if (actual == nuevo) return;
        if (actual != null) actual.SetActivo(false);
        actual = nuevo;
        if (actual != null) actual.SetActivo(true);
        OnSeleccionConId?.Invoke(nuevo.id);
    }

    public void Deseleccionar(VRSelectable item)
    {
        if (actual == item)
        {
            actual.SetActivo(false);
            actual = null;
        }
    }
}
