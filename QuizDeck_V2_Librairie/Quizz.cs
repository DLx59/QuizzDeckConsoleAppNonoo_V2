using QuizDeck_V2_Librairie.Models;

namespace QuizDeck_V2_Librairie
{
    public class Quizz
    {
        private readonly Stack<Card> _deck;
        private int _currentQuestionIndex;

        public event EventHandler<string>? QuestionDisplayed;
        public event EventHandler<int>? AnswerReceived;

        public Quizz()
        {
            _deck = CreateDeck();
            _currentQuestionIndex = 0;
        }

        public void Start()
        {
            DisplayNextQuestion();
        }

        private void DisplayNextQuestion()
        {
            if (HasNextQuestion())
            {
                var currentCard = _deck.ElementAt(_currentQuestionIndex);
                QuestionDisplayed?.Invoke(this, currentCard.Question);
            }
            else
                OnQuizzFinished();
        }

        public void ReceiveAnswer(int[] answers)
        {
            var currentCard = _deck.ElementAt(_currentQuestionIndex);
            AnswerReceived?.Invoke(this,
                answers.OrderBy(x => x).SequenceEqual(currentCard.CorrectAnswers.OrderBy(x => x)) ? 1 : 0);
            _currentQuestionIndex++;
            DisplayNextQuestion();
        }

        public Card GetCurrentCard()
        {
            return _deck.ElementAt(_currentQuestionIndex);
        }

        private bool HasNextQuestion()
        {
            return _currentQuestionIndex < _deck.Count;
        }

        private static Stack<Card> CreateDeck()
        {
            var deck = new Stack<Card>();
            deck.Push(new Card
            {
                Question = "Quel célèbre égyptologue a découvert le tombeau de Toutânkhamon ?",
                Answers = new List<string>
                    { "Jacques Cartier", "Howard Carter", "Marco Polo", "Napoléon Bonaparte" },
                CorrectAnswers = new List<int> { 2 }
            });
            deck.Push(new Card
            {
                Question = "Quelle est la plus grande chaîne de montagnes du monde ?",
                Answers = new List<string>
                    { "Les Andes", "Les Alpes", "Les Rocheuses", "L'Himalaya", "Les Montagnes de l'Atlas" },
                CorrectAnswers = new List<int> { 4 }
            });
            deck.Push(new Card
            {
                Question = "Quel écrivain français a écrit \"Les Misérables\" ?",
                Answers = new List<string> { "Victor Hugo", "Gustave Flaubert", "Marcel Proust", "Albert Camus" },
                CorrectAnswers = new List<int> { 1 }
            });
            deck.Push(new Card
            {
                Question = "Quels sont les deux pays les plus peuplés au monde en 2021 ?",
                Answers = new List<string> { "Inde", "États-Unis", "Brésil", "Chine", "Russie", "Nigéria" },
                CorrectAnswers = new List<int> { 1, 4 }
            });
            return deck;
        }

        private static void OnQuizzFinished()
        {
            Console.WriteLine("Fin du quizz.");
        }
    }
}