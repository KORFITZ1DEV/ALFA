using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ALFA.Types;

namespace ALFA
{

    [ExcludeFromCodeCoverage]
    public static class Prog
    {
        
        public static void Main(string[] args)
        {
            string input = String.Empty;
            string _output = ".";

            if (args.Length == 0)
                throw new Exception("Missing input arguments. Please provide a .alfa file, example: alfa ./path/to/file.alfa");



            if (args.Contains("--test")) // test mode 
            {
                input = args[0];
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

                Directory.CreateDirectory($"{_output}/CodeGen-p5.js/Output");
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/style.css", $"{_output}/CodeGen-p5.js/Output/style.css", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/index.html", $"{_output}/CodeGen-p5.js/Output/index.html", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/Output/p5.min.js", $"{_output}/CodeGen-p5.js/Output/p5.min.js", true);
                File.Copy(alfaexeLocation + "/CodeGen-p5.js/stdlib.js", $"{_output}/CodeGen-p5.js/Output/stdlib.js", true);

                input = File.ReadAllText(args[0]);
                _output = $"{_output}/CodeGen-p5.js/Output";
            }

            string path = ($"{_output}/index.html");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true)
            {
                path = path.Replace("/", "\\");
            }

            if (File.Exists($"{_output}/sketch.js"))
            {
                File.Delete($"{_output}/sketch.js");
            }

            ICharStream stream = CharStreams.fromString(input);
            ITokenSource lexer = new ALFALexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            ALFAParser parser = new ALFAParser(tokens);
            parser.BuildParseTree = true;
            IParseTree tree = parser.program();

            ErrorNodeImpl? syntacticErrorNode = findErrorNode(tree);
            if (syntacticErrorNode != null)
            {
                throw new SyntacticException($"Something is syntactically incorrect: {syntacticErrorNode.GetText()} on line {syntacticErrorNode.Payload?.ToString()?.Split(",")[3].Split(":")[0]} column {syntacticErrorNode.Payload.ToString().Split(",")[3].Split(":")[1]}");
            }

            SymbolTable symbolTable = new();
            BuildASTVisitor visitor = new BuildASTVisitor(symbolTable);
            Node ast = visitor.Visit(tree);
            TypeCheckVisitor typeCheckVisitor = new TypeCheckVisitor(symbolTable);
            typeCheckVisitor.Visit(ast);
            CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, _output);
            codeGenVisitor.Visit(ast);

            if (args.Contains("--test")) return;

            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }

        private static ErrorNodeImpl? findErrorNode(IParseTree context)
        {
            
            for (int i = 0; i < context.ChildCount; i++)
            {
                var child = context.GetChild(i);
                if (child is ErrorNodeImpl errorNodeChild)
                {
                    return errorNodeChild;
                }

                ErrorNodeImpl? returnedErrorNode = findErrorNode(child);
                if (returnedErrorNode != null) return returnedErrorNode;

            }

            return null;
        }
    }
}

