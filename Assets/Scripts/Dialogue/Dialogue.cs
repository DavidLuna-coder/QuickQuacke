using System.Collections;
using System.Collections.Generic;
using uLipSync;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public Sentence[] sentences;
    public List<SpriteExpression> expressionsSprites = new List<SpriteExpression>();
    public uLipSyncTexture uLipSyncTexture;
    public Material mouthSprite;
}

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    public string sentence;
    public Expression expression;
    public AudioClip audio;
}

[System.Serializable]
public class SpriteExpression
{
    public Expression expression;
    public Sprite sprite;
}

public enum Expression { Neutral, Angry, Surprised, Worried }
