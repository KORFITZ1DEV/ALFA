using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;
using ALFA.Types;

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
      
      Dictionary<string, BuiltIn> formalParams = new()
      {
            {"createRect", new BuiltIn(
                new List<ALFATypes.TypeEnum>() 
                { ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int }, 
                ALFATypes.BuiltInTypeEnum.createRect)},
            
            {"move", new BuiltIn(new List<ALFATypes.TypeEnum>()
            {
                ALFATypes.TypeEnum.rect, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int
            },ALFATypes.BuiltInTypeEnum.move)},
            
            {"wait", new BuiltIn(new List<ALFATypes.TypeEnum>()
            {
                ALFATypes.TypeEnum.@int
            }, ALFATypes.BuiltInTypeEnum.wait)}
      };

      SymbolTable symbolTable = new();
      
      BuildASTVisitor visitor = new BuildASTVisitor(symbolTable, formalParams);
      Node ast = visitor.Visit(tree);
      TypeCheckVisitor typeCheckVisitor = new TypeCheckVisitor(symbolTable);
      typeCheckVisitor.Visit(ast);
      //ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
      //astPrintVisitor.Visit(ast);
      CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, "../../../CodeGen-p5.js");
      codeGenVisitor.Visit(ast);
}

