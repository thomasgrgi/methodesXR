using System;
using UnityEngine;

/// <summary>
/// Système central (non MonoBehaviour) qui garde en mémoire la planète active
/// et alerte les contrôleurs intéressés (comme le FocusController).
/// </summary>
public class PlanetSelectionSystem
{
    // Événement déclenché avec la planète et son Transform (pour placer l'UI)
    public event Action<PlanetData.Planet, Transform> OnPlanetSelected;

    public void SelectPlanet(PlanetData.Planet planet, Transform planetTransform)
    {
        OnPlanetSelected?.Invoke(planet, planetTransform);
    }
}