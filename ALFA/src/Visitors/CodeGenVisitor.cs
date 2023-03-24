using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;

public class CodeGenVisitor : ASTVisitor<Node>
{
    private ALFATypes.OutputEnum active = ALFATypes.OutputEnum.VarOutput;
    
    private string _output = string.Empty;
    private string _varOutput = string.Empty;
    private string _setupOutput = string.Empty;
    private string _drawOutput = string.Empty;

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

        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        
        return node;
    }
    public override StatementNode Visit(StatementNode node)
    {
        Visit((dynamic)node);
        
        return node;
    }
    public override VarDclNode Visit(VarDclNode node)
    {
        Emit($"const {node.Identifier} = ", ALFATypes.OutputEnum.VarOutput);
        Visit((dynamic)node.Value);   
        return node;
    }

    public override FuncCallNode Visit(FuncCallNode node)
    {
        
        Emit(node.BuiltIns.BuiltInType.ToString(), ALFATypes.OutputEnum.Output);
        switch (node.BuiltIns.BuiltInType)
        {
            case ALFATypes.BuiltInTypeEnum.move:

                break;
            case ALFATypes.BuiltInTypeEnum.wait:

                break;
            case ALFATypes.BuiltInTypeEnum.createSquare:
                Emit("rect(", ALFATypes.OutputEnum.VarOutput);
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
}