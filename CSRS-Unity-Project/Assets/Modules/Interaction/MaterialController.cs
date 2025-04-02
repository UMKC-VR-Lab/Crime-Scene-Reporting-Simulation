using UnityEngine;

namespace XRTools
{
    /// <summary>
    /// Manages material color changes on a GameObject's MeshRenderer.
    /// </summary>
    public class MaterialController : MonoBehaviour
    {
        [Tooltip("Enables debug logs when changing colors.")]
        public bool debug = false;

        [Tooltip("The GameObject containing the MeshRenderer.")]
        public GameObject visual;

        [Tooltip("Colors used for different interaction states.")]
        public Color deselectedColor, selectedColor, activatedColor;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            if (visual != null)
            {
                _meshRenderer = visual.GetComponent<MeshRenderer>();
            }
            else
            {
                Debug.LogWarning("Visual GameObject is not assigned.");
            }
        }

        /// <summary>
        /// Sets the color of the material attached to the MeshRenderer.
        /// </summary>
        /// <param name="color">The new color to apply to the material.</param>
        public void SetColor(Color color)
        {
            if (_meshRenderer != null && _meshRenderer.material != null)
            {
                _meshRenderer.material.color = color;
                if (debug) Debug.Log($"Set color to {color}");
            }
            else
            {
                Debug.LogWarning("MeshRenderer or Material is missing on the object.");
            }
        }

        // State-based color change methods
        public void SetDeselected() => SetColor(deselectedColor);
        public void SetSelected() => SetColor(selectedColor);
        public void SetActivated() => SetColor(activatedColor);

        // Predefined color change methods
        public void SetWhite() => SetColor(Color.white);
        public void SetGrey() => SetColor(Color.gray);
        public void SetBlack() => SetColor(Color.black);
        public void SetRed() => SetColor(Color.red);
        public void SetYellow() => SetColor(Color.yellow);
        public void SetGreen() => SetColor(Color.green);
        public void SetBlue() => SetColor(Color.blue);
        public void SetCyan() => SetColor(Color.cyan);
        public void SetMagenta() => SetColor(Color.magenta);
    }
}
