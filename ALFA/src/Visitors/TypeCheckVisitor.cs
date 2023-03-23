/*
using ALFA.AST_Nodes;

namespace ALFA;

public class TypeCheckVisitor : ASTVisitor<Node>
{
    private SymbolTable _symbolTable;

    public TypeCheckVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }
    
    public override Node Visit(ProgramNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        return node;
    }

    public override Node Visit(StatementNode node)
    {
        if (node.Type == ALFATypes.TypeEnum.Int)
        {
            if (node.VarDcl != null)
            {
                if (node.VarDcl.Num == null)
                {
                    throw new Exception("Invalid type on line " + node.Line + " column " + node.Col + " expected int but got" + node.VarDcl.FuncCall.BuiltIns);
                }
          
            }
        }
        else if (node.Type == ALFATypes.TypeEnum.Square)
        {
            if (node.VarDcl != null)
            {
                if (node.VarDcl.FuncCall == null)
                {
                    throw new Exception("Invalid type expected createSquare but got " + node.VarDcl.Num + " on line " + node.Line + " column " + node.Col);
                }
                
                Visit(node.VarDcl.FuncCall);
            }
        }

        if (node.FuncCall != null)
        {
            Visit(node.FuncCall);
        }
        return node;
    }
    public override Node Visit(BuiltInsNode node)
    {
        throw new NotImplementedException();
    }

    public override Node Visit(FuncCallNode node)
    {
        if (node.Args.Count != node.BuiltIns.FormalParams.Count)
        {
            throw new Exception("Invalid number of arguments on line " + node.Line + " column " + node.Col + " expected " + node.BuiltIns.FormalParams.Count + " but got " + node.Args.Count);
        }
        int i = 0;
        foreach (var actualParam in node.Args)
        {
            if (actualParam.Id != null)
            {
                Symbol idParam = _symbolTable.RetrieveSymbol(actualParam.Id);
                if (idParam != null)
                {
                    if (idParam.Type != node.BuiltIns.FormalParams[i])
                    {
                        throw new Exception("Invalid type on line " + node.Line + " column " + node.Col + " expected " +
                                            node.BuiltIns.FormalParams[i] + " but got " + idParam.Type);
                    }
                }
            }
            else
            {
                if (node.BuiltIns.FormalParams[i] != ALFATypes.TypeEnum.Int)
                {
                    throw new Exception("Invalid type on line " + node.Line + " column " + node.Col + " expected " +
                                        node.BuiltIns.FormalParams[i] + " but got int");
                }
                i++;
            }
        }
        return node;
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
*/
    
