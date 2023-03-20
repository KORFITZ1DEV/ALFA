using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Diagnostics;
using ALFA;
using ALFA.AST_Nodes;

MyParseMethod();

void MyParseMethod()
{
      String input = File.ReadAllText("../../../srcprograms/mvptypes.alfa");
      ICharStream stream = CharStreams.fromString(input);
      ITokenSource lexer = new ALFALexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      ALFAParser parser = new ALFAParser(tokens);
      parser.BuildParseTree = true;
      IParseTree tree = parser.program();
      BuildASTVisitor visitor = new BuildASTVisitor();
      Node ast = visitor.Visit(tree);
      ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
      astPrintVisitor.Visit(ast);
}

