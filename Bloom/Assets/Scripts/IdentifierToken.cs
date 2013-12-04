using System;

namespace Interpreter 
{
	public class IdentifierToken : Token
	{
		private string symbol;
		public IdentifierToken (string symbol)
		{
			this.type = "identifier";
			this.symbol = symbol;
		}
			
		public string GetValue() {
			return this.symbol;	
		}
	}
}

