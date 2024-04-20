using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public Sentence[] sentences;
    public List<SpriteExpression> expressionsSprites;

}

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    public string sentence;
    public Expression expression;
}

[System.Serializable]
public class SpriteExpression
{
    public Expression expression;
    public Sprite sprite;
}

public enum Expression { Neutral, Happy, Sad, Angry, Surprised, Scared, Confused }
