using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardGrid : MonoBehaviour
{
    [System.Serializable]
    public class Card{
        public string cardName;
        public Sprite cardImage;
    }

    [SerializeField] private List<Card> cardList = new List<Card>();
    [SerializeField] private List<Card> cardListtoSort = new List<Card>();
    [SerializeField] private Transform container;
    [SerializeField] private Transform cardPrefab;

    private void Start()
    {
        cardPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        FillGrid();
    }

    private void FillGrid()
    {
        int cardsToShow = 0;
        System.Random dif = new System.Random();
        int difficulty = dif.Next(1, 4);
        switch(difficulty)
        {
            case 1: cardsToShow = 4; break;
            case 2: cardsToShow = 5; break;
            case 3: cardsToShow = 7; break;
            default : break;
        }

        for (int i = 0; i < cardsToShow; i++)
        {
            cardListtoSort.Add(cardList[i]);
            cardListtoSort.Add(cardList[i]);
        }

        System.Random rnd = new System.Random();
        IOrderedEnumerable<Card> randomized = cardListtoSort.OrderBy(i => rnd.Next());
        foreach(Card card in randomized)
        {
            Transform cardTransform = Instantiate(cardPrefab, container);
            cardTransform.gameObject.SetActive(true);
            cardTransform.name = card.cardName;
            Debug.Log(cardTransform.GetComponent<Cards>());
            cardTransform.GetComponent<Cards>().SetCardImage(card.cardImage);
            
        }
    }

}
