using System;

namespace Interpreter
{
	public class OperatorToken : Token
	{
		private char op;
		public OperatorToken (char op)
		{
			this.op = op;
			this.type = "operator";
		}
		
		public char GetValue() {
			return this.op;	
		}
	}
}

