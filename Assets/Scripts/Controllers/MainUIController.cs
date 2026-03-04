using UnityEngine;
using TMPro;
using System;

public class MainUIController : MonoBehaviour
{
    [Header("UI Display")]
    public TextMeshProUGUI dateText;

    [Header("Dependencies")]
    private TimeModel timeModel;
    private ScaleController scaleController;
    private PlanetViewV2[] planets;
    private Transform xrOrigin;
    private Transform solarSystem;
    private Vector3 XRinitialOriginPos;
    private Quaternion XRinitialOriginRot;
    private Vector3 solarSystemInitialPos;
    private Quaternion solarSystemInitialRot;
    private GameObject trajectories;

    private bool orbitsVisible = true;

    public void Init(TimeModel model, ScaleController scaleCtrl, PlanetViewV2[] planetViews, Transform origin, GameObject trajectoriesObj, Transform solarSystemRoot)
    {
        timeModel = model;
        scaleController = scaleCtrl;
        planets = planetViews;
        xrOrigin = origin;
        solarSystem = solarSystemRoot;
        trajectories = trajectoriesObj;


        if (xrOrigin != null)
        {
            XRinitialOriginPos = xrOrigin.position;
            XRinitialOriginRot = xrOrigin.rotation;
        }

        if (solarSystem != null)
        {
            solarSystemInitialPos = solarSystem.position;
            solarSystemInitialRot = solarSystem.rotation;
        }

        // S'abonner au changement d'heure pour mettre à jour le texte
        timeModel.OnTimeChanged += UpdateDateDisplay;
    }

    private void UpdateDateDisplay(DateTime time)
    {
        if (dateText != null)
            dateText.text = time.ToString("dd/MM/yyyy HH:mm");
    }

    // --- Actions Temporelles ---

    public void TogglePlayPause()
    {
        if (timeModel.IsPlaying) timeModel.Pause();
        else timeModel.Play();
        Debug.Log("[INPUT] Play/Pause toggled: " + timeModel.IsPlaying);
    }

    public void SetTimeSpeed(float speed)
    {
        timeModel.SetScale(speed);
        Debug.Log("[INPUT] Time speed set to x" + speed);
    }

    // --- Actions Système Solaire ---

    public void ToggleOrbits()
    {
        orbitsVisible = !orbitsVisible;
        trajectories.SetActive(orbitsVisible);
        Debug.Log("[XR] Orbits visibility: " + orbitsVisible);
    }

    public void ResetScale()
    {
        scaleController.SetScale(1f);
    }

    public void ResetViewpoint()
    {
        if (xrOrigin != null)
        {
            xrOrigin.position = XRinitialOriginPos;
            xrOrigin.rotation = XRinitialOriginRot;
            Debug.Log("[XR] Viewpoint reset");
        }
    }

    public void ResetSolarSystemPosition()
    {
        if (solarSystem != null)
        {
            solarSystem.position = solarSystemInitialPos;
            solarSystem.rotation = solarSystemInitialRot;
            Debug.Log("[XR] Solar system position reset");
        }
    }
}