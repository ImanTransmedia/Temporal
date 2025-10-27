using UnityEngine;

public class ControlRotacion : MonoBehaviour
{
    public Transform objeto;
    public Vector3 eje = Vector3.up;

    public void Rotar(float valor)
    {
        if (objeto != null)
            objeto.localRotation = Quaternion.Euler(eje * valor);
    }
}
