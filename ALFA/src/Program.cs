using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Diagnostics;

MyParseMethod();

void MyParseMethod()
{
      String input = File.ReadAllText("../../../srcprograms/mvp.txt");
      ICharStream stream = CharStreams.fromString(input);
      ITokenSource lexer = new ALFALexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      ALFAParser parser = new ALFAParser(tokens);
      parser.BuildParseTree = true;
      IParseTree tree = parser.program();
      
}

