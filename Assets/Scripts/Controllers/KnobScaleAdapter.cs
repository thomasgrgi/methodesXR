using UnityEngine;

/// <summary>
/// Fait le lien entre un XR Knob (qui renvoie une valeur de 0 à 1) 
/// et le ScaleController (qui attend une valeur d'échelle absolue).
/// Respecte la règle d'ingénierie du TP : pas d'Update, piloté par l'événement.
/// </summary>
public class KnobScaleAdapter : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Référence injectée vers le contrôleur d'échelle global")]
    public ScaleController scaleController;

    [Header("Settings")]
    [Tooltip("Inverse le sens de lecture du Knob si nécessaire")]
    public bool invertValue = false;

    /// <summary>
    /// Cette méthode doit être appelée par l'événement "On Value Change" du XR Knob.
    /// </summary>
    /// <param name="normalizedKnobValue">Valeur entre 0 (min) et 1 (max)</param>
    public void ApplyKnobValueToScale(float normalizedKnobValue)
    {
        if (scaleController == null)
        {
            Debug.LogError("[BOOT] KnobScaleAdapter : ScaleController manquant !");
            return;
        }

        // Inversion si configurée dans l'inspecteur
        float val = invertValue ? 1f - normalizedKnobValue : normalizedKnobValue;

        // Interpolation (Lerp) pour passer de [0, 1] à [minScale, maxScale]
        float targetScale = Mathf.Lerp(scaleController.minScale, scaleController.maxScale, val);

        // On envoie au contrôleur (qui va gérer l'application et les logs [INPUT]/[XR])
        scaleController.SetScale(targetScale);
    }
}