using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Extensions;
public static class StringExtensions
{
    public static string RemoveDiacritics(this string text) =>
        string.Concat(text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark))
            .Normalize(NormalizationForm.FormC);

    public static string? Truncate(this string? value, int maxLength) =>
        string.IsNullOrEmpty(value) ? value : value.Length <= maxLength ? value : value[..maxLength];

    public static string OnlyAscii(this string source)
    {
        char[] chars = [.. source.Normalize(NormalizationForm.FormKD).Where(x => x < 128)];
        return new string(chars);
    }

    public static string RemoveWhiteSpaces(this string source)
    {
        char[] chars = [.. source.ToCharArray().Where(c => !char.IsWhiteSpace(c))];
        return new string(chars);
    }
    public static string CertificateDateFormater(DateTime date)
    {
        return new string(date.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES")));
    }
}
