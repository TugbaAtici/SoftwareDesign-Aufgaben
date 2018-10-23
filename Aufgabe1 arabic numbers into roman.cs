using System;
using System.Collections.Generic;
using System.Text;
				

public class Program
{
	public static Dictionary<int, string> ROMAN_SYMBOLS = new Dictionary<int, string>()
	{
		{1, "I"},
		{2, "II"},
		{3, "III"},
		{4, "IV"},
		{5, "V"},
		{6, "VI"},
		{7, "VII"},
		{8, "VIII"},
		{9, "IX"},
		{10, "X"},
		{40, "XL"},
		{50, "L"},
		{100, "C"},
		{500, "D"},
		{1000, "M"}
	};
	
	
	public static string GetRomanUnit(int num)
	{
		int unit = num % 10;
		
		if (unit == 0)
		{
			return string.Empty;
		}
		
		return ROMAN_SYMBOLS[unit];
	}
	
	public static string GetRomanCombinedValue(int v, int position)
	{
		if (v == 0)
		{
			return string.Empty;
		}
		
		if (ROMAN_SYMBOLS.ContainsKey(v))
		{
			return ROMAN_SYMBOLS[v];
		}
		
		int left = 0;
		while (!ROMAN_SYMBOLS.ContainsKey(v - (left * position)))
		{
			left++;
		}
		
		if (left < 4)
		{
			int leftValue = v - left * position;
			int leftSubtract = position;
			
			while (!ROMAN_SYMBOLS.ContainsKey(leftValue - leftSubtract))
			{
				leftSubtract *= 2;
			}
			
			string r = ROMAN_SYMBOLS[leftValue - leftSubtract];
			
			var sb = new StringBuilder();
			for (int i = 0; i < left; i++)
			{
				sb.Append(r);
			}
			
			return string.Format("{0}{1}", ROMAN_SYMBOLS[leftValue], sb.ToString());
		}
		
		int right = position;
		while (!ROMAN_SYMBOLS.ContainsKey(v + right))
		{
			right *= 2;
		}

		int rightValue = v + right;
		return string.Format("{0}{1}", ROMAN_SYMBOLS[rightValue - v], ROMAN_SYMBOLS[rightValue]);
	}
	
	public static string GetRomanTens(int num)
	{
		int hundreds = (num / 100) * 100;
		int tens = ((num / 10) * 10 - hundreds);
		
		return GetRomanCombinedValue(tens, 10);
	}
	
	public static string GetRomanHundreds(int num)
	{
		int hundreds = (num / 100) * 100;
		
		return GetRomanCombinedValue(hundreds, 100);
	}

	public static string GetRomanNumber(int num)
	{
		return string.Format("{0}{1}{2}", GetRomanHundreds(num), GetRomanTens(num), GetRomanUnit(num));
	}

	public static void Main(string[] args)
	{
		string arg = "525";
		int value = 0;

		if (!int.TryParse(arg, out value))
		{
			Console.WriteLine("Invalid argument passed to program!");
		}
		else
		{
			string roman = GetRomanNumber(value);
			Console.WriteLine(roman);
		}
	}
}
