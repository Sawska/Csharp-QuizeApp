using System;
using System.Text.Json;
namespace Quize
{
	public static class QuizeJson
	{
		public static List<QuizeClass> getList()
		{
                string json = File.ReadAllText("Quizes.json");
                List<QuizeClass> questions = JsonSerializer.Deserialize<List<QuizeClass>>(json);
                return questions;
            
		}
		public static List<Question> getQuize(int index)
		{
			List<QuizeClass> quizes = getList();
			return quizes[index].questions;
		}
		public static Question CreateQuestion(string question, string answer)
		{
			Question obj = new Question(question, answer);
			obj.SetVariant(answer);
			return obj;
		}
		public static void AddVariant(string variant,Question question)
		{
			question.SetVariant(variant);
		}
		public static QuizeClass CreateQuize(string name,List<Question> questions)
		{
			return new QuizeClass(name, questions);
		}
		public static void addQuize(QuizeClass quize)
		{
			try
			{
                List<QuizeClass> quizes = getList();
                quizes.Add(quize);
                File.WriteAllText("Quizes.json", JsonSerializer.Serialize(quizes));
            } catch
			{
				List<QuizeClass> quizes = new List<QuizeClass>();
				quizes.Add(quize);
                File.WriteAllText("Quizes.json", JsonSerializer.Serialize(quizes));
            }
        }
		public static void DeleteQuize(int index)
		{
			List<QuizeClass> quizes = getList();
			if(index < 0 || index > quizes.Count)
			{
				Console.WriteLine("This is  not an option");
				Quize.DeleteQuestion();
			}
			quizes = quizes.Where((quize, Index) => index != Index)
			.ToList();
			File.WriteAllText("Quizes.json", JsonSerializer.Serialize(quizes));
		}
	}
	public class Question
	{
		public string name { get; set; }
		public string answer { get; set; }
		public List<string> variants { get; set; }
        public Question() { }
        public Question(string question, string answer)
		{
			this.name = question;
			this.answer = answer;
			this.variants = new List<string>();
		}
		public bool isAnswerRight(string variant)
		{
			return variant == this.answer;
		}
		public void SetVariant(string variatn)
		{
			if (this.variants.Count  == 4) {
				throw new Exception("This is maximum");
            } 
			this.variants.Add(variatn);
		}
	}
    public class QuizeClass
    {
        public string name { get; set; }
        public List<Question> questions { get; set; }

		public QuizeClass() { }

        public QuizeClass(string name, List<Question> questions)
        {
            this.name = name;
            this.questions = questions;
        }
    }

}

