using System;
using System.Text;

class MainClass
{
	public static void Help()
	{
	System.Console.WriteLine ("Usage: RegBinConv.exe <data>");
	System.Console.WriteLine ("Example 1: RegBinConv.exe 0123456789ABCDEF");
	System.Console.WriteLine ("Example 2: RegBinConv.exe deadbeef");
	System.Console.WriteLine ("Example 3: RegBinConv.exe 00000000");
	System.Console.WriteLine ("Note: data.Length%2==0");
	}

	public static bool CheckArgs(string [] args)
	{
	if (args.Length != 1) return false;
	string data = args [0];
	if (data.Length % 2 != 0) return false;

	data = data.ToUpper ();

	bool flag=System.Text.RegularExpressions.Regex.IsMatch(data, @"\A\b[0-9A-F]+\b\Z");
	return flag;
	}

	public static string[] Process(string data)
	{
	string upper = data.ToUpper ();
	int cnt = upper.Length / 2;
	int offset = 0;
	string[] els = new string[cnt];
	int els_index = 0;
	while (true) 
		{
		int index_a = offset;
		int index_b = offset + 1;
		if (index_a > upper.Length) break;
		if (index_b > upper.Length) break;
		string value=String.Format("0x{0}{1}",upper[index_a],upper[index_b]);
		if (els_index > cnt) break;
		els [els_index] = value;

		offset += 2;
		++els_index;
		}
	return els;
	}

	public static string Print(string[] Elements)
	{
	int cnt = Elements.Length;
	StringBuilder sb = new StringBuilder ();
	sb.Append (@"([byte[]](");
	for (int i=0; i<cnt; ++i)
		{
		string el = Elements [i];
		sb.Append (el);
		if (i != cnt - 1) sb.Append (',');
		}
	sb.Append (@"))");
	string str=sb.ToString ();
	Console.WriteLine (str);
	return str;
	}

	public static void Main (string[] args)
	{
	bool IsOK = CheckArgs (args);
	if (!IsOK)
		{
		Help ();
		}
	else
		{
		string data = args [0];
		string[] elements=Process (data);
		Print (elements);
		}
	}

}
