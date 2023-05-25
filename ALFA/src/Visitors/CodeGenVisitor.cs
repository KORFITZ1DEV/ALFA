using System.ComponentModel;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;

public class CodeGenVisitor : ASTVisitor<Node>
{
    
    public string _output = string.Empty;
    public string _varOutput = string.Empty;
    public string _mainOutput = string.Empty;
    public string _setupOutput = string.Empty;
    public string _drawOutput = string.Empty;
    
    public SymbolTable _symbolTable;
    public string _path;

    private int tabAmount = 1;

    public CodeGenVisitor(SymbolTable st, string path)
    {
        _symbolTable = st;
        _path = path;

    }
    void Emit(string nodeContent, ALFATypes.OutputEnum type)
    {
        switch (type)
        {
           case ALFATypes.OutputEnum.VarOutput:
               _varOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.MainOutput:
               _mainOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.SetupOutput:
               _setupOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.DrawOutput:
               _drawOutput += nodeContent;
               break;
           case ALFATypes.OutputEnum.Output:
               _output += nodeContent;
               break;
        }
    }
    
    void Emit(string nodeContent) => _mainOutput += nodeContent;

    void AddTabs()
    {
        for (int i = 0; i < tabAmount; i++) Emit("\t");
    }

    void OpenScope()
    {
        tabAmount++;
        Emit("{\n");
        AddTabs();
    }

    void CloseScope()
    {
        tabAmount--;
        AddTabs();
        Emit("}\n");
    }
    
    public override ProgramNode Visit(ProgramNode node)
    {
        string stdLib = File.ReadAllText((_path + "/stdlib.js"));
        Emit(stdLib + "\n\n", ALFATypes.OutputEnum.VarOutput);
        Emit("\nasync function main() {\n", ALFATypes.OutputEnum.MainOutput);
        Emit("\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n", ALFATypes.OutputEnum.SetupOutput);
        Emit("function draw() {\n\tbackground(255)\n\n\tfor (const shape of shapesToDraw) {\n\t\tshape.draw()\n\t}\n", ALFATypes.OutputEnum.DrawOutput);
        
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }

        Emit("\r}\n", ALFATypes.OutputEnum.MainOutput);
        Emit("}", ALFATypes.OutputEnum.DrawOutput);
        Emit(_varOutput + _mainOutput + _setupOutput + _drawOutput, ALFATypes.OutputEnum.Output);
        File.WriteAllText(_path + "/sketch.js", _output);
        
        return node;
    }

    public override Node Visit(VarDclNode node)
    {
        AddTabs();
        Emit("let ");
        Visit(node.AssignStmt);

        return (node);
    }

    public override Node Visit(AssignStmtNode node)
    {
        Emit($"var_{node.Identifier}" + "=");
        Visit((dynamic) node.Value);
        Emit("\n");
        return node;
    }

    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        Emit("\n");
        AddTabs();

        switch (node.Type)
        {
            case ALFATypes.BuiltInAnimEnum.move:
                Emit("await ");
                var child = Visit((dynamic)node.Arguments[0]);
                Emit($".move(");
                
                Visit((dynamic)node.Arguments[1]);
                Emit(",");
                Visit((dynamic)node.Arguments[2]);
                Emit(",");
                Visit((dynamic)node.Arguments[3]);
                Emit(");\n");
                
                break;
            
            case ALFATypes.BuiltInAnimEnum.wait:
                Emit($"await wait(");
                Visit((dynamic)node.Arguments[0]);
                Emit(");\n");
                break;
        }
        
        return node;
    }

    public override Node Visit(BuiltInParalAnimCallNode node)
    {
        Emit("\n");
        AddTabs();

        switch (node.Type)
        {
            case ALFATypes.BuiltInParalAnimEnum.move:
                Emit("() => ");
                var child = Visit((dynamic)node.Arguments[0]);
                Emit($".move(");
                
                Visit((dynamic)node.Arguments[1]);
                Emit(",");
                Visit((dynamic)node.Arguments[2]);
                Emit(",");
                Visit((dynamic)node.Arguments[3]);
                Emit("),");
                
                break;
        }

        return node;
    }

    public override Node Visit(IfStmtNode node)
    {
        List<Node> exprNodes = node.Expressions;
        bool hasElse = node.Blocks.Count > node.Expressions.Count;
        
        if (hasElse && node.Expressions.Count > 1) exprNodes.RemoveAt(exprNodes.Count - 1);

        for (int i = 0; i < exprNodes.Count; i++)
        {
            string ifType = i == 0 ? "if" : "else if";
            Emit($"{ifType} (");
            Visit((dynamic)exprNodes[i]);
            Emit(")");
            OpenScope();
            
            Visit(node.Blocks[i]);
            
            CloseScope();
        }

        if (hasElse)
        {
            AddTabs();
            Emit("else ");
            OpenScope();
            Visit((dynamic)node.Blocks.Last());
            CloseScope();
        }
        
        return node;
    }
    
    public override Node Visit(LoopStmtNode node)
    {
        Emit("\n");
        AddTabs();
        Emit("for (let ");
        var child = Visit(node.AssignStmt);
        // ExprNode, NumNod
        // node.AssignStmt.Value
        
        int from = VisitLoopExpr(node.AssignStmt.Value);
        int to = VisitLoopExpr(node.To);

        Emit($"; var_{node.AssignStmt.Identifier}");

        // i++
        if (from < to) {
            Emit(" < ");
            Visit(node.To);
            Emit($"; var_{node.AssignStmt.Identifier}++)");

        }
        else {
            Emit(" > ");
            Visit(node.To);
            Emit($"; var_{node.AssignStmt.Identifier}--)");
        }
        
        OpenScope();

        Visit(node.Block);
        
        CloseScope();

        return node;
    }

    private int VisitLoopExpr(Node node) {
        if(node is NumNode numNode) {
            return numNode.Value;
        }
        else if (node is IdNode idNode) {
            Symbol symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
            return VisitLoopExpr(symbol.Value);
        }
        else if (node is ExprNode exprNode) {
            return VisitLoopExpr(exprNode.Value);
        }
        else {
            throw new Exception("The guys who made this language made a mistake :)");
        }
    }

    public override Node Visit(ParalStmtNode node)
    {
        Emit("\n");
        AddTabs();
        Emit("await moveParal([");

        tabAmount++;
        AddTabs();

        Visit(node.Block);

        tabAmount--;
        AddTabs();
        
        Emit("\n");
        AddTabs();

        Emit("]);\n\n");
        
        return node;
    }

    // TODO: Use evalated expression value (dont replace loop variable tho)
    public override Node Visit(ExprNode node)
    {
        if (node.Right == null)
        {
            if (node.Operator == "u-") node.Operator = "-";
            if (node.Operator == "()") node.Operator = "(";
            Emit($" {node.Operator}");
            EmitValue(node.Left);
            Emit(")");
        }
        else
        {
            //TODO Unaries should be handled differently op before emitting left.
            EmitValue(node.Left);
            Emit($" {node.Operator} ");
            EmitValue(node.Right);    
        }
        
        return node;
    }
    
    public void EmitValue(Node value)
    {
        if (value is ExprNode exprNode)
        {
            Visit(exprNode);
        }
        else if (value is NumNode numNode)
        {
            Emit(numNode.Value.ToString());
        }
        else if (value is IdNode idNode)
        {
            Emit($"var_{idNode.Identifier}");
        }
        else if (value is BoolNode boolNode)
        {
            Emit(boolNode.Value.ToString().ToLower());
        }
    }

    public override Node Visit(BlockNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit((dynamic)stmt);
        }

        return node;
        
    }

    public override Node Visit(ParalBlockNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit((dynamic)stmt);
        }

        return node;
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        switch (callNode.Type)
        {
            case ALFATypes.CreateShapeEnum.createRect:
                Emit("new Rect(");
                break;
        }

        Visit((dynamic)callNode.Arguments[0]);
        foreach (var arg in callNode.Arguments.Skip(1))
        {
            Emit(",");
            Visit((dynamic)arg);
        }

        Emit(")");

        return callNode;
    }
    
    public override IdNode Visit(IdNode node)
    {
        // we prefix with var to avoid potential errors related to reserved js keywords
        Emit($"var_{node.Identifier}");
        return node;
    }
    
    public override Node Visit(BoolNode node)
    {
        Emit(node.Value.ToString());
        return node;
    }

    public override NumNode Visit(NumNode node)
    {
        Emit(node.Value.ToString());
        return node;
    }

}