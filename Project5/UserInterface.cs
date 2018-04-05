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
            int indexq = 0; //the index of the question mark in the word
            int indexp = 0; //the index of the word in partial
            for (int i = 0; i < partial.Length; i++)
            {
                if (partial[i].ToString().Contains("?"))
                {
                    indexq = partial[i].ToString().IndexOf("?");
                    indexp = i;
                    break;
                }
            }
            //StringBuilder str = partial[indexp];
            //str[indexq] the position where the question mark is
            for (int i = 0; i < alphaUsed.Length; i++)
            {
                if (!alphaUsed[i]) //if we haven't used it yet
                {
                    char replace = alphabet[i]; //get the letter we want to sub in
                    ReplaceChar(cipher, partial, cipher[indexp][indexq], replace);
                    alphaUsed[i] = true;
                    //partial[indexp] = temp;

                    if (Decrypt(cipher, partial, alphaUsed)) return true;
                    //undo changes
                    alphaUsed[i] = false;
                    //partial[indexp] = str; 
                    ReplaceChar(cipher, partial, cipher[indexp][indexq], '?');
                }
            }
            return false;
        }

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

        private bool IsProperFormat(string expression)
        {
            return Regex.IsMatch(expression, @"^[a-zA-Z ]+$");
        }

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

                bool[] alphaUsed = new bool[26];
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
