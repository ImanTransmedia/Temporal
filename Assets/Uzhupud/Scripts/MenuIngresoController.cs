// MenuIngresoController.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuIngresoController : MonoBehaviour
{
    public Image imgPiso;
    public TMP_Text txtNombre;
    public TMP_Text txtLevel;
    public GameObject botonObj;
    public GameObject buttonEnter;

    public Transform referenciaOrientacion;
    public bool autoOrientar = true;

    string escenaObjetivo;

    void OnEnable()
    {
        VRSeleccionManager.OnUIData += OnData;
    }

    void OnDisable()
    {
        VRSeleccionManager.OnUIData -= OnData;
    }

    void Update()
    {
        if (!autoOrientar || referenciaOrientacion == null) return;

        Vector3 dir = referenciaOrientacion.forward;
        float ang = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        if (ang < 0) ang += 360f;

        int sector = Mathf.RoundToInt(ang / 90f) % 4;
        float snapY = sector * 90f;
        var e = transform.eulerAngles;
        e.y = snapY;
        transform.eulerAngles = e;
    }

    void OnData(PisoUIData data)
    {
        if (data == null)
        {
            if (imgPiso != null) imgPiso.sprite = null;
            if (txtNombre != null) txtNombre.text = "";
            if (txtLevel != null) txtLevel.text = "";
            if (botonObj != null) botonObj.SetActive(false);
            escenaObjetivo = "";
            return;
        }

        if (imgPiso != null) imgPiso.sprite = data.imagen;
        if (txtNombre != null) txtNombre.text = data.nombre;
        if (txtLevel != null) txtLevel.text = "Nivel " + data.level;

        if (botonObj != null) botonObj.SetActive(data.ingresable);
        escenaObjetivo = data.ingresable ? data.sceneName : "";
    }

    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(escenaObjetivo))
            SceneManager.LoadScene(escenaObjetivo);
    }
}
