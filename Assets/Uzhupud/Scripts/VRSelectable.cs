using UnityEngine;

public class VRSelectable : MonoBehaviour
{
    public string id = "piso_0";
    public GameObject objetoActivar;
    private VRSeleccionManager manager;

    void Awake()
    {
        manager = FindObjectOfType<VRSeleccionManager>();
    }

    public void Seleccionar()
    {
        if (manager != null) manager.Seleccionar(this);
    }

    public void Deseleccionar()
    {
        if (manager != null) manager.Deseleccionar(this);
    }

    public void SetActivo(bool activo)
    {
        if (objetoActivar != null) objetoActivar.SetActive(activo);
    }

    // ADAPTADORES PARA DIFERENTES EVENTOS

    // Meta XR Interaction SDK: InteractableUnityEventWrapper -> WhenSelect / WhenUnselect (sin parametros)
    public void OnWhenSelect() { Seleccionar(); }
    public void OnWhenUnselect() { Deseleccionar(); }

    // Eventos que envian bool (true seleccionado / false no)
    public void OnBoolChanged(bool v) { if (v) Seleccionar(); else Deseleccionar(); }

    // XR Interaction Toolkit (XRI) selectEntered/selectExited
    public void OnXRISelectEntered(object _args) { Seleccionar(); }
    public void OnXRISelectExited(object _args) { Deseleccionar(); }
}
