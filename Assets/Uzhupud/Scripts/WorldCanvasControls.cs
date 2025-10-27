using UnityEngine;
using UnityEngine.UI;

public class WorldCanvasControls : MonoBehaviour
{
    [Header("Rotacion")]
    public Transform targetToRotate;

    public Slider rotationSlider;

    public Vector3 rotationAxis = Vector3.up;

    public float minDegrees = 0f;
    public float maxDegrees = 360f;

    [Header("Pisos")]
    public Slider floorsSlider;

    public GameObject[] floors; // Debe tener 10 elementos
    public bool useLocalRotation = true;

    void Awake()
    {
        if (rotationSlider != null)
        {
            rotationSlider.minValue = 0f;
            rotationSlider.maxValue = 1f; 
            rotationSlider.onValueChanged.AddListener(OnRotationSliderChanged);
        }

        if (floorsSlider != null)
        {
            floorsSlider.minValue = 0f;     
            floorsSlider.maxValue = floors != null ? floors.Length : 10f;
            floorsSlider.wholeNumbers = true; 
            floorsSlider.onValueChanged.AddListener(OnFloorsSliderChanged);
        }
    }

    void Start()
    {
        if (rotationSlider != null) OnRotationSliderChanged(rotationSlider.value);
        if (floorsSlider != null) OnFloorsSliderChanged(floorsSlider.value);
    }

    private void OnRotationSliderChanged(float normalized)
    {
        if (targetToRotate == null) return;

        float degrees = Mathf.Lerp(minDegrees, maxDegrees, Mathf.Clamp01(normalized));
        Quaternion rot = Quaternion.AngleAxis(degrees, rotationAxis.normalized);

        if (useLocalRotation)
            targetToRotate.localRotation = rot;
        else
            targetToRotate.rotation = rot;
    }


    private void OnFloorsSliderChanged(float value)
    {
        if (floors == null || floors.Length == 0) return;

        int n = Mathf.Clamp(Mathf.RoundToInt(value), 0, floors.Length);

        for (int i = 0; i < floors.Length; i++)
        {
            if (floors[i] != null)
                floors[i].SetActive(i < n);
        }
    }

    public void SetRotationDegrees(float degrees)
    {
        float t = Mathf.InverseLerp(minDegrees, maxDegrees, degrees);
        if (rotationSlider != null) rotationSlider.value = t;
        OnRotationSliderChanged(t);
    }

    public void SetActiveFloors(int count)
    {
        int n = Mathf.Clamp(count, 0, floors != null ? floors.Length : 0);
        if (floorsSlider != null) floorsSlider.value = n;
        OnFloorsSliderChanged(n);
    }
}

