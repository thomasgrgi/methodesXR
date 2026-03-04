using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Composant attaché à chaque planète. 
/// Rôle : Capter l'interaction XR et la transmettre au système, sans contenir de logique UI.
/// </summary>
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class PlanetSelectable : MonoBehaviour
{
    public PlanetViewV2 planetView;
    
    private PlanetSelectionSystem selectionSystem;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    // Injecté par le Bootstrapper
    public void Init(PlanetSelectionSystem system)
    {
        selectionSystem = system;
    }

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        // S'abonne à l'événement de sélection du XR Interaction Toolkit
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (selectionSystem != null && planetView != null)
        {
            Debug.Log($"[INPUT] Planète pointée via XR : {planetView.planet}");
            selectionSystem.SelectPlanet(planetView.planet, planetView.transform);
        }
    }
}