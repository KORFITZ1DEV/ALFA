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
    
    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        List<ALFATypes.TypeEnum> nodeFormalParameters = FormalParameters.FormalParams[node.Type.ToString()];
        
        if (node.Arguments.Count != nodeFormalParameters.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {node.Type.ToString()}, expected {nodeFormalParameters.Count} but got {node.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in node.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != FormalParameters.FormalParams[node.Type.ToString()][i])
                        throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                }
            }
            else if (actualParam is NumNode numNode)
            { 
                if (nodeFormalParameters[i] != ALFATypes.TypeEnum.@int) 
                    throw new ArgumentTypeException($"Invalid type expected {nodeFormalParameters[i]} but got {ALFATypes.TypeEnum.@int} on line {numNode.Line}:{numNode.Col}");
            } 
            i++;
        }
        return node;
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        List<ALFATypes.TypeEnum> nodeFormalParameters = FormalParameters.FormalParams[callNode.Type.ToString()];
        
        if (callNode.Arguments.Count != nodeFormalParameters.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {callNode.Type.ToString()}, expected {nodeFormalParameters.Count} but got {callNode.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in callNode.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Visit(actualParam); //Not needed now but it is more extensible
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != FormalParameters.FormalParams[callNode.Type.ToString()][i])
                        throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                }
            }
            i++;
        }
        return callNode;
    }
    
    //Typecheck not needed for VarDcl as they are covered in the BuildASTVisitor, and for some reason it cant be wirtten in => node format
    public override VarDclNode Visit(VarDclNode node)
    {
        var visitedNode = Visit((dynamic)node.Value);

        if (visitedNode is BuiltInCreateShapeCallNode)
        {
            if (node.Type != ALFATypes.TypeEnum.rect)
                throw new TypeException($"Invalid type {node.Type}, expected type {ALFATypes.TypeEnum.rect} on line {node.Line}:{node.Col}");
        }
        else if (visitedNode is NumNode)
        {
            if (node.Type != ALFATypes.TypeEnum.@int)
                throw new TypeException($"Invalid type {node.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {node.Line}:{node.Col}");
        }

        return node;
    }

    
    //TODO hit
    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
