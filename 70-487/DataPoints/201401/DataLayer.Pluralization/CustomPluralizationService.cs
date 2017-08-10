using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
namespace DataLayer.Pluralization
{
public class CustomPluralizationService : IPluralizationService
{
  private readonly IPluralizationService baseService = new EnglishPluralizationService();
  public static List<string> PluralWords = new List<string>();

  public string Pluralize(string word)
  {
    if (word.EndsWith("ou"))
    {
      string newWord = string.Format("{0}z", word);
      PluralWords.Add(newWord);
      return newWord;
    }
    if (word.EndsWith("us"))
    {
      string newWord = string.Format("{0}ii", word.Remove(word.Length - 2));
      PluralWords.Add(newWord);
      return newWord;
    }
    if (word.StartsWith("French") && word.EndsWith("al"))
    {
      string newWord = string.Format("{0}aux", word.Remove(word.Length - 2));
      PluralWords.Add(newWord);
      return newWord;
    }

    //do not reprocess words that are already converted
    //this is happening as a result of calling the baseservice
    if (!PluralWords.Contains(word))
    {
      return baseService.Pluralize(word);
    }
    return word;
  }

  public string Singularize(string word)
  {
    return word;
  }
}
}
