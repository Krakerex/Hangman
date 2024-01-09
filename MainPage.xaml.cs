using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string spotlight;

        #region UI Properties

        public string Spotlight
        {
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        #endregion UI Properties

        #region Fields

        List<string> words = new List<string>()
        {
            "python",
            "java",
            "kotlin",
            "swift",
            "javascript",
            "php",
            "html",
            "css",
            "csharp",
            "ruby",
            "sql",
            "perl",
            "word",
            "excel",
            "powerpoint",
            "access",
            "outlook",
        };

        string answer = "";
        List<char> guessed = new List<char>();

        #endregion Fields

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            PickWord();
            CalculateWord(answer, guessed);
        }

        #region Game Engine

        private void PickWord()
        {
            answer = words[new Random().Next(0, words.Count)];
            Debug.WriteLine($"Answer is {answer}");
        }

        private void CalculateWord(string answer, List<char> guessed)
        {
            var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();
            Spotlight = string.Join(' ', temp);
        }

        #endregion Game Engine
    }
}