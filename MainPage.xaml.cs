using System.ComponentModel;
using System.Diagnostics;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string spotlight;
        private List<char> letters = new List<char>();
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
        public List<char> Letters
        {
            get => letters; set
            {
                letters = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get => message; set
            {

                message = value;
                OnPropertyChanged();
            }
        }
        public string GameStatus
        {
            get => gameStatus; set
            {
                gameStatus = value;
                OnPropertyChanged();
            }
        }
        public string CurrentImage
        {
            get => currentImage; set
            {
                currentImage = value;
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
        private string message;
        int mistakes= 0;
        int maxwrong = 6;
        private string gameStatus;
        private string currentImage="img0.jpg";

        #endregion Fields

        public MainPage()
        {
            InitializeComponent();
            Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var letter = btn.Text;
                btn.IsEnabled = false;
                HandleGuess(letter[0]);
            }
        }

        private void HandleGuess(char letter)
        {
            if (guessed.IndexOf(letter) == -1)
            {
                guessed.Add(letter);
            }
            if(answer.IndexOf(letter) >= 0)
            {
                CalculateWord(answer, guessed);
                CheckIfGameWon();
            }
            else if(answer.IndexOf(letter) == -1)
            {
                mistakes++;
                UpdateStatus();
                CheckIfGameLost();
                CurrentImage = $"img{mistakes}.jpg";
            }
        }

        private void CheckIfGameLost()
        {
            if(mistakes == maxwrong)
            {
                Message = "You lost!";
                DisableLetters();
            }
        }

        private void DisableLetters()
        {
            foreach(var child in LettersContainer.Children)
            {
                var btn = child as Button;
                if(btn != null)
                {
                    btn.IsEnabled = false;
                }
            }
        }

        private void CheckIfGameWon()
        {
            if(Spotlight.Replace(" ", "") == answer)
            {
                Message= "You won!";
                DisableLetters();
            }
        }
        private void UpdateStatus()
        {

                GameStatus = $"Errors: {mistakes}/{maxwrong}.";
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            mistakes = 0;
            guessed = new List<char>();
            CurrentImage = "img0.jpg";
            PickWord();
            CalculateWord(answer, guessed);
            Message = "";
            UpdateStatus();
            EnableLetters();
        }

        private void EnableLetters()
        {
            foreach (var child in LettersContainer.Children)
            {
                var btn = child as Button;
                if (btn != null)
                {
                    btn.IsEnabled = true;
                }
            }
        }
    }
}