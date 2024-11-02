using System.Text.Json;

namespace exam_task
{
    public class Dictionary_
    {
        private Dictionary<string, Word> Words { get; set; } = new Dictionary<string, Word>();
        public void AddWord(Word word)
        {
            if (!Words.ContainsKey(word._Word)) {
                Words.Add(word._Word, word);
            }
        }
        public void AddTranslationToWord(string word, string translation)
        {
            if (Words.ContainsKey(word))
            {
                Words[word].Translations.Add(translation);
            }
            else
            {
                Console.WriteLine($"Word {word} wasn't found");
            }
        }

        public void RemoveWord(string word)
        {
            if (Words.ContainsKey(word))
            {
                Words.Remove(word);
            }
            else
            {
                Console.WriteLine($"Word {word} wasn't found\nPress any key to continue");
                Console.ReadKey();
            }
        }
        public Word FindWord(string word)
        {
            return Words[word];
        }
        public Dictionary<string, Word> ReadWords()
        {
            return Words;
        }
        public void ChangeWord(string oldWord, string newWord, string newTranslation)
        {
            if (Words.ContainsKey(oldWord))
            {
                this.RemoveWord(oldWord);
                this.AddWord(new Word { _Word = newWord, Translations = new List<string> { newTranslation } });
            }
            else
            {
                Console.WriteLine($"Word {oldWord} wasn't found\nPress any key to continue");
                Console.ReadKey();
            }
        }
        public void ChangeTranslation(string word,string oldTranslation, string newTranslation)
        {
            if (Words.ContainsKey(word))
            {
                if (!Words[word].Translations.Contains(oldTranslation))
                {
                    Console.WriteLine("Translation wasn't found");
                    return;
                }
                int index = Words[word].Translations.IndexOf(oldTranslation);
                Words[word].Translations[index] = newTranslation;
            }
            else
            {
                Console.WriteLine($"Word {word} wasn't found\nPress any key to continue");
                Console.ReadKey();
            }
        }
        public void RemoveTranslation(string word, string transletion)
        {
            if (Words.ContainsKey(word))
            {
                if (!Words[word].Translations.Contains(transletion))
                {
                    Console.WriteLine("Translation wasn't found");
                    return;
                }
                if (Words[word].Translations.Count <= 1)
                {
                    Console.WriteLine("You can't delete last translation\nPress any key to continue");
                    Console.ReadKey();
                    return;
                }
                Words[word].Translations.Remove(transletion);
            }
            else
            {
                Console.WriteLine($"Word {word} wasn't found");
            }
        }

        public string GetProjectRootPath()
        {
            var directory = new DirectoryInfo(Environment.CurrentDirectory);
            while (directory != null && directory.Name != "exam_task")
            {
                directory = directory.Parent;
            }
            return directory?.FullName ?? Environment.CurrentDirectory;
        }
        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(Words);
        }

    }
}
