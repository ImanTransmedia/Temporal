using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class IdScene
{
    public string id;
    public string sceneName;
}

public class VRSeleccionManager : MonoBehaviour
{
    public UnityEvent<string> OnSeleccionConId;
    public GameObject uiConCarga;
    public GameObject uiSinCarga;
    public List<IdScene> idsConCarga = new List<IdScene>(); // agrega {id="2",sceneName="EscenaPiso2"} y {id="8",sceneName="EscenaPiso8"}

    private VRSelectable actual;
    private string escenaPendiente;

    public void Seleccionar(VRSelectable nuevo)
    {
        if (actual == nuevo) return;

        if (actual != null) actual.SetActivo(false);
        actual = nuevo;
        if (actual != null) actual.SetActivo(true);

        OnSeleccionConId?.Invoke(nuevo.id);

        var tieneCarga = TryGetSceneForId(nuevo.id, out escenaPendiente);
        if (uiConCarga != null) uiConCarga.SetActive(tieneCarga);
        if (uiSinCarga != null) uiSinCarga.SetActive(!tieneCarga);
    }

    public void Deseleccionar(VRSelectable item)
    {
        if (actual == item)
        {
            actual.SetActivo(false);
            actual = null;
            escenaPendiente = null;
            OcultarUIs();
        }
    }

    public void Regresar()
    {
        if (actual != null) actual.SetActivo(false);
        actual = null;
        escenaPendiente = null;
        OcultarUIs();
    }

    public void CargarEscenaSeleccionada()
    {
        if (!string.IsNullOrEmpty(escenaPendiente))
            SceneManager.LoadScene(escenaPendiente);
    }

    private void OcultarUIs()
    {
        if (uiConCarga != null) uiConCarga.SetActive(false);
        if (uiSinCarga != null) uiSinCarga.SetActive(false);
    }

    private bool TryGetSceneForId(string id, out string sceneName)
    {
        for (int i = 0; i < idsConCarga.Count; i++)
        {
            if (idsConCarga[i].id == id)
            {
                sceneName = idsConCarga[i].sceneName;
                return true;
            }
        }
        sceneName = null;
        return false;
    }
}
