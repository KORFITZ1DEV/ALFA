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
            string input = String.Empty;
            string _output = String.Empty;
            
            if (args.Length == 0)
                throw new Exception("Missing input arguments. Please provide a .alfa file, example: alfa ./path/to/file.alfa");
            
            if (args.Contains("--test")) // test mode 
            {
                input = File.ReadAllText(args[0]);
                _output = args[1];
            }
            else
            {
                if (!args[0].EndsWith(".alfa")) 
                    throw new Exception("Input file must be an alfa file");

                if (args.Length == 2)
                {
                    if (Path.Exists(args[1]))
                    {
                        _output = args[1];
                    }
                    else
                    {
                        throw new Exception("Output path does not exist");
                    }
                }

                string alfaexeLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                
                Directory.CreateDirectory($"{_output}/Output");
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/style.css", $"{_output}/Output/style.css", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/index.html", $"{_output}/Output/index.html", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/p5.min.js", $"{_output}/Output/p5.min.js", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/stdlib.js", $"{_output}/Output/stdlib.js", true);

                input = File.ReadAllText(args[0]);
                _output = $"{_output}/Output";   
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
            //ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
            //astPrintVisitor.Visit(ast);
            CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, _output);
            codeGenVisitor.Visit(ast);
            
            Process.Start(new ProcessStartInfo($"{_output}/index.html") { UseShellExecute = true });
        }
    }
}

