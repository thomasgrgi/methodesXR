using UnityEngine;
using System;

public class AppBootstrapper : MonoBehaviour
{
    public SolarSystemConfig config;

    public PlanetViewV2[] planets;
    public TimeController timeController;
    public ScaleController scaleController;
    [Header("UI & Debug")]
    public MainUIController mainUI;
    public VRLogOverlay logOverlay;
    public GameObject trajectoriesObj; // Container des trajectoires
    public Transform xrOrigin; // xr rig
    public Transform solarSystemTransform; // Racine du système solaire pour recentrer

    [Header("Focus System")]
    public FocusPanelView focusPanel;
    TimeModel timeModel;
    PlanetSystemController controller;
    PlanetSelectionSystem selectionSystem;
    FocusController focusController;

    void Start()
    {
        Debug.Log("[BOOT] Initializing application");

        timeModel = new TimeModel();

        var ephemeris = new PlanetEphemerisService();
        selectionSystem = new PlanetSelectionSystem();

        controller = new PlanetSystemController(
            timeModel,
            ephemeris,
            planets
        );

        focusController = new FocusController(
            selectionSystem,
            timeModel,
            focusPanel
        );

        foreach (var planetView in planets)
        {
            var selectable = planetView.GetComponent<PlanetSelectable>();
            if (selectable != null)
            {
                selectable.Init(selectionSystem);
            }
        }

        timeController.Init(timeModel);
        // scaleController.solarSystemRoot = scaleController.solarSystemRoot; // Déjà fait via inspecteur normalement
        mainUI.Init(timeModel, scaleController, planets, xrOrigin, trajectoriesObj, solarSystemTransform);
        logOverlay.Init(timeModel);
        focusPanel.ShowPanel(false);
        timeModel.SetTime(DateTime.Now);
        controller.UpdateTrajectories(DateTime.Now);
    }
}
