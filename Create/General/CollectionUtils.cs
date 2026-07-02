using System.Collections.Frozen;

namespace Create.General;

public static class CollectionUtils
{
    extension<TValue>(FrozenDictionary<string, TValue> frozen)
    {   
        /// <summary>
        /// Gets an instance of a type that may be used to perform operations on a <see cref="FrozenDictionary{TKey, TValue}"/>
        /// using a <see cref="ReadOnlySpan{char}"/> as a key instead of a <see cref="string"/>.
        /// </summary>
        /// <returns>The created lookup instance.</returns>
        /// <exception cref="InvalidOperationException">This instance's comparer is not compatible with <see cref="ReadOnlySpan{char}"/>.</exception>
        public FrozenDictionary<string, TValue>.AlternateLookup<ReadOnlySpan<char>> GetAlternateLookup() =>
            frozen.GetAlternateLookup<ReadOnlySpan<char>>();
    }
}