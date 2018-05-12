using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Project5
{
    /// <summary>
    /// The User Interface class. Reads in the dictionary file and has click events
    /// for Get Message from File button (so the user can select a file to load in for their message)
    /// and Decrypt (which unencrypts the message the user entered).
    /// </summary>
    public partial class UserInterface : Form
    {
        private Trie _words;
        public UserInterface()
        {
            InitializeComponent();
            _words = new Trie();
            try
            {
                using (StreamReader sr = new StreamReader("dictionary.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        _words.Insert(line.Trim().ToLower());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// A recursive method for finding if message the user entered has a
        /// solution.
        /// </summary>
        /// <param name="cipher">the initial message the user enters</param>
        /// <param name="partial">a SB of the partially solved message with ? characters for the letters that haven't been decrypted</param>
        /// <param name="alphaUsed">boolean array of size 26 for the letters we have used so far, ex: alphaUsed[0] represents if 'a' has been used, [1] is 'b' and so on</param>
        /// <returns></returns>
        private bool Decrypt(string[] cipher, StringBuilder[] partial, bool[] alphaUsed)
        {
            //string[] cipher – the words of the encrypted message

            //StringBuilder[] partial – the current partial solution to the cipher, 
            //where each element corresponds to the current partial solution of the corresponding element in cipher.
            //Words in partial will have ? for characters that have not been decrypted yet, and will have lowercase 
            //values for characters that have been decrypted.The values in partial will be consistent as a possible 
            //solution to the cipher(so if two of the same letters appear in cipher, and those corresponding letters
            //have been decrypted in partial, they will have the same decrypted values).

            //bool[] alphaUsed – a size-26 array that keeps track of which lowercase letters have been used in the 
            //decryption. Spot 0 corresponds to ‘a’, spot 1 corresponds to ‘b’, etc.For each non -‘?’ character in partial,
            //that corresponding position in alphaUsed will be true.

            //this is a char array for easy access to the index of the letters in the alphabet
            char[] alphabet = {'a', 'b', 'c', 'd', 'e',
                   'f', 'g', 'h', 'i', 'j',
                   'k', 'l', 'm', 'n', 'o',
                   'p', 'q', 'r', 's', 't',
                   'u', 'v', 'w', 'x', 'y',
                   'z' };

            //If partial represents a solved puzzle(i.e.it contains no ? characters, and each StringBuilder is a word in the trie), return true

            //base case
            bool _passCheck = true;
            foreach (StringBuilder sb in partial)
            {
                //check that it doesn't contain any '?'
                //check that its a word in the trie
                if (!sb.ToString().Contains("?") && _words.Contains(sb.ToString())) continue;
                else
                {
                    _passCheck = false;
                    break; //leave loop b/c it failed to pass our tests, not a solution
                }
            }
            if (_passCheck) return true;

            foreach (StringBuilder sb in partial)
            {
                if (!_words.WildcardSearch(sb.ToString())) return false;
            }

            //recursive case
            //choose the first available ? position. (which word, which letter)
            int indexquestmark = 0; //the index of the question mark in the word
            int indexpartial = 0; //the index of the word in partial
            for (int i = 0; i < partial.Length; i++)
            {
                if (partial[i].ToString().Contains("?"))
                {
                    indexquestmark = partial[i].ToString().IndexOf("?");
                    indexpartial = i;
                    break;
                }
            }

            //str[indexquestmark] the position where the question mark is
            for (int i = 0; i < alphaUsed.Length; i++)
            {
                if (!alphaUsed[i]) //if we haven't used it yet
                {
                    char replace = alphabet[i]; //get the letter we want to sub in
                    ReplaceChar(cipher, partial, cipher[indexpartial][indexquestmark], replace);
                    alphaUsed[i] = true;
                    //partial[indexpartial] = temp;

                    if (Decrypt(cipher, partial, alphaUsed)) return true;
                    //undo changes
                    alphaUsed[i] = false;
                    ReplaceChar(cipher, partial, cipher[indexpartial][indexquestmark], '?');
                }
            }
            return false;
        }

        /// <summary>
        /// Replaces all occurences of a certain letter in partial by checking to see
        /// where char c is in cipher[i] and replacing the ? with char replace at the
        /// same index in partial
        /// </summary>
        /// <param name="cipher">the encrypted message</param>
        /// <param name="partial">the partially solved puzzle with ?s for unknowns</param>
        /// <param name="c">the letter we are looking for in cipher</param>
        /// <param name="replace">the letter we want to replace c with in partial</param>
        private void ReplaceChar(string[] cipher, StringBuilder[] partial, char c, char replace)
        {
            for (int i = 0; i < cipher.Length; i++)
            {
                //everytime i see char c I want to replace it at the corresponding position in partial
                string word = cipher[i]; //the encrypted word
                StringBuilder part = partial[i]; //the '?????'/partially solved word
                for (int j = 0; j < word.Length; j++)
                {
                    if (word[j] == c)
                    {
                        part[j] = replace;
                    }
                }
            }
        }

        /// <summary>
        /// The click event to the GetMessage button.
        /// Reads in an input file that the user chooses and updates
        /// the textbox with the contents of the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxGetMessageButton_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (uxOpenDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(uxOpenDialog.FileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            sb.Append(line);
                        }
                    }
                }
                //update the message box to contain stringbuilder
                uxTextbox.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Looks for any non spaces and non letters in a string and returns
        /// false if it finds any.
        /// </summary>
        /// <param name="expression">the string we're looking at</param>
        /// <returns>Returns true if the expression is all letters and spaces. No other characters allowed.</returns>
        private bool IsProperFormat(string expression)
        {
            return Regex.IsMatch(expression, @"^[a-zA-Z ]+$");
        }

        /// <summary>
        /// The Decrypt Button click event. 
        /// Makes sb partial - a stringbuilder the same length as cipher with
        /// "???? ?????" replacing all the words in cipher. Essentially a copy
        /// of cipher's words but with question marks instead.
        /// Calls the decrypt method to find the solution to
        /// the cipher.
        /// Updates uxSolvedTextbox with the
        /// solution (if there is one).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxDecryptButton_Click(object sender, EventArgs e)
        {
            string message = uxTextbox.Text.ToLower().Trim();

            if (IsProperFormat(message))
            {
                char[] delims = { ' ' };
                string[] cipher = message.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                int size = cipher.Length;
                StringBuilder[] partial = new StringBuilder[size];
                for (int i = 0; i < partial.Length; i++)
                {
                    partial[i] = new StringBuilder(); //create a new stringbuilder
                    int length = cipher[i].Length; //get the length of the cipher word
                    for (int j = 0; j < length; j++)
                    {
                        partial[i].Append("?"); //make the sb have the amount of "?" that matches the length of the cipher word
                    }
                }

                bool[] alphaUsed = new bool[26]; //new fresh array to pass in when we call the Decrypt method
                if (Decrypt(cipher, partial, alphaUsed))
                {
                    StringBuilder answer = new StringBuilder();
                    for (int i = 0; i < partial.Length; i++)
                    {
                        answer.Append(partial[i].ToString() + " ");
                    }
                    uxSolvedTextbox.Text = answer.ToString();
                }
                else
                {
                    uxSolvedTextbox.Text = "Error: No solution exists.";
                }
            }
            else
            {
                uxSolvedTextbox.Text = "Error: only letters and spaces are allowed.";
            }
        }
    }
}
