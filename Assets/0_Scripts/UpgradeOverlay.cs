using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOverlay : MonoBehaviour
{

    private static UpgradeOverlay Instance { get; set; }
    

    private void Awake()
    {
        Instance = this;
    }

    public static void ShowStatic(Tower tower)
    {
        Instance.Show(tower);
    }

    private void Show(Tower tower)
    {
        gameObject.SetActive(true);
        transform.position = tower.transform.position;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
