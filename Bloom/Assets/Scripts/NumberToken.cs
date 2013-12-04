using System;

namespace Interpreter
{
	public class NumberToken : Token
	{
		private float num;	
		public NumberToken (float num)
		{
			this.num = num;
			this.type = "number";
		}
		
		public float GetValue()
		{
			return this.num;	
		}
	}
}

