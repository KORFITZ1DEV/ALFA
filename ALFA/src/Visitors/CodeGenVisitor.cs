using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;

public class CodeGenVisitor : ASTVisitor<Node>
{
    
    public string _output = string.Empty;
    public string _varOutput = string.Empty;
    public string _mainOutput = string.Empty;
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
           case ALFATypes.OutputEnum.MainOutput:
               _mainOutput += nodeContent;
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
        Emit("\nasync function main() {\n\t", ALFATypes.OutputEnum.MainOutput);
        Emit("\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n", ALFATypes.OutputEnum.SetupOutput);
        Emit("function draw() {\n\tbackground(255)\n", ALFATypes.OutputEnum.DrawOutput);
        
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }

        Emit("\r}\n", ALFATypes.OutputEnum.MainOutput);
        Emit("}", ALFATypes.OutputEnum.DrawOutput);
        Emit(_varOutput + _mainOutput + _setupOutput + _drawOutput, ALFATypes.OutputEnum.Output);
        File.WriteAllText(_path + "/sketch.js", _output);
        
        return node;
    }

    public override Node Visit(VarDclNode node)
    {
        Emit("const ", ALFATypes.OutputEnum.VarOutput);
        Visit(node.AssignStmt);

        return (node);
    }

    public override Node Visit(AssignStmtNode node)
    {
        Emit(node.Identifier + "=", ALFATypes.OutputEnum.VarOutput);
        Visit((dynamic) node.Value);
        Emit(";\n", ALFATypes.OutputEnum.VarOutput);

        return node;
    }

    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        switch (node.Type)
        {
            case ALFATypes.BuiltInAnimEnum.move:
                Emit("await ", ALFATypes.OutputEnum.MainOutput);
                var child = Visit((dynamic)node.Arguments[0], ALFATypes.OutputEnum.MainOutput);
                Emit($".move(", ALFATypes.OutputEnum.MainOutput);
                
                Visit((dynamic)node.Arguments[1], ALFATypes.OutputEnum.MainOutput);
                Emit(", 0, ", ALFATypes.OutputEnum.MainOutput);
                Visit((dynamic)node.Arguments[2], ALFATypes.OutputEnum.MainOutput);
                Emit(");\n\t", ALFATypes.OutputEnum.MainOutput);
                
                break;
            
            case ALFATypes.BuiltInAnimEnum.wait:
                Emit($"await wait(", ALFATypes.OutputEnum.MainOutput);
                Visit((dynamic)node.Arguments[0], ALFATypes.OutputEnum.MainOutput);
                Emit(");\n\t", ALFATypes.OutputEnum.MainOutput);
                break;
        }
        
        return node;
    }

    public override Node Visit(IfStmtNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(LoopStmtNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(ParalStmtNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(ExprNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(BoolNode node)
    {
        throw new NotImplementedException();
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        switch (callNode.Type)
        {
            case ALFATypes.CreateShapeEnum.createRect:
                Emit("new Rect(", ALFATypes.OutputEnum.VarOutput);
                break;
        }

        Visit((dynamic)callNode.Arguments[0]);
        foreach (var arg in callNode.Arguments.Skip(1))
        {
            Emit(",", ALFATypes.OutputEnum.VarOutput);
            Visit((dynamic)arg);
        }

        Emit(");", ALFATypes.OutputEnum.VarOutput);

        return callNode;
    }

    public IdNode Visit(IdNode node, ALFATypes.OutputEnum type)
    {
        Emit(node.Identifier, type);
        return node;
    }
    
    public NumNode Visit(NumNode node, ALFATypes.OutputEnum type)
    {
        Emit(node.Value.ToString(), type);
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