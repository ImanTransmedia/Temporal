using UnityEngine;

public class ControlDiaNoche : MonoBehaviour
{
    public Material materialDia;
    public Material materialNoche;
    public Renderer objetoRenderer;
    public bool usarSkybox = true;
    public Light luzPrincipal;
    public Color colorLuzDia = Color.white;
    public Color colorLuzNoche = new Color(0.1f, 0.1f, 0.3f);

    public void SetModo(bool esDia)
    {
        if (esDia)
        {
            if (usarSkybox && materialDia != null)
                RenderSettings.skybox = materialDia;
            if (objetoRenderer != null && materialDia != null)
                objetoRenderer.material = materialDia;
            if (luzPrincipal != null)
            {
                luzPrincipal.color = colorLuzDia;
                luzPrincipal.intensity = 1.2f;
            }
        }
        else
        {
            if (usarSkybox && materialNoche != null)
                RenderSettings.skybox = materialNoche;
            if (objetoRenderer != null && materialNoche != null)
                objetoRenderer.material = materialNoche;
            if (luzPrincipal != null)
            {
                luzPrincipal.color = colorLuzNoche;
                luzPrincipal.intensity = 0.3f;
            }
        }
    }
}
