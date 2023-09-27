using System;
using System.Linq;
using System.IO;

namespace MyDefaultMAUIApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            using var stream = FileSystem.OpenAppPackageFileAsync("DadJokes.txt").GetAwaiter().GetResult();
            using var reader = new StreamReader(stream);

            var contents = reader.ReadToEnd();

            jokes.AddRange(contents.Split('\n'));
            DisplayJoke();
        }

        List<string> jokes = new List<string> { };
        List<string> displayedJokes = new List<string> { };

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            DisplayJoke();
        }

        private string GetNextJoke()
        {
            Random rnd = new Random();
            int currentJokeIndex = rnd.Next(0, jokes.Count);
            return jokes.ElementAt(currentJokeIndex);
        }

        private void RemoveJokeFromPool(string joke)
        {
            jokes.Remove(joke);
            displayedJokes.Add(joke);
            if (jokes.Count == 0)
            {
                jokes.AddRange(displayedJokes);
                displayedJokes.Clear();
            }
        }

        private void DisplayJoke()
        {
            var joke = GetNextJoke();
            DisplayLbl.Text = joke;
            SemanticScreenReader.Announce(DisplayLbl.Text);
            RemoveJokeFromPool(joke);
        }
    }
}