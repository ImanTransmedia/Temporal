using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VRSelectable : MonoBehaviour
{
    public string id = "Piso_0";
    public GameObject highlight;

    public Sprite imagenPiso;
    public int level;
    public bool esIngresable;
    public string sceneName;

    [Header("Trigger")]
    public bool usarTriggers = true;
    public string tagMano = "VRHand";
    public LayerMask capasMano = ~0;

    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        if (usarTriggers && col != null) col.isTrigger = true;
    }

    public void Seleccionar()
    {
        var m = FindFirstObjectByType<VRSeleccionManager>();
        if (m != null) m.Seleccionar(this);
    }

    public void Deseleccionar()
    {
        var m = FindFirstObjectByType<VRSeleccionManager>();
        if (m != null) m.Deseleccionar(this);
    }

    public void SetActivo(bool activo)
    {
        if (highlight != null) highlight.SetActive(activo);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!usarTriggers) return;
        if (CumpleFiltro(other)) Seleccionar();
    }

    void OnTriggerExit(Collider other)
    {
        if (!usarTriggers) return;
        //if (CumpleFiltro(other)) Deseleccionar();
    }

    bool CumpleFiltro(Collider other)
    {
        //if (!string.IsNullOrEmpty(tagMano) && other.CompareTag(tagMano)) return true;
        return ((1 << other.gameObject.layer) & capasMano) != 0;
    }
}
