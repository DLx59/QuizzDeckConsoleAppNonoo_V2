using QuizDeck_V2_Librairie;

namespace QuizDeck_V2_UI
{
    public static class Program
    {
        private static Quizz _quizz;

        public static void Main(string[] args)
        {
            _quizz = new Quizz();
            _quizz.QuestionDisplayed += OnQuestionDisplayed;
            _quizz.AnswerReceived += OnAnswerReceived;
            _quizz.Start();
        }

        private static void OnQuestionDisplayed(object? sender, string question)
        {
            Console.WriteLine(question);

            var currentCard = _quizz.GetCurrentCard();
            var i = 0;
            foreach (var answer in currentCard.Answers)
            {
                Console.WriteLine($"{i + 1}) {answer}");
                i++;
            }

            var validAnswers = Enumerable.Range(1, currentCard.Answers.Count).ToList();
            int[] userAnswers;

            while (true)
            {
                Console.Write($"Votre réponse (séparez les numéros par des virgules) : ");
                var input = Console.ReadLine();

                if (TryParseUserAnswers(input!, out userAnswers, validAnswers.Count))
                {
                    break;
                }

                Console.WriteLine("Réponse invalide. Essayez à nouveau.");
            }

            _quizz.ReceiveAnswer(userAnswers);
        }

        private static bool TryParseUserAnswers(string input, out int[] userAnswers, int validAnswersCount)
        {
            var answersArray = input?.Split(',')
                .Select(x => x.Trim())
                .Where(x => int.TryParse(x, out var num) && num > 0 && num <= validAnswersCount)
                .Select(int.Parse)
                .Distinct()
                .OrderBy(x => x)
                .ToArray();

            if (answersArray!.Length == 0)
            {
                userAnswers = null!;
                return false;
            }

            userAnswers = answersArray;
            return true;
        }

        private static void OnAnswerReceived(object? sender, int result)
        {
            if (result == 1)
            {
                Console.WriteLine("Correct!\n");
            }
            else
            {
                var currentCard = _quizz.GetCurrentCard();
                var correctAnswers = currentCard.CorrectAnswers.Select(x => currentCard.Answers[x - 1]).ToList();
                Console.WriteLine($"Faux! Les réponses correctes étaient : {string.Join(", ", correctAnswers)}\n");
            }
        }
    }
}