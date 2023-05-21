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
        Visit(node.AssignStmt);

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

    public override AssignStmtNode Visit(AssignStmtNode assNode)
    {
        Symbol? idSymbol = _symbolTable.RetrieveSymbol(assNode.Identifier);
        Visit(assNode.Value);

        if (idSymbol != null)
        {
            if (idSymbol.Value is ExprNode exprValue)
            {
                EvaluateExpression(exprValue);
                switch (exprValue.Value.GetType().ToString())
                {
                    case "NumNode":
                        if (idSymbol.Type != ALFATypes.TypeEnum.@int) throw new ArgumentTypeException($"Invalid type, exception evaluates to an integer on line {exprValue.Value.Line}:{exprValue.Value.Col}");
                        break;
                    case "BoolNode":
                        if (idSymbol.Type != ALFATypes.TypeEnum.@bool) throw new ArgumentTypeException($"Invalid type, exception should evaluate to a boolean, but evaluates to a {idSymbol.Type} on line {exprValue.Value.Line}:{exprValue.Value.Col}");
                        break;
                }
            }

            if (idSymbol.Value is AssignStmtNode assNodeChild && assNodeChild.Value is IdNode idChildNode)
            {
                Symbol? idSymbolChild = _symbolTable.RetrieveSymbol(idChildNode.Identifier);
                if (idSymbolChild.Type != idSymbol.Type)
                    throw new TypeException("Invalid type on line " + assNodeChild.Line + ": " + "column: " + assNodeChild.Col);
            }
        }

        return assNode;
    }

    public override IfStmtNode Visit(IfStmtNode ifNode)
    {

        foreach (var expr in ifNode.Expressions)
        {
            Visit((dynamic)expr);
        }

        return ifNode;
    }

    public override LoopStmtNode Visit(LoopStmtNode node)
    {
        Visit(node.AssignStmt);
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
        EvaluateExpression(node);
        Console.WriteLine("Test");

        return node;
    }


    public void EvaluateExpression(ExprNode node)
    {
        var leftValue = node.Left;
        var rightValue = node.Right;

        ExprNode? leftValueExpr = leftValue as ExprNode;
        ExprNode? rightValueExpr = rightValue as ExprNode;

        //Visit left and right value recursively when they are expressions
        if (leftValueExpr != null)
        {
            EvaluateExpression(leftValueExpr);
            leftValue = leftValueExpr.Value;

            if (node.Operator == "()") node.Value = leftValue;
        }
        if (rightValueExpr != null)
        {
            EvaluateExpression(rightValueExpr);
            rightValue = rightValueExpr.Value;
        }

        //If both are not an expresion then we must retrieve values from id.

        switch (node.Operator)
        {
            case "<":
            case ">":
            case "<=":
            case ">=":
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
                EvaluateArithmeticExpression(leftValue, rightValue, node.Operator, node);
                break;
            case "==":
            case "!=":
            case "and":
            case "or":
            case "!":
                EvaluateBooleanExpression(leftValue, rightValue, node.Operator, node);
                break;
            case "()":
                break;
            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }


        Node leftTypeToCompare = leftValue, rightTypeToCompare = rightValue;

        if (leftValue is IdNode assNodeL)
        {
            Symbol leftSym = _symbolTable.RetrieveSymbol(assNodeL.Identifier);
            leftTypeToCompare = leftSym.Value;
        }
        if (rightValue is IdNode assNodeR)
        {
            Symbol rightSym = _symbolTable.RetrieveSymbol(assNodeR.Identifier);
            rightTypeToCompare = rightSym.Value;
        }

        if (leftValue != null && rightValue != null
        && (leftTypeToCompare.GetType() != rightTypeToCompare.GetType())) throw new ArgumentTypeException("You tried to evaluate an expression with invalid type compability," +
        $"left side type is {leftValue.GetType()}, right side type is {rightValue.GetType()}");


    }

    // Node -> BoolNode, IdNode, NumNode
    public void EvaluateArithmeticExpression(Node left, Node right, string op, ExprNode parent)
    {
        Tuple<NumNode, NumNode> expectedNodes = EvaluateIdNode<NumNode>(left, right, op, right == null);

        switch (op)
        {
            case "<":
                parent.Value = new BoolNode(expectedNodes.Item1.Value < expectedNodes.Item1.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case ">":
                parent.Value = new BoolNode(expectedNodes.Item1.Value > expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "<=":
                parent.Value = new BoolNode(expectedNodes.Item1.Value <= expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case ">=":
                parent.Value = new BoolNode(expectedNodes.Item1.Value >= expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "+":
                parent.Value = new NumNode(expectedNodes.Item1.Value + expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "-":
                parent.Value = new NumNode(expectedNodes.Item1.Value - expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "*":
                parent.Value = new NumNode(expectedNodes.Item1.Value * expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "/":
                parent.Value = new NumNode(expectedNodes.Item1.Value / expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "%":
                parent.Value = new NumNode(expectedNodes.Item1.Value % expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "u-":
                parent.Value = new NumNode(-expectedNodes.Item1.Value, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
        }
        
    }

    public void EvaluateBooleanExpression(Node left, Node right, string op, ExprNode parent)
    {
        Tuple<BoolNode, BoolNode> expectedNodes = EvaluateIdNode<BoolNode>(left, right, op, right != null);

        switch (op)
        {
            case "and":
                parent.Value = new BoolNode(expectedNodes.Item1.Value && expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "or":
                parent.Value = new BoolNode(expectedNodes.Item1.Value || expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "!":
                parent.Value = new BoolNode(!expectedNodes.Item1.Value, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
            default:
                throw new Exception("You used a boolean operator that is not being switched on");
        }
    }

    public Tuple<T, T> EvaluateIdNode<T>(Node left, Node right, string op, bool isBinary) where T : Node
    {
        T? leftTNode = left is T leftNum ? leftNum : null;
        T? rightTNode = right is T rightNum ? rightNum : null;

        if (left is IdNode idNode) leftTNode = VisitSymbol<T>(idNode);
        if (right is IdNode idNode1) rightTNode = VisitSymbol<T>(idNode1);

        if (leftTNode == null) throw new ArgumentTypeException($"Incompatible type {left.GetType()} in '{op}' expression on line {left.Line} column {left.Col}");
        if (rightTNode == null && isBinary) throw new ArgumentTypeException($"Incompatible type {right.GetType()} in '{op}' expression on line {right.Line} column {right.Col}");

        return new Tuple<T, T>(leftTNode, rightTNode!);
    }

    //VisitSymbol is called from an arithmetic or boolean expression when it must be determined
    //whether the identifier's value is a boolean or an integer
    public T VisitSymbol<T>(IdNode idNode) where T : Node
    {
        var symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);

        if (symbol.Value is AssignStmtNode assNode)
        {
            return (T)assNode.Value;
        }
        
        return (T)symbol.Value;
    }

    public override BoolNode Visit(BoolNode node) => node;
    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
