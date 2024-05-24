using UnityEngine;

public class OutlineSelection : MonoBehaviour
{
    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private Color selectionColor = Color.blue;
    [SerializeField] private float highlightOutlineWidth = 1.0f;
    [SerializeField] private float selectionOutlineWidth = 3.0f;
    [SerializeField] private float doubleClickTime = 0.3f; // Tiempo máximo entre clics para considerar doble clic

    private Transform highlight;
    private Transform currentSelection;
    private RaycastHit raycastHit;
    private float lastClickTime;
    private bool isDoubleClick;

    void Update()
    {
        HandleHighlight();
        HandleSelection();
    }

    void HandleHighlight()
    {
        // Desactivar el outline del objeto previamente resaltado si no es el seleccionado
        if (highlight != null && highlight != currentSelection)
        {
            Outline outline = highlight.gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
            highlight = null;
        }

        // Realizar el raycast para detectar el objeto bajo el cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != currentSelection)
            {
                Outline outline = highlight.gameObject.GetComponent<Outline>();
                if (outline == null)
                {
                    outline = highlight.gameObject.AddComponent<Outline>();
                }
                outline.OutlineColor = highlightColor;
                outline.OutlineWidth = highlightOutlineWidth;
                outline.enabled = true;
            }
            else
            {
                highlight = null;
            }
        }
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickTime)
            {
                isDoubleClick = true;
            }
            else
            {
                isDoubleClick = false;
            }

            lastClickTime = Time.time;

            if (isDoubleClick)
            {
                // Si hay un objeto resaltado, cambiar la selección
                if (highlight)
                {
                    // Restablecer el outline del objeto previamente seleccionado
                    if (currentSelection != null && currentSelection != highlight)
                    {
                        ResetSelection(currentSelection);
                    }

                    // Asignar el nuevo objeto seleccionado
                    currentSelection = highlight;
                    Outline outline = currentSelection.gameObject.GetComponent<Outline>();
                    if (outline == null)
                    {
                        outline = currentSelection.gameObject.AddComponent<Outline>();
                    }
                    outline.OutlineColor = selectionColor;
                    outline.OutlineWidth = selectionOutlineWidth;
                    outline.enabled = true;

                    highlight = null;
                }
            }
        }
    }

    void ResetSelection(Transform selection)
    {
        if (selection != null)
        {
            Outline outline = selection.gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
                // Eliminar el componente Outline para evitar conflictos
                Destroy(outline);
            }
        }
    }
}
