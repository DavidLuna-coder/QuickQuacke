using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance {get; private set;}

    [SerializeField] private CardGroup cardGroup;
    [SerializeField] private List<Cards> cardsList = new List<Cards>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cardGroup.OnCardMatch += CardGroup_OnCardMatch;
    }

    private void CardGroup_OnCardMatch(object sender, System.EventArgs e)
    {
        /*bool allCardsMatch = true;
        foreach (var card in cardsList)
        {
            if (!card.GetObjectMatch())
            {
                allCardsMatch = false;
                break;
            }
         }
        if (allCardsMatch)
        {
            StartCoroutine(OnCompleteGame());
        }*/
        if (cardsList.All(x => x.GetObjectMatch()))
        {
            Debug.Log("GANAMOOOOOOOOOOOOOOOOOOOOS"); 
            StartCoroutine(OnCompleteGame());
        }
    }

    private IEnumerator OnCompleteGame()
    {
        yield return new WaitForSeconds(0.75f);
        Debug.Log("GANAMOOOOOOOOOOOOOOOOOOOOS");   
    }

    public void Subscribe(Cards card)
    {
        if (cardsList == null)
        {
            cardsList = new List<Cards>();
        }

        if (!cardsList.Contains(card))
        {
            cardsList.Add(card);
        }
    }

}
