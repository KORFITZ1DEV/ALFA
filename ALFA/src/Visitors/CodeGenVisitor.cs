using System.Reflection.Metadata;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;

public class CodeGenVisitor : ASTVisitor<Node>
{
    
    private string _output = string.Empty;
    private string _varOutput = string.Empty;
    private string _setupOutput = string.Empty;
    private string _drawOutput = string.Empty;
    private int _state = 0;
    private Dictionary<string, string> _squares = new();
    private int _squareNum = 0;
    private SymbolTable _symbolTable;
    private string _path;

    public CodeGenVisitor(SymbolTable st, string path)
    {
        _symbolTable = st;
        _path = path;

    }
    void Emit(string nodeContent, ALFATypes.OutputEnum type)
    {
        switch (type)
        {
           case ALFATypes.OutputEnum.VarOutput:
               _varOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.SetupOutput:
               _setupOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.DrawOutput:
               _drawOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.Output:
               _output += nodeContent;
               break;
        }
    }
    
    public override ProgramNode Visit(ProgramNode node)
    {
        Emit("let startTime = 0\nlet state = 0\nlet progress = 0\n", ALFATypes.OutputEnum.VarOutput);
        Emit("\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n", ALFATypes.OutputEnum.SetupOutput);
        Emit("function draw() {\n\tbackground(255)\n", ALFATypes.OutputEnum.DrawOutput);
        
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        Emit("}", ALFATypes.OutputEnum.DrawOutput);
        Emit(_varOutput + _setupOutput + _drawOutput, ALFATypes.OutputEnum.Output);
        File.WriteAllText(_path, _output);
        return node;
    }
    public override StatementNode Visit(StatementNode node)
    {
        Visit((dynamic)node);
        
        return node;
    }
    public override VarDclNode Visit(VarDclNode node)
    {
        Visit((dynamic)node.Value);
        return node;
    }

    public override FuncCallNode Visit(FuncCallNode node)
    {
        switch (node.BuiltIns.BuiltInType)
        {
            case ALFATypes.BuiltInTypeEnum.createSquare:
                string squareX = "x" + _squareNum;
                _squareNum++;
                _squares.Add(node.Identifier!, squareX);
                Emit($"let {squareX} = {ExtractArgValue(node.Arguments[0].Value)}\n", ALFATypes.OutputEnum.VarOutput);
                Emit($"\trect({squareX}", ALFATypes.OutputEnum.DrawOutput);
                
                foreach (var argNode in node.Arguments.Skip(1))
                {
                    Emit($",{ExtractArgValue(argNode.Value)}", ALFATypes.OutputEnum.DrawOutput);
                }
                
                Emit(")\n", ALFATypes.OutputEnum.DrawOutput);
                break;
            case ALFATypes.BuiltInTypeEnum.move:
                string moveDirection;
                string moveOperand;
                FuncCallNode createSquare = (FuncCallNode)_symbolTable.RetrieveSymbol(node.Identifier!).Value;
                int startX = ExtractArgValue(createSquare.Arguments[0].Value);
                int targetX = ExtractArgValue(node.Arguments[1].Value) + startX;
                int endX = startX + targetX;
                if (ExtractArgValue(node.Arguments[1].Value) > 0)
                {
                    moveDirection = $"{_squares[node.Identifier!]} = {startX} + ({targetX} - {startX}) * progress";
                    moveOperand = "<";
                }
                else
                {
                    targetX = startX+1;
                    moveDirection = $"{_squares[node.Identifier!]} = {Math.Abs(endX)} + ({endX} + {targetX}) * progress";
                    moveOperand = ">";
                }

                Emit("\n\tif (state == " + _state + ") {\n\t\t" +
                     "if (" + _squares[node.Identifier!] + " " + moveOperand + " " + targetX + ") {"
                     + "progress = (millis() - startTime) / " + ExtractArgValue(node.Arguments[2].Value) + ";"
                     + moveDirection +
                     " }\n\t\t" +
                     "else { startTime = millis(); state = " + (_state + 1) + ";}\n\t}\n", ALFATypes.OutputEnum.DrawOutput);
                _state++;
                break;
            case ALFATypes.BuiltInTypeEnum.wait:
                Emit("\n\tif (state == " + _state + ") {\n\t\t" + 
                     "if (millis() - startTime >= " + ExtractArgValue(node.Arguments[0].Value) + ") { startTime = millis(); state = " + (_state + 1) + " }\n\t}\n", ALFATypes.OutputEnum.DrawOutput);
                _state++;
                break;
        }


        foreach (var arg in node.Arguments)
        {
            Visit(arg);
        }
        
        return node;
    }
    public override BuiltInsNode Visit(BuiltInsNode node)
    {
        return node;
    }

    public override ArgNode Visit(ArgNode node)
    {
        
        Visit((dynamic)node.Value);
        return node;
    }
    
    public override IdNode Visit(IdNode node)
    {
        return node;
    }

    public override NumNode Visit(NumNode node)
    {
        return node;
    }

    public override TypeNode Visit(TypeNode node)
    {
        return node;
    }
    
    private int ExtractArgValue(Node argValue)
    {
        int value = 0;
        if (argValue is IdNode idNode)
        {
            NumNode numNode = (NumNode)_symbolTable.RetrieveSymbol(idNode.Identifier)!.Value;
            value = numNode.Value;
        }
        else if (argValue is NumNode numNode)
        {
           value = numNode.Value;
        }
        
        return value;
    }
}