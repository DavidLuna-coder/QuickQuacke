using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardGroup : MonoBehaviour
{
    [SerializeField] private List<Cards> cardList = new List<Cards>();
    [SerializeField] private List<Cards> selectedCardList = new List<Cards>();

    [SerializeField] private Sprite cardIdle;
    [SerializeField] private Sprite cardActive;

    public event EventHandler OnCardMatch;

    public void Subscribe(Cards card)
    {
        if(cardList == null)
        {
            cardList = new List<Cards>();
        }

        if(!cardList.Contains(card))
        {
            cardList.Add(card);
        }
    }

    public void OnCardSelected(Cards card)
    {
        selectedCardList.Add(card);
        card.Select();
        card.GetCardFrontBackground().sprite = cardActive;
        if(selectedCardList.Count == 2)
        {
           if(CheckIfMatch())
           {
               foreach(Cards selectedCard in selectedCardList)
               {
                   card.DisableCardBackButton();
                   card.SetObjectMatch();
                   selectedCardList = new List<Cards>();
                   OnCardMatch?.Invoke(this, EventArgs.Empty);
               }
           }

           else
           {
               StartCoroutine(DontMatch());
           }
           
        }
    }

    public void ResetTabs()
    {
        foreach(Cards card in selectedCardList)
        {
            if(selectedCardList != null && selectedCardList.Count < 3) continue;
            card.GetCardBackBackground().sprite = cardIdle;
        }
    }

    private IEnumerator DontMatch()
    {
        yield return new WaitForSeconds(0.5f);
        foreach(Cards card in selectedCardList)
        {
            card.Deselect();
            selectedCardList = new List<Cards>();
        }
    }

    private bool CheckIfMatch()
    {
        Cards firstCard = selectedCardList[0];
        foreach(Cards card in selectedCardList)
        {
            if(card.name != firstCard.name)
            {
                return false;
            }
            
        }
        return true;
    }

    
}
