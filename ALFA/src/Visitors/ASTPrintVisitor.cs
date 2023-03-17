using ALFA.AST_Nodes;
using Antlr4.Runtime.Tree;

namespace ALFA;

public class ASTPrintVisitor : ASTVisitor<Node>
{
    public override Node Visit(ProgramNode node)
    {
        Console.WriteLine("ProgramNode");

        foreach (var stmt in node.stmts)
        {
            Visit(stmt);
        }
        
        return node;
    }
    public override Node Visit(StmtNode node)
    {
        Console.WriteLine("\tStmtNode");
        
        if (node.VarDcl != null)
        {
            Visit(node.VarDcl);
        }
        else
        {
            Visit(node.FuncCall);
        }

        return node;
    }
    public override Node Visit(VarDclNode node)
    {  
        Console.WriteLine("\t\tVarDcl: " + node.Id);
        Console.Write("\t");
        Visit(node.FuncCall);
        
        return node;
    }
    public override Node Visit(FuncCallNode node)
    {
        Console.WriteLine("\t\tFuncCall");
        Visit(node.BuiltIns);
        
        Console.WriteLine("\t\t\tArgs");
        foreach (var arg in node.Args)
        {
            Visit(arg);
        }
        
        return node;
    }
    public override Node Visit(BuiltInsNode node)
    {
        Console.Write("\t\t\tBuiltIns: ");
        
        switch (node.Type)
        {
            case BuiltInsNode.TypeEnum.create:
                Console.WriteLine("createSquare");
                break;
            case BuiltInsNode.TypeEnum.move:
                Console.WriteLine("move");
                break;
            case BuiltInsNode.TypeEnum.wait:
                Console.WriteLine("wait");
                break;
        }
        
        return node;
    }

    public override Node Visit(ArgNode node)
    {
        if (node.Id != null)
        {
            Console.WriteLine("\t\t\t\tArg: " + node.Id);
        }
        else
        {
            Console.WriteLine("\t\t\t\tArg: " + node.Num);
        }
        
        return node;
    }
}
