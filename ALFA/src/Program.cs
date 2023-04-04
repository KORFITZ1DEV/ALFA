using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;

Prog.Main();

namespace ALFA
{

    public static class Prog
    {
        public static void Main(string path = "../../../srcprograms/mvptypes.alfa",string prog = "",string output = "")
        {
            string _output = output == "" ? "../../../Output/sketch.js" : "../../../../ALFA/Output/sketch.js";
            
            String input = prog == "" ? File.ReadAllText(path) : prog;
            
            ICharStream stream = CharStreams.fromString(input);
            ITokenSource lexer = new ALFALexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ALFAParser parser = new ALFAParser(tokens);
            parser.BuildParseTree = true;
            IParseTree tree = parser.program();

            SymbolTable symbolTable = new();
            BuildASTVisitor visitor = new BuildASTVisitor(symbolTable);
            Node ast = visitor.Visit(tree);
            TypeCheckVisitor typeCheckVisitor = new TypeCheckVisitor(symbolTable);
            typeCheckVisitor.Visit(ast);
            ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
            astPrintVisitor.Visit(ast);
            CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, _output);
            codeGenVisitor.Visit(ast);
        }
    }
}

