using UnityEngine;
using TMPro; // Nécessite TextMeshPro
using System;

/// <summary>
/// Vue de l'interface du panneau de Focus.
/// Contient uniquement des références graphiques et des fonctions d'affichage.
/// </summary>
public class FocusPanelView : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panelRoot;
    public TextMeshProUGUI planetNameText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI timeText;

    public void ShowPanel(bool show)
    {
        if (panelRoot != null)
        {
            panelRoot.SetActive(show);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        
        // Optionnel : Faire regarder le panneau vers la caméra
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    public void UpdateInfo(string planetName, float distance, DateTime currentTime)
    {
        if (planetNameText != null) planetNameText.text = $"Planète : {planetName}";
        if (distanceText != null) distanceText.text = $"Distance : {distance:F2} UA";
        if (timeText != null) timeText.text = $"Date : {currentTime:dd/MM/yyyy}";
    }
}