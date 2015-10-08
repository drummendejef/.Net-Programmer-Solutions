using System;

public class Test 
{
	public static void Main(string[] args)
	{
		if(args == null)
			Console.WriteLine("Usage: "abc "arg"");		
		Console.WriteLine(args[0]);
	}
}