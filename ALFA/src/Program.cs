using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;

Prog.MyParseMethod();

namespace ALFA
{

    public static class Prog
    {
        public static void MyParseMethod(string path = "../../../srcprograms/mvptypes.alfa",string prog = "",string output = "")
        {
            string _output = output == "" ? "../../../Output/sketch.js" : "../../../../ALFA/Output/sketch.js";
            
            String input = prog == "" ? File.ReadAllText(path) : prog;

            if (prog == "int k = 0;\nint y = 0;\n\nint length = 20;\nint animDuration = 4000;\nint delay = 2000;\nsquare mySquare1 = createSquare(0, 0, length, length);\nint i = mySquare1;\nmove(mySquare1 , 200, animDuration);\nwait(delay);\nmove(mySquare1 , -200, animDuration);")
            {
                Console.WriteLine("WTH");
            }
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

