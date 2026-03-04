using UnityEngine;

/// <summary>
/// Contrôleur responsable de modifier la taille du système solaire.
/// Respecte l'architecture du TP : aucune logique dans Update, 
/// déclenché uniquement par des événements externes (via l'adaptateur).
/// </summary>
public class ScaleController : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Glissez ici l'objet racine (SolarSystemRoot)")]
    public Transform solarSystemRoot;

    [Header("Scale limits")]
    public float minScale = 0.1f;
    public float maxScale = 10f;

    private float currentScale = 1f;

    /// <summary>
    /// Applique une nouvelle échelle absolue au système solaire.
    /// </summary>
    /// <param name="value">Valeur d'échelle cible</param>
    public void SetScale(float value)
    {
        Debug.Log("[INPUT] Scale requested: " + value);

        // On contraint la valeur entre le min et le max
        float clamped = Mathf.Clamp(value, minScale, maxScale);
        
        if (!Mathf.Approximately(clamped, value))
        {
            Debug.LogWarning("[WARN] Scale clamped from " + value + " to " + clamped);
        }

        currentScale = clamped;

        // On applique la transformation si la cible est bien assignée
        if (solarSystemRoot != null)
        {
            solarSystemRoot.localScale = Vector3.one * currentScale;
            Debug.Log("[XR] Scale applied: " + currentScale);
        }
        else
        {
            Debug.LogError("[WARN] ScaleController : La cible 'solarSystemRoot' n'est pas assignée !");
        }
    }
}