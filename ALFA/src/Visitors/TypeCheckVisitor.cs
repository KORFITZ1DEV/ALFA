using ALFA.AST_Nodes;
using ALFA.Types;

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
        Visit((dynamic)node);
        return node;
    }

    public override Node Visit(FuncCallNode node)
    {
        if (node.Arguments.Count != node.BuiltIns.FormalParams.Count)
        {
            throw new Exception($"Invalid number of arguments to {node.BuiltIns.BuiltInType.ToString()}, expected {node.BuiltIns.FormalParams.Count} but got {node.Arguments.Count} arguments");
        }
        
        int i = 0;
        foreach (var actualParam in node.Arguments)
        {
            if (actualParam.Value is IdNode idNode)
            {
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != node.BuiltIns.FormalParams[i])
                    {
                        throw new Exception($"Invalid type, expected {node.BuiltIns.FormalParams[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                    }
                }
            }
            else if (actualParam.Value is NumNode numNode)
            {
                if (node.BuiltIns.FormalParams[i] != ALFATypes.TypeEnum.@int)
                {
                    throw new Exception($"Invalid type expected {node.BuiltIns.FormalParams[i]} but got int on line {numNode.Line}:{numNode.Col}");
                }
            }
            i++;
        }
        return node;
    }
    
    public override Node Visit(VarDclNode node)
    {
        var visitedNode = Visit((dynamic)node.Value);

        if (visitedNode is FuncCallNode)
        {
            if (node.Type != ALFATypes.TypeEnum.square)
            {
                throw new Exception($"Invalid type {node.Type.ToString()}, expected type square on line {node.Line}:{node.Col}");
            }
        }
        else if (visitedNode is NumNode)
        {
            if (node.Type != ALFATypes.TypeEnum.@int)
            {
                throw new Exception($"Invalid type {node.Type.ToString()}, expected type int on line {node.Line}:{node.Col}");
            }
        }

        return node;
    }

    public override Node Visit(BuiltInsNode node) => node;
    public override Node Visit(ArgNode node) => node;
    public override Node Visit(IdNode node) => node;
    public override Node Visit(NumNode node) => node;
    public override Node Visit(TypeNode node) => node;
} 
    
