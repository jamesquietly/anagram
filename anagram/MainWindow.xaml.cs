using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace anagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //method for submit button
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string[] words = this.WordTextBox.Text.Split(' ');

            string result = CheckAnagram(words);
            this.ResultTextBlock.Text = result;
        }

        //removes duplicate input and checks for anagrams
        static string CheckAnagram(string[] input)
        {
            List<string> wordsToCheck = new List<string>();

            //new list doesn't contain repeated words
            for (int i = 0; i < input.Length; i++)
            {
                if (!wordsToCheck.Contains(input[i]))
                {
                    wordsToCheck.Add(input[i]);
                }
            }

            string tempString;
            string resultString = "";
            List<string> anagrams;
            List<string> usedWords = new List<string>();
            bool notUsed;
            foreach (string word in wordsToCheck)
            {
                //find anagrams for current word
                anagrams = FindAnagram(word, wordsToCheck);

                //add current word to used words list
                usedWords.Add(word);
                notUsed = true;
                //add current word to temp string
                tempString = word;

                //add anagrams to result string
                foreach (string a in anagrams)
                {
                    tempString = tempString + ", " + a;
                    //if see anagram pair was seen before, mark as false
                    if (usedWords.Contains(a))
                    {
                        notUsed = false;
                    }
                    //add all words to used words list
                    usedWords.Add(a);
                }
                //add to result string if anagram pair has not been seen before
                if (notUsed)
                {
                    resultString += tempString + "\n";
                }
            }

            return resultString;

        }

        //used to compare words of the same length
        static List<string> FindAnagram(string targetWord, List<string> words)
        {

            List<string> result = new List<string>();
            foreach (string currWord in words)
            {
                //check for words that have the same length
                if (targetWord.Length == currWord.Length)
                {
                    if (IsAnagram(targetWord, currWord))
                    {
                        result.Add(currWord);
                    }
                }
            }

            return result;
        }

        //used to check if words have contain same amount of letters
        static bool IsAnagram(string target, string word2)
        {
            int targetCount, word2Count;

            //check if same word is being compared
            if (target == word2)
            {
                return false;
            }

            for (int i = 0; i < word2.Length; i++)
            {
                //check if word has letter in it
                if (target.Contains(word2[i]))
                {
                    targetCount = CountLetters(word2[i], target);
                    word2Count = CountLetters(word2[i], word2);
                    //check if letter count is the same
                    if (targetCount != word2Count)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //used to count how manu times letter occurs in a word
        static int CountLetters(char letter, string word)
        {
            int result = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    result++;
                }
            }
            return result;
        }
    }

}
