using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Gère exclusivement l'affichage des performances techniques (FPS)
/// et de l'état de la simulation (Date, Vitesse).
/// </summary>
public class VRLogOverlay : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Texte pour les statistiques (Header)")]
    public TextMeshProUGUI headerText;

    private TimeModel timeModel;
    private float deltaTime = 0.0f;

    public void Init(TimeModel model)
    {
        timeModel = model;
    }

    void Update()
    {
        if (headerText == null || timeModel == null) return;

        // Calcul fluide du FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        string status = timeModel.IsPlaying ? "PLAY" : "PAUSE";
        
        // Construction du texte formaté
        headerText.text = string.Format(
            "<color=#00FF00>PERF:</color> {0:0.} FPS ({1:0.0} ms)\t" +
            "<color=#00FFFF>DATE:</color> {2:dd/MM/yyyy HH:mm}\t" +
            "<color=#FFFF00>VITESSE:</color> x{3} [{4}]",
            fps, msec, timeModel.CurrentTime, timeModel.TimeScale, status
        );
    }
}