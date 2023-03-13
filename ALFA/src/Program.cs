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
      CustomListener listener = new CustomListener();
      parser.AddParseListener(listener);
      IParseTree tree = parser.start();
}

