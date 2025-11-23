using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Ajustes")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private Vector3 localAxis = Vector3.up;
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private bool startOpen = false;

    [Header ("Picaporte")]

    [SerializeField] private GameObject PicaporteRef;
    [SerializeField] private Material PicaporteMat;
    [SerializeField] private Material PicaporteHighLight;


    public bool IsOpen { get; private set; }

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine rotatingCo;

    void Awake()
    {
        closedRotation = transform.localRotation;
        Vector3 axis = localAxis == Vector3.zero ? Vector3.up : localAxis.normalized;
        openRotation = closedRotation * Quaternion.AngleAxis(openAngle, axis);
        PicaporteRef.GetComponent<Renderer>().material = PicaporteHighLight;

        if (startOpen)
        {
            transform.localRotation = openRotation;
            IsOpen = true;
        }
        else
        {
            transform.localRotation = closedRotation;
            IsOpen = false;
        }
    }



    public void ToggleIsOpen()
    {
        SetOpen(!IsOpen);
    }

    public void SetOpen(bool open, bool instant = false)
    {
        if (IsOpen == open) return;

        if (rotatingCo != null) StopCoroutine(rotatingCo);

        Quaternion target = open ? openRotation : closedRotation;

        if (instant || duration <= 0f)
        {
            transform.localRotation = target;
            IsOpen = open;
        }
        else
        {
            rotatingCo = StartCoroutine(RotateTo(target, open));
        }
    }

    private System.Collections.IEnumerator RotateTo(Quaternion target, bool finalState)
    {
        Quaternion start = transform.localRotation;
        float t = 0f;

        Debug.Log("Comenzando rotación puerta " + name);

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localRotation = Quaternion.Slerp(start, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.localRotation = target;
        IsOpen = finalState;
        rotatingCo = null;

        if (IsOpen)
        {
            PicaporteRef.GetComponent<Renderer>().material = PicaporteMat;
            PicaporteRef.GetComponent<Collider>().enabled = false;
        }

        Debug.Log("Rotación terminada puerta " + name);

    }
}
