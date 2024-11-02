using System.Text.Json;

namespace exam_task
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary_ dictionary_ = new Dictionary_();
            Console.WriteLine("Select dictionary:\n1.english to ukranian\n2.english to russian");
            int lang = int.Parse(Console.ReadLine());
            if (lang == 1) { dictionary_ = LoadFromFile("EngToUkr"); }
            else if (lang == 2) { dictionary_ = LoadFromFile("EngToRus"); }
            else { Console.WriteLine("Wrong choice"); }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1.Add\n2.Remove\n3.Change\n4.Find word\n5.Show all\n6.Exit");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Remove what?\n1.Word\n2.Translation\n3.Go back");
                        choice = Int32.Parse(Console.ReadLine());
                        if (choice == 1)
                        {
                            AddWord(dictionary_);
                            break;
                        }
                        else if (choice == 2)
                        {
                            AddTranslation(dictionary_);
                            break;
                        }
                        else break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Remove what?\n1.Word\n2.Translation\n3.Go back");
                        choice = Int32.Parse(Console.ReadLine());
                        if (choice == 1)
                        {
                            RemoveWord(dictionary_);
                            break;
                        }
                        else if (choice == 2)
                        {
                            RemoveTranslation(dictionary_);
                            break;
                        }
                        else break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Change what?\n1.Word\n2.Translation\n3.Go back");
                        choice = Int32.Parse(Console.ReadLine());
                        if (choice == 1)
                        {
                            ChangeWord(dictionary_);
                            break;
                        }
                        else if (choice == 2)
                        {
                            ChangeTranslation(dictionary_);
                            break;
                        }
                        else break;
                    case 4:
                        FindWord(dictionary_);
                        Console.ReadKey();
                        break;
                    case 5:
                        ShowAll(dictionary_);
                        Console.ReadKey();
                        break;
                    case 6:
                        if (lang == 1) SaveToFile("EngToUkr", dictionary_);
                        if (lang == 2) SaveToFile("EngToRus", dictionary_);
                        return;
                    default:
                        Console.WriteLine("Wrong command");
                        break;
                }
                
            }
        }



        static void AddWord(Dictionary_ dictionary_)
        {
            Console.Clear();
            Word Word = new Word();
            Console.Write("Input word: ");
            string word = Console.ReadLine();
            Word._Word = word;

            Console.Write("Input translation: ");
            string translation = Console.ReadLine();
            Word.Translations.Add(translation);

            dictionary_.AddWord(Word);
        }
        static void AddTranslation(Dictionary_ dictionary_)
        {

            Console.Write("Input word: ");
            string word = Console.ReadLine();

            Console.Write("Input translation: ");
            string translation = Console.ReadLine();
            dictionary_.AddTranslationToWord(word, translation);
        }
        static void RemoveWord(Dictionary_ dictionary_)
        {
            Console.Write("Input word: ");
            string word = Console.ReadLine();
            try
            {
                dictionary_.RemoveWord(word);
            }
            catch (Exception)
            {
                Console.WriteLine("The word not found");
            }
            
        }
        static void RemoveTranslation(Dictionary_ dictionary_)
        {
            Console.Write("Input word: ");
            string word = Console.ReadLine();
            Console.Write("Input translation: ");
            string translation = Console.ReadLine();
            try
            {
                dictionary_.RemoveTranslation(word, translation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void ChangeTranslation(Dictionary_ dictionary_)
        {
            Console.Write("Input word: ");
            string word = Console.ReadLine();

            Console.Write("Input old translation: ");
            string oldTranslation = Console.ReadLine();
            Console.Write("Input old translation: ");
            string newTranslation = Console.ReadLine();
            try
            {
                dictionary_.ChangeTranslation(word, oldTranslation, newTranslation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private static void ChangeWord(Dictionary_ dictionary_)
        {
            Console.Write("Input old word: ");
            string oldWord = Console.ReadLine();

            Console.Write("Input new word: ");
            string newWord = Console.ReadLine();
            Console.Write("Input translation of this word: ");
            string newTranslation = Console.ReadLine();
            try
            {
                dictionary_.ChangeWord(oldWord, newWord, newTranslation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void FindWord(Dictionary_ dictionary_)
        {
            Console.Write("Input word: ");
            string word = Console.ReadLine();
            try
            {
                Console.WriteLine(dictionary_.FindWord(word));
            }
            catch (Exception)
            {
                Console.WriteLine("The word not found");
            }

        }
        static void ShowAll(Dictionary_ dictionary_)
        {
            foreach (var word in dictionary_.ReadWords())
            {
                Console.WriteLine($"\nWord: {word.Key}");
                Console.WriteLine("Translations:");
                foreach (var item in word.Value.Translations)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }
        static void SaveToFile(string filename, Dictionary_ dictionary_)
        {
            string path = dictionary_.GetProjectRootPath() + @"\" + filename + ".json";
            if (!File.Exists(filename))
            {
                File.Create(path).Dispose();
            }
            File.WriteAllText(path, dictionary_.ConvertToJson());
        }
        static Dictionary_ LoadFromFile(string fileName)
        {
            string filePath = new Dictionary_().GetProjectRootPath() + $"\\{fileName}.json";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File wasn't found");
                return new Dictionary_();
            }

            string jsonStr = File.ReadAllText(filePath);
            if (jsonStr == "") return new Dictionary_();
            var wordsDictionary = JsonSerializer.Deserialize<Dictionary<string, Word>>(jsonStr) ?? new Dictionary<string, Word>();

            var dict = new Dictionary_();
            foreach (var word in wordsDictionary)
            {
                dict.AddWord(word.Value);
            }

            return dict;
        }
    }
}
