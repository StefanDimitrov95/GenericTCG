using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo;
    public bool cardPlayed = false;
    public Card currentCard;
    public bool isBeingDragged = false;

    private GameObject placeholder;
    private PlayerHand hand;

    public void ReturnCardToHand()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        OnEndDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isBeingDragged = true;
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);

        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        CardScaling.UpscaleCard(this);

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(parentToReturnTo.parent);

        AnimateRowOnBeginDrag();

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (placeholder == null || !isBeingDragged)
        {
            return;
        }

        CardScaling.DownscaleCard(this);
        isBeingDragged = false;

        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        Destroy(placeholder);
        AnimateRowOnEndDrag();

        if (cardPlayed)
        {
            hand = GameObject.Find("Hand").GetComponent<PlayerHand>();
            hand.CardsInHand.Remove(currentCard);
            hand.UpdateHandLabel();
            Destroy(this);
            if (currentCard is MagicCard)
            {
                (currentCard as MagicCard).OnPlay();
            }
        }
    }

    private void AnimateRowOnBeginDrag()
    {
        currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.enabled = true;
        currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.Play("OnBeginDrag");
    }

    private void AnimateRowOnEndDrag()
    {
        currentCard.ToRow.currentRow.CurrentRow.GetComponent<DropZone>().animator.Play("OnEndDrag");
    }
}