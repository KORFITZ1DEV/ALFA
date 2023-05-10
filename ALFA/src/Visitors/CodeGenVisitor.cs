using System.Reflection.Metadata;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;

public class CodeGenVisitor : ASTVisitor<Node>
{
    
    public string _output = string.Empty;
    public string _varOutput = string.Empty;
    public string _setupOutput = string.Empty;
    public string _drawOutput = string.Empty;
    
    public int _animationCount = 0;
    public SymbolTable _symbolTable;
    public string _path;

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

        if (child is BuiltInAnimCallNode builtInAnimNode)
        {
            if (builtInAnimNode.BuiltInAnimType == ALFATypes.BuiltInAnimEnum.move)
            {
            }
        }
        
        Emit("\n", ALFATypes.OutputEnum.VarOutput);
        return node;
    }

    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        switch (node.BuiltInAnimType)
        {
            case ALFATypes.BuiltInAnimEnum.move:
                Emit($"const anim_{_animationCount} = new MoveAnimation(", ALFATypes.OutputEnum.VarOutput);
                
                var child = Visit((dynamic)node.Arguments[0]);

                if (child is IdNode && !_drawOutput.Contains($"{child.Identifier}.render();"))
                {
                    Emit($"\t{child.Identifier}.render();\n", ALFATypes.OutputEnum.DrawOutput);
                }
                _animationCount++;
                break;
            
            case ALFATypes.BuiltInAnimEnum.wait:
                Emit($"const anim_{_animationCount} = new WaitAnimation(", ALFATypes.OutputEnum.VarOutput);
                _animationCount++;
                break;
        }
        
        Visit(node.Arguments[0]);
        foreach (var arg in node.Arguments.Skip(1))
        {
            Emit(",", ALFATypes.OutputEnum.VarOutput);
            Visit(arg);
        }

        Emit(");\n", ALFATypes.OutputEnum.VarOutput);
        
        return node;
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        switch (callNode.Type)
        {
            case ALFATypes.CreateShapeEnum.createRect:
                Emit("new Rectangle(", ALFATypes.OutputEnum.VarOutput);
                break;
        }

        //Todo create function that has a list of nodes as a parameter
        Visit(callNode.Arguments[0]);
        foreach (var arg in callNode.Arguments.Skip(1))
        {
            Emit(",", ALFATypes.OutputEnum.VarOutput);
            Visit(arg);
        }

        Emit(");\n", ALFATypes.OutputEnum.VarOutput);

        return callNode;
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