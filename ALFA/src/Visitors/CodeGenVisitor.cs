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
    
    private int _animationCount = 0;
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
        string stdLib = File.ReadAllText((_path + "/stdlib.js"));
        Emit(stdLib + "\n\n", ALFATypes.OutputEnum.VarOutput);
        Emit("\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n", ALFATypes.OutputEnum.SetupOutput);
        Emit("function draw() {\n\tbackground(255)\n", ALFATypes.OutputEnum.DrawOutput);
        
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        
        Emit("const seqAnim = new SeqAnimation([", ALFATypes.OutputEnum.VarOutput);
        
        Emit("anim_0", ALFATypes.OutputEnum.VarOutput);
        
        for (int i = 1; i < _animationCount; i++)
        {
            Emit($",anim_{i}", ALFATypes.OutputEnum.VarOutput);
        }
        
        Emit("]);\n", ALFATypes.OutputEnum.VarOutput);
        
        Emit("\tseqAnim.play();\n", ALFATypes.OutputEnum.DrawOutput);
        
        Emit("}", ALFATypes.OutputEnum.DrawOutput);
        Emit(_varOutput + _setupOutput + _drawOutput, ALFATypes.OutputEnum.Output);
        File.WriteAllText(_path + "/sketch.js", _output);
        
        return node;
    }
    public override VarDclNode Visit(VarDclNode node)
    {
        Emit($"const {node.Identifier} = ", ALFATypes.OutputEnum.VarOutput);
        
        Node child = Visit((dynamic)node.Value);

        if (child is FuncCallNode funcCallNode)
        {
            if (funcCallNode.BuiltIns.BuiltInType == ALFATypes.BuiltInTypeEnum.createRect)
            {
                Emit($"\t{node.Identifier}.render();\n", ALFATypes.OutputEnum.DrawOutput);
            }
        }
        
        Emit("\n", ALFATypes.OutputEnum.VarOutput);
        return node;
    }

    public override FuncCallNode Visit(FuncCallNode node)
    {
        Visit(node.BuiltIns);

        Visit(node.Arguments[0]);
        foreach (var arg in node.Arguments.Skip(1))
        {
            Emit(",", ALFATypes.OutputEnum.VarOutput);
            Visit(arg);
        }
        
        Emit(");\n", ALFATypes.OutputEnum.VarOutput);
        return node;
    }
    public override BuiltInsNode Visit(BuiltInsNode node)
    {
        switch (node.BuiltInType)
        {
            case ALFATypes.BuiltInTypeEnum.move:
                Emit($"const anim_{_animationCount} = new MoveAnimation(", ALFATypes.OutputEnum.VarOutput);
                _animationCount++;
                break;
            
            case ALFATypes.BuiltInTypeEnum.wait:
                Emit($"const anim_{_animationCount} = new WaitAnimation(", ALFATypes.OutputEnum.VarOutput);
                _animationCount++;
                break;
            
            case ALFATypes.BuiltInTypeEnum.createRect:
                Emit("new Rectangle(", ALFATypes.OutputEnum.VarOutput);
                break;
        }
        
        return node;
    }
    
    
    public override IdNode Visit(IdNode node)
    {
        Emit(node.Identifier, ALFATypes.OutputEnum.VarOutput);
        return node;
    }

    public override NumNode Visit(NumNode node)
    {
        Emit(node.Value.ToString(), ALFATypes.OutputEnum.VarOutput);
        return node;
    }
}