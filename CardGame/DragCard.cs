
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    GameObject placeholder = null;
    public List<HoverCard> hoverCards = new List<HoverCard>();
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1f, 1f, 0);
        hoverCards = FindObjectsOfType<HoverCard>().ToList();
        foreach (HoverCard hoverCard in hoverCards)
        {
            hoverCard.enabled = false;
        }
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.flexibleWidth = this.GetComponent<LayoutElement>().flexibleWidth;
        le.flexibleHeight = this.GetComponent<LayoutElement>().flexibleHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
        transform.localScale = new Vector3(1f, 1f, 0);
        foreach (HoverCard hoverCard in hoverCards)
        {
            hoverCard.enabled = true;
            hoverCard.isChecked = false;
        }
    }
}
