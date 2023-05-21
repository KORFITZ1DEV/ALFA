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

    public override Node Visit(VarDclNode node)
    {
        throw new NotImplementedException();
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

    public override AssignStmtNode Visit(AssignStmtNode assNode)
    {
        Symbol? idSymbol = _symbolTable.RetrieveSymbol(assNode.Identifier);
        Node assNodeVal = Visit(assNode.Value);

        if (idSymbol != null)
        {
            if (idSymbol.Type != assNodeVal is )
                throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
        }

        return assNode;
    }

    public override IfStmtNode Visit(IfStmtNode ifNode)
    {
        foreach (var expr in ifNode.Expressions)
        {
            //Maybe no
            var exprChild = Visit((dynamic)expr);
        }

        return ifNode;
    }

    public override LoopStmtNode Visit(LoopStmtNode node)
    {
        AssignStmtNode assigStmt = Visit(node.AssignStmt);
        Visit(node.To);
        Visit(node.Block);

        return node;
    }

    public override ParalStmtNode Visit(ParalStmtNode node)
    {
        foreach (var statement in node.Block.Statements)
        {
            Visit(statement);
        }

        return node;
    }

    public override ExprNode Visit(ExprNode node)
    {
        throw new NotImplementedException();
    }


    public void EvaluateExpression(ExprNode node)
    {
        var leftValue = node.Left;
        var rightValue = node.Right;

        //Visit left and right value when they are expressions
        if (leftValue is ExprNode leftValueExpr)
        {
            EvaluateExpression(leftValueExpr);
        }
        if (rightValue is ExprNode rightValueExpr)
        {
            EvaluateExpression(rightValueExpr);
        }

        //Evaluate the expressions.


        if (leftValue is NumNode leftNum && rightValue is NumNode rightNum)
        {
            EvaluateArithmeticExpression(leftNum.Value, rightNum.Value, node.Operator);
        }
        else if (leftValue is BoolNode leftBool && rightValue is BoolNode rightBool)
        {
            EvaluateBooleanExpression(leftBool.Value, rightBool.Value, node.Operator);
        }
        else if (leftValue is IdNode leftId && rightValue is NumNode rightNumb)
        {

        }
        else if (leftValue is NumNode leftNumb && rightValue is IdNode rightId)
        {

        }
        else if (leftValue is IdNode leftIdentifier && rightValue is IdNode rightIdentifier)
        {

        }
        else
        {
            throw new ArgumentTypeException("You tried to evaluate an expression with invalid type compability," +
             $"left side type is {leftValue.GetType()}, right side type is {rightValue.GetType()}");
        }
    }

    public int EvaluateArithmeticExpression(int leftVal, int rightVal, string op)
    {

        switch (op)
        {
            case "+":
                return leftVal + rightVal;
            case "-":
                return leftVal - rightVal;
            case "*":
                return leftVal * rightVal;
            case "/":
                return leftVal / rightVal;
            case "%":
                return leftVal % rightVal;

            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }
    }

    public bool EvaluateBooleanRelationalExpression(NumNode leftVal, NumNode rightVal, string op)
    {
        switch (op)
        {
            case "<":
                return leftVal.Value < rightVal.Value;
            case ">":
                return leftVal.Value > rightVal.Value;
            case "<=":
                return leftVal.Value <= rightVal.Value;
            case ">=":
                return leftVal.Value >= rightVal.Value;
            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }
    }

    public bool EvaluateBooleanExpression(bool leftVal, bool rightVal, string op)
    {

        switch (op)
        {
            case "and":
                return leftVal && rightVal;
            case "or":
                return leftVal || rightVal;
            case "!":
                return leftVal
            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }
    }

    public override BoolNode Visit(BoolNode node)
    {
        throw new NotImplementedException();
    }

    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
