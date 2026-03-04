using System;
using UnityEngine;

/// <summary>
/// Contrôleur responsable de la mise en avant de la planète.
/// Fait le lien entre la sélection, le temps, et l'affichage UI.
/// </summary>
public class FocusController
{
    private PlanetSelectionSystem selectionSystem;
    private TimeModel timeModel;
    private FocusPanelView focusPanel;

    private PlanetData.Planet? activePlanet;
    private Transform activePlanetTransform;

    public FocusController(PlanetSelectionSystem selectionSystem, TimeModel timeModel, FocusPanelView focusPanel)
    {
        this.selectionSystem = selectionSystem;
        this.timeModel = timeModel;
        this.focusPanel = focusPanel;

        // On écoute les changements
        this.selectionSystem.OnPlanetSelected += HandlePlanetSelected;
        this.timeModel.OnTimeChanged += HandleTimeChanged;
    }

    private void HandlePlanetSelected(PlanetData.Planet planet, Transform planetTransform)
    {
        Debug.Log($"[FOCUS] Mode focus activé sur : {planet}");
        activePlanet = planet;
        activePlanetTransform = planetTransform;
        
        focusPanel.ShowPanel(true);
        UpdatePanelUI();
    }

    private void HandleTimeChanged(DateTime time)
    {
        // Si une planète est focus, on met à jour son UI (qui suit sa position) quand le temps avance
        if (activePlanet.HasValue)
        {
            UpdatePanelUI();
        }
    }

    private void UpdatePanelUI()
    {
        if (!activePlanet.HasValue) return;

        // 1. Placement de l'UI : On le met un peu au-dessus de la planète sélectionnée
        // On évite ainsi d'utiliser Update() dans la Vue, la position suit l'événement de temps !
        focusPanel.SetPosition(activePlanetTransform.position + Vector3.up * 1f);

        // 2. Mise à jour des données (Magnitude donne la distance par rapport au soleil en local)
        float distance = activePlanetTransform.localPosition.magnitude;
        
        focusPanel.UpdateInfo(
            activePlanet.Value.ToString(),
            distance,
            timeModel.CurrentTime
        );
    }
}