using System;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace Quize
{
public static class Quize
    {
        public static void Main()
        {
            Greatings();
        }
        public static void Greatings()
        {
            Console.WriteLine("Welcome to the Quize game");
            Options();
        }
        public static void Options()
        {
            Console.WriteLine("Choose options:");
            Console.WriteLine("1 Take part in quize");
            Console.WriteLine("2 Manage quizez");
            Console.WriteLine("3 Exit");
            int option = Convert.ToInt32(Console.ReadLine());
            try
            {
                ChooseOptions(option);
            } catch
            {
                PressAnyButton("This is not a number");
                Options();
            }
        }
        public static void QuizeOptions()
        {
            Console.WriteLine("Choose option:");
            Console.WriteLine("1 Create new quize");
            Console.WriteLine("2 Delete quize");
            Console.WriteLine("3 Go to the main");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                ChooseQuizeOptions(option);
            }
            catch
            {
                PressAnyButton("This is not a number");
                QuizeOptions();
            }
        }
        public static void ChooseQuizeOptions(int option)
        {
            if(option < 1 || option > 3)
            {
                PressAnyButton("This is not an option");
                QuizeOptions();
            }
            if (option == 1)
            {
                CreateQuize();
            }
                if (option == 2)
                {
                try
                {
                    DeleteQuestion();
                } catch
                {
                    PressAnyButton("You don't have list");
                    Options();
                }
                }
                    if (option == 3)
                    {
                        Options();
                    }
        }
        public static void CreateQuize()
        {
            Console.WriteLine("Create quize");
            Console.WriteLine("Give quize a name");
            string QuizeName = Console.ReadLine();
            bool doYouWantTo = true;
            List<Question> questions = new List<Question>() ;
            while(doYouWantTo)
            {
               Question Question = CreateQusetion();
                questions.Add(Question);
                Console.WriteLine("Do you wan't to make another question Y/N");
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "Yes") doYouWantTo = true;
                else if (answer == "N" || answer == "No") doYouWantTo = false;
                else
                {
                    PressAnyButton("Invalid answer");
                    Options();
                }
            }
            QuizeClass quize = QuizeJson.CreateQuize(QuizeName, questions);
            QuizeJson.addQuize(quize);
            PressAnyButton("Added your quize");
            Options();
        }
        public static void DeleteQuestion()
        {
            try
            {
                ShowQuizes();
                Console.WriteLine("Choose quize to delete:");
                int index = Convert.ToInt32(Console.ReadLine());
                QuizeJson.DeleteQuize(index - 1);
                Console.WriteLine("Deleted your task");
                Options();
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                PressAnyButton("Nothing to delete");
                Options();
            }
        }
        public static Question CreateQusetion()
        {
            Console.WriteLine("Give me question:");
            string question = Console.ReadLine();
            Console.WriteLine("Give me right answer");
            string answer = Console.ReadLine();
            Question Question = QuizeJson.CreateQuestion(question, answer);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Give me some false answer");
                string variant = Console.ReadLine();
                QuizeJson.AddVariant(variant, Question);
            }
            return Question;
        }
        public static void ShowQuizes()
        {
            List<QuizeClass> quizes = QuizeJson.getList();
            for(int i = 0;i<quizes.Count;i++)
            {
                Console.WriteLine($"{i+1} {quizes[i].name}");
            }
        }
        public static void ChooseOptions(int option)
        {
            if(option < 1 || option > 3)
            {
                PressAnyButton("This is not an option");
                Options();
            }
            if (option == 1)
            {
                try
                {
                    
                    ShowQuizes();
                    Console.WriteLine("Chose Quize:");
                    int index = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        List<int> answerMap = PrintChosedQuize(index);
                        showHowMuchRight(answerMap);
                        Options();
                    } catch
                    {
                        PressAnyButton("This was not an option");
                        ChooseOptions(option);
                    }
                } catch
                {
                    PressAnyButton("You don't have quizes");
                    Options();
                }
            }
            if (option == 2)
            {
                QuizeOptions();
                try
                {
                    QuizeOptions();
                }
                catch
                {
                    PressAnyButton("This is not an option");
                    ChooseOptions(option);
                }
            }
            if (option == 3) {
                PressAnyButton("Closing");
                Environment.Exit(0);
            }
        }
        public static List<int> PrintChosedQuize(int index)
        {
            List<Question> quize = QuizeJson.getQuize(index-1);
            List<int> answerMap = new List<int>();
            foreach(Question el in quize)
            {
                Console.WriteLine(el.name);
                for(int i = 0;i<el.variants.Count;i++)
                {
                    Console.WriteLine($"{i + 1} {el.variants[i]}");
                }
                Console.WriteLine("Choose answer:");
                try
                {
                    int IndexOfAnswer = Convert.ToInt32(Console.ReadLine());
                    bool isRight = el.isAnswerRight(el.variants[IndexOfAnswer]);
                    if (isRight) answerMap.Add(1);
                    else answerMap.Add(0);
                } catch
                {
                    PressAnyButton("This was bad option");
                    Options();
                }
            }
            return answerMap;
        }
        public static void showHowMuchRight(List<int> answerMap)
        {
            List<int> Right = answerMap.Where(el => el == 1)
                .ToList();
            PressAnyButton($"You get {Right.Count} of {answerMap.Count}");
        }
        public static void PressAnyButton(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press any button");
            Console.ReadLine();
        }
    }
}