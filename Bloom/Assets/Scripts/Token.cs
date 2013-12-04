using System;

namespace Interpreter
{
	public class Token
	{
		public string type;
		public Token ()
		{
		}
		
		public string GetTokenType() {
			return this.type;	
		}
	}
	

}

