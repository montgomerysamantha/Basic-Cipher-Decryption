using System;

namespace Project5
{
    class Trie
    {
        private bool _isWord = false;
        private Trie[] _children = new Trie[26];

        /// <summary>
        /// Returns whether a word is contained in this trie
        /// </summary>
        /// <param name="word">The word to find</param>
        /// <returns>Whether word is contained in this trie</returns>
        public bool Contains(string word)
        {
            if (word == "" && _isWord) return true;
            else if (word == "" && !_isWord) return false;
            else
            {
                char first = word[0];
                int index = first - 'a';
                if (_children[index] == null)
                {
                    return false;
                }
                return _children[index].Contains(word.Substring(1));
            }
        }

        /// <summary>
        /// Adds a word to this trie (no change is made if the word is already there)
        /// </summary>
        /// <param name="word">The word to add</param>
        public void Insert(string word)
        {
            if (word == "") _isWord = true;
            else
            {
                char first = word[0];
                int index = first - 'a';
                if (_children[index] == null)
                {
                    _children[index] = new Trie(); //add path
                }
                _children[index].Insert(word.Substring(1));
                //recursively call insert on the rest of the word
            }
        }

        /// <summary>
        /// Removes a word from this trie (no change is made if the word is not there)
        /// </summary>
        /// <param name="word">The word to remove</param>
        public void Remove(string word)
        {
            if (word == "") _isWord = false;
            else
            {
                char first = word[0];
                int index = first - 'a';
                if (_children[index] == null)
                {
                    return;
                }
                _children[index].Remove(word.Substring(1));
            }
        }

        public bool WildcardSearch(string s)
        {
            return true;
        }
    }
}