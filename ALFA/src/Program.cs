using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;
using ALFA.Types;

namespace ALFA
{
    public static class Prog
    {
        public static void Main(string[] args)
        {
            string _output = String.Empty;
            string input = String.Empty;
            
            if (args.Length == 1) // release mode (.exe)
            {
                if (args.Length == 0)
                    throw new Exception("Missing args");

                if (!args[0].EndsWith(".alfa")) 
                    throw new Exception("File must be .alfa");

                
                string? baseDir = Path.GetFullPath(Path.GetDirectoryName(args[0]));
            
                if (baseDir is null) 
                    throw new Exception($"{args[0]} is not a valid path");

                //string path = Path.GetFullPath(args[0]);
                input = "./CodeGen-p5.js/";
                // System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string alfaexeLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                
                // make output folder 
                Directory.CreateDirectory("./Output");
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/style.css", "./Output/style.css", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/index.html", "./Output/index.html", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/p5.min.js", "./Output/p5.min.js", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/stdlib.js", "./Output/stdlib.js", true);

                _output = baseDir + "Output/";
            }
            else if (args.Length == 2)// test / dev mode 
            {
                input = File.ReadAllText(args[0]);
                _output = args[1];
            }
            else // invalid mode
            {
                throw new Exception("Invalid args");
            }
            
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
            ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
            astPrintVisitor.Visit(ast);
            CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, _output);
            codeGenVisitor.Visit(ast);
        }
    }
}

