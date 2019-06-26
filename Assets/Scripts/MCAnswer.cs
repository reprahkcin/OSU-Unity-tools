using System;
using UnityEngine;

public class MCAnswer
{
    public Guid Id { get; }
    public string value;
    public bool isCorrect;
    public string reason;

    public MCAnswer()
    {
    }
    public MCAnswer(string value, bool isCorrect, string reason)
    {
        this.Id = Guid.NewGuid();
        this.value = value;
        this.isCorrect = isCorrect;
        this.reason = reason;
    }
}
