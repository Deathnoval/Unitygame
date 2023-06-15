using UnityEngine.EventSystems;
using UnityEngine;

public class HoverCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float yOffset = 125f;
    public GameObject dropZone;
    public bool isChecked = false;

    void Start()
    {
        dropZone = GameObject.Find("Drop Zone");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.transform.IsChildOf(dropZone.transform))
        {
            return;
        }

        isChecked = true;
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
        transform.localPosition += new Vector3(0, yOffset, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.transform.IsChildOf(dropZone.transform))
        {
            return;
        }

        if (!isChecked)
        {
            return;
        }
        transform.localScale = new Vector3(1f, 1f, 0);
        transform.localPosition -= new Vector3(0, yOffset, 0);
        isChecked = false;
    }
}