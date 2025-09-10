using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Service.Repositories;

namespace CodingTest.Service.Services
{
    public class ConversionService : IConversionService
    {
        private readonly Dictionary<char, int> _upperCaseMap = new()
        {
            {'A', 0}, {'B', 1}, {'C', 1}, {'D', 1}, {'E', 2}, {'F', 3}, {'G', 3}, {'H', 3},
            {'I', 4}, {'J', 5}, {'K', 5}, {'L', 5}, {'M', 5}, {'N', 5}, {'O', 6}, {'P', 7},
            {'Q', 7}, {'R', 7}, {'S', 7}, {'T', 7}, {'U', 8}, {'V', 9}, {'W', 9}, {'X', 9},
            {'Y', 9}, {'Z', 9}
        };

        private readonly Dictionary<char, int> _lowerCaseMap = new()
        {
            {'a', 9}, {'b', 8}, {'c', 8}, {'d', 8}, {'e', 7}, {'f', 6}, {'g', 6}, {'h', 6},
            {'i', 5}, {'j', 4}, {'k', 4}, {'l', 4}, {'m', 4}, {'n', 4}, {'o', 3}, {'p', 2},
            {'q', 2}, {'r', 2}, {'s', 2}, {'t', 2}, {'u', 1}, {'v', 0}, {'w', 0}, {'x', 0},
            {'y', 0}, {'z', 0}, {' ', 0}
        };

        private readonly Dictionary<int, char> _numberToLetterMap = new()
        {
            {0, 'A'}, {1, 'B'}, {2, 'E'}, {3, 'F'}, {4, 'I'}, {5, 'J'}, {6, 'O'}, {7, 'P'}, {8, 'U'}, {9, 'V'}
        };

        public ConversionResponse convertText(string inputText)
        {
            var response = new ConversionResponse
            {
                OriginalText = inputText
            };

            response.FirstResult = convertFirstResult(inputText);
            response.SecondResult = convertSecondResult(response.FirstResult);
            response.ThirdResult = convertThirdResult(response.SecondResult);
            response.FourthResult = convertFourthResult(response.ThirdResult);
            response.FifthResult = convertFifthResult(response.FourthResult);

            return response;
        }

        private string convertFirstResult(string input)
        {
            var numbers = new List<int>();

            foreach (char c in input)
            {
                if (char.IsUpper(c) && _upperCaseMap.ContainsKey(c))
                    numbers.Add(_upperCaseMap[c]);
                else if (char.IsLower(c) && _lowerCaseMap.ContainsKey(c))
                    numbers.Add(_lowerCaseMap[c]);
                else if (c == ' ')
                    numbers.Add(0);
            }

            return string.Join(" ", numbers);
        }

        private int convertSecondResult(string numericString)
        {
            var numbers = numericString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(int.Parse)
                                      .ToList();

            if (numbers.Count == 0) return 0;

            int result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                if (i % 2 == 1)
                    result += numbers[i];
                else
                    result -= numbers[i];
            }

            return result;
        }

        private string convertThirdResult(int number)
        {
            int target = Math.Abs(number);

            if (target == 0)
                return _numberToLetterMap[0].ToString();

            var digits = new List<int>();
            int sum = 0;

            while (sum < target)
            {
                bool addedInCycle = false;

                for (int d = 0; d <= 9 && sum < target; d++)
                {
                    if (sum + d <= target)
                    {
                        digits.Add(d);
                        sum += d;
                        addedInCycle = true;
                    }
                }

                if (!addedInCycle) break;
            }

            return string.Join(" ", digits.Select(d => _numberToLetterMap[d]));
        }

        private string convertFourthResult(string alphabetString)
        {
            if (string.IsNullOrEmpty(alphabetString))
                return string.Empty;

            string clean = alphabetString.Replace(" ", "");

            if (clean.Length >= 2)
                clean = clean[..^2] + "BE";
            else
                clean += "BE";

            return string.Join(" ", clean.ToCharArray());
        }

        private string convertFifthResult(string finalString)
        {
            var numbers = new List<string>();

            foreach (char c in finalString)
            {
                switch (c)
                {
                    case 'A':
                    case 'B':
                        numbers.Add("1");
                        break;
                    case 'E':
                    case 'F':
                        numbers.Add("3");
                        break;
                    case 'I':
                    case 'J':
                        numbers.Add("5");
                        break;
                    case 'O':
                    case 'P':
                        numbers.Add("7");
                        break;
                    case 'U':
                    case 'V':
                        numbers.Add("9");
                        break;
                }
            }

            return string.Join(" ", numbers);
        }
    }
}
