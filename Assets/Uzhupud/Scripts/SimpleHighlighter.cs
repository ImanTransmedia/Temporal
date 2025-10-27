using UnityEngine;

[DisallowMultipleComponent]
public class SimpleHighlighter : MonoBehaviour
{
    [Header("Visual de highlight (prende/apaga)")]
    [Tooltip("Puede ser un objeto hijo con malla de contorno, un halo, un outline script, etc.")]
    public GameObject highlightVisual;

    [Header("Opcional: Emisión")]
    public Renderer[] targetRenderers;
    public string emissionProperty = "_EMISSION";
    public bool toggleEmission = false;

    public bool IsOn { get; private set; }

    public void SetHighlight(bool on)
    {
        IsOn = on;

        if (highlightVisual != null)
            highlightVisual.SetActive(on);

        if (toggleEmission && targetRenderers != null)
        {
            foreach (var r in targetRenderers)
            {
                if (r == null) continue;
                foreach (var mat in r.materials)
                {
                    if (on)
                        mat.EnableKeyword(emissionProperty);
                    else
                        mat.DisableKeyword(emissionProperty);
                }
            }
        }
    }

    private void Reset()
    {
        if (highlightVisual == null && transform.childCount > 0)
            highlightVisual = transform.GetChild(0).gameObject;
    }
}
