using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public class TypeCheckVisitor : ASTVisitor<Node>
{
    private SymbolTable _symbolTable;

    public TypeCheckVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }
    
    public override ProgramNode Visit(ProgramNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        return node;
    }
    
    public override FuncCallNode Visit(FuncCallNode node)
    {
        if (node.Arguments.Count != node.BuiltIns.FormalParams.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {node.BuiltIns.BuiltInType.ToString()}, expected {node.BuiltIns.FormalParams.Count} but got {node.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in node.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != node.BuiltIns.FormalParams[i])
                        throw new ArgumentTypeException($"Invalid type, expected {node.BuiltIns.FormalParams[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                }
            }
            else if (actualParam is NumNode numNode)
            { //think this check is useless as the parser should not allow it
                if (node.BuiltIns.FormalParams[i] != ALFATypes.TypeEnum.@int)
                    throw new ArgumentTypeException($"Invalid type expected {node.BuiltIns.FormalParams[i]} but got {ALFATypes.TypeEnum.@int} on line {numNode.Line}:{numNode.Col}");
            } 
            i++;
        }
        return node;
    }
    
    //Typecheck not needed for VarDcl as they are covered in the BuildASTVisitor, and for some reason it cant be wirtten in => node format
    public override VarDclNode Visit(VarDclNode node)
    {
        var visitedNode = Visit((dynamic)node.Value);

        if (visitedNode is FuncCallNode)
        {
            if (node.Type != ALFATypes.TypeEnum.rect)
                throw new Exception($"Invalid type {node.Type}, expected type {ALFATypes.TypeEnum.rect} on line {node.Line}:{node.Col}");
        }
        else if (visitedNode is NumNode)
        {
            if (node.Type != ALFATypes.TypeEnum.@int)
                throw new Exception($"Invalid type {node.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {node.Line}:{node.Col}");
        }

        return node;
    }

    public override BuiltInsNode Visit(BuiltInsNode node) => node;
    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
