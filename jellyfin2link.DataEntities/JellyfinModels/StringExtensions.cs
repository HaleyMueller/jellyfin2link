using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace jellyfin2link.DataEntities.JellyfinModels
{
    /// <summary>
    /// Provides extensions methods for <see cref="string" />.
    /// </summary>
    public static partial class StringExtensions
    {
        private static readonly Lazy<string> _transliteratorId = new Lazy<string>(() =>
            Environment.GetEnvironmentVariable("JELLYFIN_TRANSLITERATOR_ID")
            ?? "Any-Latin; Latin-Ascii; Lower; NFD; [:Nonspacing Mark:] Remove; [:Punctuation:] Remove;");

        /// <summary>
        /// Counts the number of occurrences of [needle] in the string.
        /// </summary>
        /// <param name="value">The haystack to search in.</param>
        /// <param name="needle">The character to search for.</param>
        /// <returns>The number of occurrences of the [needle] character.</returns>
        public static int Count(this ReadOnlySpan<char> value, char needle)
        {
            var count = 0;
            var length = value.Length;
            for (var i = 0; i < length; i++)
            {
                if (value[i] == needle)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the part on the left of the <c>needle</c>.
        /// </summary>
        /// <param name="haystack">The string to seek.</param>
        /// <param name="needle">The needle to find.</param>
        /// <returns>The part left of the <paramref name="needle" />.</returns>
        public static ReadOnlySpan<char> LeftPart(this ReadOnlySpan<char> haystack, char needle)
        {
            if (haystack.IsEmpty)
            {
                return ReadOnlySpan<char>.Empty;
            }

            var pos = haystack.IndexOf(needle);
            return pos == -1 ? haystack : haystack[..pos];
        }

        /// <summary>
        /// Returns the part on the right of the <c>needle</c>.
        /// </summary>
        /// <param name="haystack">The string to seek.</param>
        /// <param name="needle">The needle to find.</param>
        /// <returns>The part right of the <paramref name="needle" />.</returns>
        public static ReadOnlySpan<char> RightPart(this ReadOnlySpan<char> haystack, char needle)
        {
            if (haystack.IsEmpty)
            {
                return ReadOnlySpan<char>.Empty;
            }

            var pos = haystack.LastIndexOf(needle);
            if (pos == -1)
            {
                return haystack;
            }

            if (pos == haystack.Length - 1)
            {
                return ReadOnlySpan<char>.Empty;
            }

            return haystack[(pos + 1)..];
        }

        /// <summary>
        /// Ensures all strings are non-null and trimmed of leading an trailing blanks.
        /// </summary>
        /// <param name="values">The enumerable of strings to trim.</param>
        /// <returns>The enumeration of trimmed strings.</returns>
        public static IEnumerable<string> Trimmed(this IEnumerable<string> values)
        {
            return values.Select(i => (i ?? string.Empty).Trim());
        }

        /// <summary>
        /// Truncates a string at the first null character ('\0').
        /// </summary>
        /// <param name="text">The input string.</param>
        /// <returns>
        /// The substring up to (but not including) the first null character,
        /// or the original string if no null character is present.
        /// </returns>
        public static string TruncateAtNull(this string text)
        {
            return string.IsNullOrEmpty(text) ? text : text.AsSpan().LeftPart('\0').ToString();
        }
    }
}
