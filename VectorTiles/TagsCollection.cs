﻿using System.Collections.Generic;
using System.Text;

namespace VectorTiles
{
    /// <summary>
    /// Represents a simple tags collection based on a dictionary.
    /// </summary>
    public class TagsCollection : Dictionary<string, object>
    {
        private const char KeyValueSeparator = '=';

        /// <summary>
        /// Creates a new tags collection.
        /// </summary>
        public TagsCollection(params KeyValuePair<string, object>[] tags)
        {
            foreach (var tag in tags)
                Add(tag.Key, tag.Value);
        }

        /// <summary>
        /// Creates a new tags collection initialized with the given existing tags.
        /// </summary>
        /// <param name="tags"></param>
        public TagsCollection(IEnumerable<KeyValuePair<string, object>> tags)
        {
            foreach (var tag in tags)
                Add(tag.Key, tag.Value);
        }

        /// <summary>
        /// Creates a new tags collection initialized with the given existing tags.
        /// </summary>
        /// <param name="tags"></param>
        public TagsCollection(IDictionary<string, string> tags)
        {
            if (tags != null)
            {
                foreach (KeyValuePair<string, string> pair in tags)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// Adds a tag from a string with key-value-separator
        /// </summary>
        /// <param name="tag">String of key-value-pair separated with key-value-separator</param>
        public void Add(string tag)
        {
            var splitPosition = tag.IndexOf(KeyValueSeparator);

            Add(tag.Substring(0, splitPosition), tag.Substring(splitPosition + 1));
        }

        /// <summary>
        /// Adds a list of tags to this collection.
        /// </summary>
        /// <param name="tags">List of tags</param>
        public void Add(IEnumerable<KeyValuePair<string, object>> tags)
        {
            foreach(var tag in tags)
                Add(tag.Key, tag.Value);
        }

        /// <summary>
        /// Returns true if the given key-value pair is found in this tags collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsKeyValue(string key, object value)
        {
            if (TryGetValue(key, out object val))
                return val.Equals(value);

            return false;
        }

        /// <summary>
        /// Returns true if one of the given keys exists in this tag collection.
        /// </summary>
        /// <param name="keys">Collection of keys to check</param>
        /// <returns>True, if one key in keys is containd in this collection</returns>
        public virtual bool ContainsOneOfKeys(ICollection<string> keys)
        {
            foreach (var tag in this)
            {
                if (keys.Contains(tag.Key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes all tags with given key and value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveKeyValue(string key, object value)
        {
            if (ContainsKeyValue(key, value))
            {
                Remove(key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check for equality
        /// </summary>
        /// <param name="other">Other TagCollections to check</param>
        /// <returns>True, if it is the same TagsCollection or contains the same tags</returns>
        public override bool Equals(object other)
        {
            if (other == this)
                return true;

            if (!(other is TagsCollection otherTagsCollection) || otherTagsCollection.Count != Count)
                return false;

            foreach (var tag in otherTagsCollection)
                if (!ContainsKeyValue(tag.Key, tag.Value))
                    return false;

            return true;
        }

        /// <summary>
        /// Returns a string that represents this tags collection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder tags = new StringBuilder();
            foreach (var tag in this)
            {
                tags.Append($"{tag.Key}{KeyValueSeparator}{tag.Value}");
                tags.Append(',');
            }
            if (tags.Length > 0)
            {
                return tags.ToString(0, tags.Length - 1);
            }
            return "empty";
        }
    }
}