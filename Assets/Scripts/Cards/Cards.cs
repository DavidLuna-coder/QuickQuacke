using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Cards : MonoBehaviour
{
    [SerializeField] private Button cardBackButton;
    [SerializeField] private Image cardBackBackground;
    [SerializeField] private Image cardFrontBackground;
    [SerializeField] private Image cardFrontImage;

    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;
    private bool objectMatch;

    [Header("DoTween Animation")]
    [SerializeField] private Vector3 selectRotation = new Vector3();
    [SerializeField] private Vector3 doSelectRotation = new Vector3();
    [SerializeField] private float duration = 0.25f;
    private Tweener[] tweener =  new Tweener[3];

    private CardGroup cardGroup;

    private void Awake()
    {
        if(cardGroup == null)
        {
            cardGroup = transform.parent.GetComponent<CardGroup>();
        }
       
        if(cardGroup != null)
        {
          cardGroup.Subscribe(this);
        }
        
    }

    private void Start()
    {
        cardBackButton.onClick.AddListener(OnClick);
        StartCoroutine(WaitingtoHide());
        MemoryManager.Instance.Subscribe(this);
    }

    private void OnClick()
    {
        cardGroup.OnCardSelected(this);
    }

    private IEnumerator WaitingtoHide()
    {
        yield return new WaitForSeconds(3.0f);
        tweener[2] = transform.DORotate(doSelectRotation, duration).SetEase(Ease.InOutElastic).OnUpdate(CheckWaitingToHide);
    }

    private void CheckWaitingToHide()
    {
        float elapsed = tweener[2].Elapsed();
        float halfDuration = tweener[2].Duration()/2f;
        if(elapsed >= halfDuration)
        {
            cardBack.SetActive(true);
            cardFront.SetActive(false);
        }
    }
    public void Select()
    {
        tweener[0] = transform.DORotate(selectRotation, duration).SetEase(Ease.InOutElastic).OnUpdate(CheckSelectHalfDuration);
    }

    public void Deselect()
    {
        tweener[1] = transform.DORotate(doSelectRotation, duration).SetEase(Ease.InOutElastic).OnUpdate(CheckDeSelectHalfDuration);
    }

    private void CheckSelectHalfDuration()
    {
        float elapsed = tweener[0].Elapsed();
        float halfDuration = tweener[0].Duration()/2f;
        if(elapsed >= halfDuration)
        {
            cardBack.SetActive(false);
            cardFront.SetActive(true);
        }
    }

    private void CheckDeSelectHalfDuration()
    {
        float elapsed = tweener[0].Elapsed();
        float halfDuration = tweener[0].Duration()/2f;
        if(elapsed >= halfDuration)
        {
            cardBack.SetActive(true);
            cardFront.SetActive(false);
        }
    }

    public Image GetCardFrontBackground()
    {
        return cardFrontBackground;
    }

    public Image GetCardBackBackground()
    {
        return cardBackBackground;
    }
    public void DisableCardBackButton()
    {
        cardBackButton.interactable = false;
    }

    public void SetObjectMatch()
    {
        objectMatch = true;
    }

    public bool GetObjectMatch()
    {
        return objectMatch;
    }

    public void SetCardImage(Sprite sprite)
    {
        
        cardFrontImage.sprite = sprite;
        Debug.Log(cardFrontImage.sprite);
    }
}
