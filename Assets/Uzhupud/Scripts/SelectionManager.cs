using UnityEngine;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
    [Header("Prefab del menú por defecto")]
    public GameObject defaultMenuPrefab;

    [Header("Menús específicos por ID (opcional)")]
    public List<MenuById> menusById = new List<MenuById>();

    private Dictionary<int, GameObject> menuMap = new Dictionary<int, GameObject>();
    private SelectableItem currentItem;
    private GameObject currentMenuInstance;

    private void OnEnable()
    {
        SelectableItem.OnItemSelected += HandleItemSelected;

        // Crear mapa de menús
        menuMap.Clear();
        foreach (var entry in menusById)
        {
            if (entry != null && entry.prefab != null)
                menuMap[entry.id] = entry.prefab;
        }
    }

    private void OnDisable()
    {
        SelectableItem.OnItemSelected -= HandleItemSelected;
    }

    private void HandleItemSelected(int id, SelectableItem item)
    {
        if (currentItem == item)
        {
            DeselectCurrent();
            return;
        }

        DeselectCurrent();

        currentItem = item;
        currentItem.SetHighlight(true);

        ShowMenuFor(id, item);
    }

    private void DeselectCurrent()
    {
        if (currentItem != null)
        {
            currentItem.SetHighlight(false);
            currentItem = null;
        }

        if (currentMenuInstance != null)
        {
            Destroy(currentMenuInstance);
            currentMenuInstance = null;
        }
    }

    private void ShowMenuFor(int id, SelectableItem item)
    {
        if (item == null) return;

        GameObject prefab = menuMap.ContainsKey(id) ? menuMap[id] : defaultMenuPrefab;
        if (prefab == null) return;

        currentMenuInstance = Instantiate(prefab);
        currentMenuInstance.transform.position = item.transform.position + Vector3.up * 0.5f;
        currentMenuInstance.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    [System.Serializable]
    public class MenuById
    {
        public int id;
        public GameObject prefab;
    }
}
