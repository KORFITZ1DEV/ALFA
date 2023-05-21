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

        //Visit left and right value recursively when they are expressions
        if (leftValue is ExprNode leftValueExpr)
        {
            EvaluateExpression(leftValueExpr);
        }
        if (rightValue is ExprNode rightValueExpr)
        {
            EvaluateExpression(rightValueExpr);
        }


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

                break;
            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }

        throw new ArgumentTypeException("You tried to evaluate an expression with invalid type compability," +
            $"left side type is {leftValue.GetType()}, right side type is {rightValue.GetType()}");

    }

    // Node -> BoolNode, IdNode, NumNode
    public void EvaluateArithmeticExpression(Node left, Node right, string op, ExprNode parent)
    {
        NumNode? leftNumNode = left is NumNode leftNum ? leftNum : null;
        NumNode? rightNumNode = right is NumNode rightNum ? rightNum : null;

        if (left is IdNode idNode) leftNumNode = VisitSymbol<NumNode>(idNode);
        if (right is IdNode idNode1) rightNumNode = VisitSymbol<NumNode>(idNode1);

        //This is true when left or rightNumNode is a BooleanNode.
        if (leftNumNode == null) throw new ArgumentTypeException($"Trying to use something that is a boolean in an addition on line {left.Line} column {left.Col}");
        if (rightNumNode == null) throw new ArgumentTypeException($"Trying to use something that is a boolean in an addition on line {right.Line} column {right.Col}");

        switch (op)
        {
            case "<":
                parent.Value = new BoolNode(leftNumNode.Value < rightNumNode.Value);
                break;
            case ">":
                parent.Value = new BoolNode(leftNumNode.Value > rightNumNode.Value);
                break;
            case "<=":
                parent.Value = new BoolNode(leftNumNode.Value <= rightNumNode.Value);
                break;
            case ">=":
                parent.Value = new BoolNode(leftNumNode.Value >= rightNumNode.Value);
                break;
            case "+":
                parent.Value = new NumNode(leftNumNode.Value + rightNumNode.Value);
                break;
            case "-":
                parent.Value = new NumNode(leftNumNode.Value - rightNumNode.Value);
                break;
            case "*":
                parent.Value = new NumNode(leftNumNode.Value * rightNumNode.Value);
                break;
            case "/":
                parent.Value = new NumNode(leftNumNode.Value / rightNumNode.Value);
                break;
            case "%":
                parent.Value = new NumNode(leftNumNode.Value % rightNumNode.Value);
                break;

                //Todo also needs unary minus.
        }
    }

    public void EvaluateBooleanExpression(Node left, Node right, string op, ExprNode parent)
    {
        Tuple<BoolNode, BoolNode> expectedNodes = EvaluateIdNode<BoolNode>(left, right, op, right == null);


        switch (op)
        {
            case "and":
                parent.Value = new BoolNode(expectedNodes.Item1.Value && expectedNodes.Item2.Value);
                break;
            case "or":
                parent.Value = new BoolNode(expectedNodes.Item1.Value || expectedNodes.Item2.Value);
                break;
            case "!":
                parent.Value = new BoolNode(!expectedNodes.Item1.Value);
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

        if (leftTNode == null) throw new ArgumentTypeException($"Incompatible type {leftTNode?.GetType()} in expression {op} on line {left.Line} column {left.Col}");
        if (rightTNode == null && isBinary) throw new ArgumentTypeException($"Incompatible type {rightTNode?.GetType()} in expression {op} on line {right.Line} column {right.Col}");

        return new Tuple<T, T>(leftTNode, rightTNode);
    }

    //VisitSymbol is called from an arithmetic or boolean expression when it must be determined
    //whether the identifier's value is a boolean or an integer
    public T VisitSymbol<T>(IdNode idNode) where T : Node
    {
        var symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);

        return (T)symbol.Value;
    }

    public override BoolNode Visit(BoolNode node)
    {
        throw new NotImplementedException();
    }

    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
