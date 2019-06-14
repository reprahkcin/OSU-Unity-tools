using System;
using UnityEngine;

public class MCSet : MonoBehaviour
{
    private MCAnswer answer1;
    private MCAnswer answer2;
    private MCAnswer answer3;
    private MCAnswer answer4;

    private MCQuestion question;
    
    public void CreateAnswer(MCAnswer whichAnswer, String answer, bool isCorrect, String reason)
    {
        whichAnswer.Answer = answer;
        whichAnswer.Reason = reason;
        whichAnswer.IsCorrect = isCorrect;
    }

    public void CreateQuestion(String question)
    {
        this.question.question = question;
    }
}
