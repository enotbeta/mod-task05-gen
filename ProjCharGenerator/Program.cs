using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace generator
{
    class Generator1
    {
        private string alphabet = "абвгдежзийклмнопрстуфхцчшщьыэюя"; 
        private int alphaLen = 31;
        private int[] sum;
        private int[,] matrix;
        private string text;
        private Random random = new Random();
        public Generator1(string path)
        {
            string tmp = File.ReadAllText(path);
            string[] lines = tmp.Split('\n');
            int counter = 0;
            int len = lines.Length;
            matrix = new int[len,len];

            foreach (var line in lines)
            {
                string[] temp_string = line.Split(' ');
                
                for(int i = 0; i < len; i++)
                {
                    matrix[counter,i] = Int32.Parse(temp_string[i]);
                }
                counter++;
            }

            sum = new int[len];
            for (int i = 0; i < len; i ++)
            {
                int sumTemp = 0;
                for(int j = 0; j < len; j ++)
                {
                    sumTemp += matrix[i,j];
                }
                sum[i] = sumTemp;
            }
        }

        public void GenerateText(int length, string name)
        {
            string text = new string(alphabet[random.Next(alphaLen)].ToString());
            int index;
            int randomNumber;
            for(int i = 1; i < length + 1; i++)
            {                
                if(random.Next(5) != 4)
                {
                    index = alphabet.IndexOf(text[text.Length - 1]);
                    randomNumber = random.Next(sum[index]);
                    text += NextLetter(randomNumber, index);
                }
                else
                {
                    text += ' ' + alphabet[random.Next(alphaLen)].ToString();
                    i++;
                }
            }
            File.WriteAllText(name, text);
        }
        private string NextLetter(int randomNumber, int index)
        {
            int tmp = 0;
            for(int i = 0; i < alphaLen; i ++)
            {
                tmp += matrix[index, i];
                if(tmp >= randomNumber)
                {
                    return alphabet[i].ToString();
                }
            }
            return "";
            
        }
    }
    class Generator2
    {
        private int[] probability;
        private string[] words;
        private string text;
        private int sum;
        private Random random = new Random();

        public Generator2(string path)
        {
            string tmp = File.ReadAllText(path);
            string[] lines = tmp.Split('\n');
            probability = new int[lines.Length];
            words = new string[lines.Length];
            int counter = 0;
            foreach(var line in lines)
            {
                string[] tmpLine = line.Split(' ');
                words[counter]+= tmpLine[0] + " " + tmpLine[1];
                probability[counter] = Int32.Parse(tmpLine[2]);
                counter++;
            }
            for(int i = 0; i < probability.Length; i ++)
            {
                sum += probability[i];
            }
        }
        public void GenerateText(string name)
        {
            string text = new string(words[random.Next(words.Length)] + " ");
            for(int i = 0; i < 500; i ++)
            {
                int randNum = random.Next(sum);
                text += words[NextWords(randNum)] + " ";
            }
            File.WriteAllText(name, text);
        }
        private int NextWords(int randomNumber)
        {
            int tmp = 0;
            for(int i = 0; i < probability.Length; i ++)
            {
                tmp += probability[i];
                if(tmp >= randomNumber)
                {
                    return i;
                }
            }
            return 0;

            
        }
    }
    class Generator3
    {
        private int[] probability;
        private string[] words;
        private string text;
        private int sum;
        private Random random = new Random();

        public Generator3(string path)
        {
            string tmp = File.ReadAllText(path);
            string[] lines = tmp.Split('\n');
            probability = new int[lines.Length];
            words = new string[lines.Length];
            int counter = 0;
            foreach(var line in lines)
            {
                string[] tmpLine = line.Split(' ');
                words[counter]+= tmpLine[0];
                probability[counter] = Int32.Parse(tmpLine[1]);
                counter++;
            }
            for(int i = 0; i < probability.Length; i ++)
            {
                sum += probability[i];
            }
        }
        public void GenerateText(string name)
        {
            string text = new string(words[random.Next(words.Length)] + " ");
            for(int i = 0; i < 350; i ++)
            {
                int randNum = random.Next(sum);
                text += words[NextWords(randNum)] + " ";
            }
            File.WriteAllText(name, text);
        }
        private int NextWords(int randomNumber)
        {
            int tmp = 0;
            for(int i = 0; i < probability.Length; i ++)
            {
                tmp += probability[i];
                if(tmp >= randomNumber)
                {
                    return i;
                }
            }
            return 0;    
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Generator1 gen1 = new Generator1(".\\pairs.txt"); 
            gen1.GenerateText(1000, "gen1.txt"); 
            Generator2 gen2 = new Generator2(".\\pairswords.txt"); 
            gen2.GenerateText("gen2.txt"); 
            Generator3 gen3 = new Generator3(".\\words.txt"); 
            gen3.GenerateText("gen3.txt"); 
        }
    }
}

