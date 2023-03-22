using ALFA.AST_Nodes;

namespace ALFA;

public class TypeCheckVisitor : ASTVisitor<Node>
{
    public override Node Visit(ProgramNode node)
    {
        foreach (var stmt in node.stmts)
        {
            Visit(stmt);
        }
        return node;
    }

    public override Node Visit(StmtNode node)
    {
        if (node.Type == StmtNode.TypeEnum.Int)
        {
            if (node.VarDcl != null)
            {
                if (node.VarDcl.Num == null)
                {
                    throw new Exception("Invalid type on line " + node.Line + " column " + node.Col + " expected int but got" + node.VarDcl.FuncCall.BuiltIns);
                }
          
            }
        }
        else if (node.Type == StmtNode.TypeEnum.Square)
        {
            if (node.VarDcl != null)
            {
                if (node.VarDcl.FuncCall == null)
                {
                    throw new Exception("Invalid type expected createSquare but got " + node.VarDcl.Num + " on line " + node.Line + " column " + node.Col);
                }
            }
        }
        return node;
    }
    public override Node Visit(BuiltInsNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(FuncCallNode node)
    {
        throw new NotImplementedException();
    }
    public override Node Visit(ArgNode node)
    {
          throw new NotImplementedException();
    }
    public override Node Visit(VarDclNode node)
    {
        throw new NotImplementedException();
    }
} 

    
