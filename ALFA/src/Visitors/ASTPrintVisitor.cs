using System.Diagnostics.CodeAnalysis;
using ALFA.AST_Nodes;

namespace ALFA.Visitors;

[ExcludeFromCodeCoverage]
public class ASTPrintVisitor : ASTVisitor<Node>
{
    public override ProgramNode Visit(ProgramNode node)
    {
        Console.WriteLine("ProgramNode");

        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        
        return node;
    }
    
    public override VarDclNode Visit(VarDclNode node)
    {
        Console.WriteLine("\t\tVarDcl: ");
        Console.Write($"\t\t\tType {node.Type}\n\t\t\tid {node.Identifier}\n\t\t\t");
        Visit((dynamic)node.Value);
        return node;
    }
    
    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        Console.Write("\t\t\tBuiltIns: ");
        Console.WriteLine(node.Type.ToString());
        
        return node;
    }

    
    public override IdNode Visit(IdNode node)
    {
        Console.WriteLine(node.Identifier);
        return node;
    }

    public override NumNode Visit(NumNode node)
    {
        Console.WriteLine("value " + node.Value);
        return node;
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        Console.Write("\t\t\tBuiltIns: ");
        Console.WriteLine(callNode.Type.ToString());

        return callNode;
    }
}
