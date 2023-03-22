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

        Visit((dynamic)node);
        return node;
    }

    public override Node Visit(FuncCallNode node)
    {
        Console.WriteLine("\t\tFuncCall");
        
        Visit((dynamic)node);
        return node;
    }
    public override Node Visit(BuiltInsNode node)
    {
        Console.Write("\t\t\tBuiltIns: ");
        Console.WriteLine(node.Name);

        Visit((dynamic)node);
        return node;
    }

    public override Node Visit(ArgNode node)
    {
        Console.Write("\t\t\t\tArg: ");

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
        Console.WriteLine(node.Value);
        return node;
    }

    public override Node Visit(TypeNode node)
    {
        Console.WriteLine(node.Type);
        return node;
    }
}
