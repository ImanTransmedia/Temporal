using UnityEngine;

public class BolaSelectable : MonoBehaviour
{
    public int Indice { get; private set; }
    public Material MaterialBase { get; private set; }

    private DynamicObject _manager;
    private Renderer _rend;
    private Material _highlightMat;
    private Material _cachedBaseMat;

    // llamado por DynamicBolas al crear
    public void Configurar(DynamicObject manager, int indice, Renderer rend, Material baseMat, Material highlightMat)
    {
        _manager = manager;
        Indice = indice;
        _rend = rend;
        MaterialBase = baseMat;
        _highlightMat = highlightMat;
        _cachedBaseMat = baseMat;

        if (_rend != null && baseMat != null)
        {
            if (Application.isPlaying) _rend.material = baseMat;
            else _rend.sharedMaterial = baseMat;
        }
    }

    public void SetHighlight(bool on)
    {
        if (_rend == null) return;
        if (on && _highlightMat != null)
        {
            if (Application.isPlaying) _rend.material = _highlightMat;
            else _rend.sharedMaterial = _highlightMat;
        }
        else
        {
            if (Application.isPlaying) _rend.material = _cachedBaseMat;
            else _rend.sharedMaterial = _cachedBaseMat;
        }
    }

    // integra con tus eventos de Meta XR / XRI
    public void Seleccionar() { if (_manager != null) _manager.Seleccionar(this); }
    public void Deseleccionar() { if (_manager != null) _manager.Deseleccionar(this); }

    public void OnBoolChanged(bool v) { if (v) Seleccionar(); else Deseleccionar(); }
    public void OnWhenSelect() { Seleccionar(); }     // InteractableUnityEventWrapper -> WhenSelect
    public void OnWhenUnselect() { Deseleccionar(); } // InteractableUnityEventWrapper -> WhenUnselect
    public void OnXRISelectEntered(object _) { Seleccionar(); }
    public void OnXRISelectExited(object _) { Deseleccionar(); }
}
