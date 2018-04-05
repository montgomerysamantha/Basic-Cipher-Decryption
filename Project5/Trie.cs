using System;

namespace Project5
{
    /// <summary>
    /// The Trie data structure that holds the dictionary.txt file
    /// </summary>
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
            if (word.Length == 0)
            {
                return _isWord;
            }
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
        /// Returns true if the string is an actual word.
        /// </summary>
        /// <returns>Whether or not if the string is a word in the dictionary</returns>
        public bool IsWord
        {
            get
            {
                return _isWord;
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

        /// <summary>
        /// Recursive method for finding words in the trie. 
        /// For example, if string word was “?a?a?a”, then WildcardSearch should return true 
        /// because “banana” (and possibly other words) is a word in the trie and 
        /// matches that wildcard search, since each ‘?’ can be matched by any letter.
        /// </summary>
        /// <param name="word">The string that contains ? for certain letters.</param>
        /// <returns>Returns true if the string word is a possible word in the trie</returns>
        public bool WildcardSearch(string word)
        {
            if (word == "" && _isWord) return true;
            else if (word == "" && !_isWord) return false;
            else
            {
                if (word[0] != '?') //if the first letter is a real letter
                {
                    //follow that child recursively (just like Contains)
                    char first = word[0];
                    int index = first - 'a';
                    if (_children[index] == null)
                    {
                        return false;
                    }
                    return _children[index].WildcardSearch(word.Substring(1));
                }
                else //if the first letter is a ?
                {
                    //loop through all children, 
                    //try recursive calls on all
                    for (int i = 0; i < _children.Length; i++)
                    {
                        //if ANY recursive calls return true->you return true
                        if (_children[i] != null && _children[i].WildcardSearch(word.Substring(1))) return true;
                    }

                    //after loop, if still there, return false
                    return false;
                }
            }
        }
    }
}