namespace exam_task
{
    public class Word
    {
        public string _Word {  get; set; } = string.Empty;
        public List<string> Translations { get; set; } = new List<string>();
        public override string ToString()
        {
            string result = $"Word: {_Word}, All translations:";
            foreach (var word in Translations)
            {
                result += "\t" + word + "\n";
            }
            return result;
        }


    }
}
