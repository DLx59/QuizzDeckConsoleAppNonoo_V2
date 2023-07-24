namespace QuizDeck_V2_Librairie.Models;

public class Card
{
    public string Question { get; set; }
    public List<string> Answers { get; set; }
    public List<int> CorrectAnswers { get; set; }
}