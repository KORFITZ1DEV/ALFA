using ALFA.AST_Nodes;
using Antlr4.Runtime.Tree;

namespace ALFA;

public class ASTPrintVisitor : ASTVisitor<Node>
{
    public override Node Visit(ProgramNode node)
    {
        Console.WriteLine("ProgramNode");

        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        
        return node;
    }
    public override Node Visit(StatementNode node)
    {
        Console.WriteLine("\tStmtNode");
        
        Visit((dynamic)node);
        return node;
    }
    public override Node Visit(VarDclNode node)
    {
        Console.WriteLine("\t\tVarDcl: ");
        Console.Write($"\t\t\tType {node.Type}\n\t\t\tid {node.Identifier}\n\t\t\t");
        Visit((dynamic)node.Value);
        return node;
    }

    public override Node Visit(FuncCallNode node)
    {
        Console.WriteLine("\n\t\tFuncCall:");
        Visit(node.FunctionName);

        Console.WriteLine("\t\t\t\tArgs:");
        foreach (var arg in node.Arguments)
        {
            Visit(arg);
        }
        
        return node;
    }
    public override Node Visit(BuiltInsNode node)
    {
        Console.Write("\t\t\tBuiltIns: ");
        Console.WriteLine(node.BuiltInType.ToString());
        
        return node;
    }

    public override Node Visit(ArgNode node)
    {
        Console.Write("\t\t\t\t\tArg: ");

        Visit((dynamic)node.Value);
        return node;
    }
    
    public override Node Visit(IdNode node)
    {
        Console.WriteLine(node.Identifier);
        return node;
    }

    public override Node Visit(NumNode node)
    {
        Console.WriteLine("value " + node.Value);
        return node;
    }

    public override Node Visit(TypeNode node)
    {
        Console.WriteLine(node.Type);
        return node;
    }
}
