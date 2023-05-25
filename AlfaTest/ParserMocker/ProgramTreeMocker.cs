using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AlfaTest;

public class ProgramTreeMocker
{
    private int _myInvokingState = 0;

    //Generates our wanted parse tree created from the program: int i = 2;
    public ALFAParser.ProgramContext MockProgramTreeWithIntVarDcl()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StmtContext stmtWithVarDclNode = new ALFAParser.StmtContext(programContext, _myInvokingState++);
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.AssignStmtContext assmt = new ALFAParser.AssignStmtContext(varDclNode, _myInvokingState);
        ALFAParser.TypeContext terminalIntNode = new ALFAParser.TypeContext(varDclNode, _myInvokingState++);
        
        TerminalNodeImpl terminalNodeImplInt = new TerminalNodeImpl(new CommonToken(9, "int"));
        varDclNode.AddChild(terminalIntNode);
        varDclNode.AddChild(assmt);
 
        terminalIntNode.AddChild(terminalNodeImplInt);
        
        stmtWithVarDclNode.AddChild(varDclNode);
        
        TerminalNodeImpl identifierImpl = new TerminalNodeImpl(new CommonToken(11, "i"));
        TerminalNodeImpl equalSignImpl = new TerminalNodeImpl(new CommonToken(2, "="));
        assmt.AddChild(identifierImpl);
        assmt.AddChild(equalSignImpl);
        
        ALFAParser.ExprContext expr = new ALFAParser.ExprContext(assmt, _myInvokingState++);
        ALFAParser.NumContext rightSideAssignmentImpl = new ALFAParser.NumContext(expr);
        TerminalNodeImpl twoImpl = new TerminalNodeImpl(new CommonToken(2, "2"));
        rightSideAssignmentImpl.AddChild(twoImpl);
        assmt.AddChild(rightSideAssignmentImpl);
        
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        stmtWithVarDclNode.AddChild(semiColonImpl);

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
 
        programContext.children = new List<IParseTree>() { stmtWithVarDclNode, eofImpl };
        
        return programContext;
    }
    
    //Generates our wanted parse tree created from the program: rect myRect1 = createRect(100, 100, 100, 100);
    public ALFAParser.ProgramContext MockProgramTreeWithRectVarDcl()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StmtContext stmtWithVarDclNode = new ALFAParser.StmtContext(programContext, _myInvokingState++);
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.TypeContext terminalRectNode = new ALFAParser.TypeContext(varDclNode, _myInvokingState++);
        ALFAParser.AssignStmtContext assmt = new ALFAParser.AssignStmtContext(varDclNode, _myInvokingState++);
        
        
        TerminalNodeImpl terminalNodeRectImpl = new TerminalNodeImpl(new CommonToken(9, "rect"));
 
        //Assigning child and parent to TypeContext node
        terminalRectNode.AddChild(terminalNodeRectImpl);
        terminalNodeRectImpl.Parent = terminalRectNode;
        
        //Assigning child to StatementContext node
        stmtWithVarDclNode.AddChild(varDclNode);
        
        //Assigning child and parent to VarDclContext node
        TerminalNodeImpl identifierImpl = new TerminalNodeImpl(new CommonToken(11, "myRect1"));
        TerminalNodeImpl equalSignImpl = new TerminalNodeImpl(new CommonToken(2, "="));

        varDclNode.AddChild(terminalRectNode);
        varDclNode.AddChild(assmt);
        
        assmt.AddChild(identifierImpl);
        identifierImpl.Parent = assmt;
        assmt.AddChild(equalSignImpl);
        equalSignImpl.Parent = assmt;
        ALFAParser.BuiltInCreateShapeCallContext builtInCreateShapeCallContext =
            new ALFAParser.BuiltInCreateShapeCallContext(assmt, _myInvokingState++);
        assmt.AddChild(builtInCreateShapeCallContext);
        
        
        ALFAParser.BuiltInCreateShapeContext builtInCreateShapeContextCreateRect =
            new ALFAParser.BuiltInCreateShapeContext(builtInCreateShapeCallContext, _myInvokingState++);
        TerminalNodeImpl createRectImpl = new TerminalNodeImpl(new CommonToken(11, "createRect"));
        builtInCreateShapeContextCreateRect.AddChild(createRectImpl);
        
        builtInCreateShapeCallContext.AddChild(builtInCreateShapeContextCreateRect);
        TerminalNodeImpl leftParenImpl = new TerminalNodeImpl(new CommonToken(11, "("));
        TerminalNodeImpl rightParenImpl = new TerminalNodeImpl(new CommonToken(11, ")"));
        builtInCreateShapeCallContext.AddChild(leftParenImpl);
        leftParenImpl.Parent = builtInCreateShapeCallContext;


        ALFAParser.ActualParamsContext argsContext = new ALFAParser.ActualParamsContext(builtInCreateShapeCallContext, _myInvokingState);
        ALFAParser.ExprContext argContext1 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        
        ALFAParser.NumContext numContext1 = new ALFAParser.NumContext(argContext1);
        TerminalNodeImpl arg1 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext1.AddChild(arg1);
        argsContext.AddChild(numContext1);

        TerminalNodeImpl comma1 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma1);
        comma1.Parent = argsContext;

        
        ALFAParser.ExprContext argContext2 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext2 = new ALFAParser.NumContext(argContext2);
        TerminalNodeImpl arg2 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext2.AddChild(arg2);
        argContext2.AddChild(numContext2);
        
        argsContext.AddChild(numContext2);
        TerminalNodeImpl comma2 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma2);
        comma2.Parent = argsContext;
        
        ALFAParser.ExprContext argContext3 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext3 = new ALFAParser.NumContext(argContext2);
        TerminalNodeImpl arg3 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext3.AddChild(arg3);
        argContext3.AddChild(numContext3);
        
        argsContext.AddChild(numContext3);
        TerminalNodeImpl comma3 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma3);
        comma3.Parent = argsContext;
        
        ALFAParser.ExprContext argContext4 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext4 = new ALFAParser.NumContext(argContext2);
        TerminalNodeImpl arg4 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext4.AddChild(arg4);
        argContext4.AddChild(numContext4);
        
        argsContext.AddChild(numContext4);


        builtInCreateShapeCallContext.AddChild(argsContext);
        builtInCreateShapeCallContext.AddChild(rightParenImpl);
        rightParenImpl.Parent = builtInCreateShapeCallContext;

        
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        stmtWithVarDclNode.AddChild(semiColonImpl);

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
        eofImpl.Parent = programContext;
 
        programContext.children = new List<IParseTree>() { stmtWithVarDclNode, eofImpl };
        
        return programContext;
    }
    
    //Generates our wanted parse tree created from the program: "wait(100);"
    public ALFAParser.ProgramContext MockProgramTreeWithWait()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StmtContext stmtWithBuiltInAnimCallContext = new ALFAParser.StmtContext(programContext, _myInvokingState++);
        
        //Assigning child and parent to VarDclContext node
        ALFAParser.BuiltInAnimCallContext builtInAnimCallContext =
            new ALFAParser.BuiltInAnimCallContext(stmtWithBuiltInAnimCallContext, _myInvokingState);
        stmtWithBuiltInAnimCallContext.AddChild(builtInAnimCallContext);
        
        ALFAParser.BuiltInAnimContext builtInAnimContextWait =
            new ALFAParser.BuiltInAnimContext(builtInAnimCallContext, _myInvokingState);
        builtInAnimCallContext.AddChild(builtInAnimContextWait);
        
        TerminalNodeImpl waitImpl = new TerminalNodeImpl(new CommonToken(11, "wait"));
        builtInAnimContextWait.AddChild(waitImpl);
        waitImpl.Parent = builtInAnimContextWait;
        
        
        TerminalNodeImpl leftParenImpl = new TerminalNodeImpl(new CommonToken(11, "("));
        TerminalNodeImpl rightParenImpl = new TerminalNodeImpl(new CommonToken(11, ")"));
        builtInAnimCallContext.AddChild(leftParenImpl);
        leftParenImpl.Parent = builtInAnimCallContext;


        ALFAParser.ActualParamsContext argsContext = new ALFAParser.ActualParamsContext(builtInAnimCallContext, _myInvokingState);
        ALFAParser.ExprContext argContext1 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext1 = new ALFAParser.NumContext(argContext1);
        TerminalNodeImpl numImpl1 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext1.AddChild(numContext1);
        numContext1.AddChild(numImpl1);
        numImpl1.Parent = numContext1;

        argsContext.AddChild(numContext1);

        builtInAnimCallContext.AddChild(argsContext);
        builtInAnimCallContext.AddChild(rightParenImpl);
        rightParenImpl.Parent = builtInAnimCallContext;
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        builtInAnimCallContext.AddChild(semiColonImpl);
        semiColonImpl.Parent = builtInAnimCallContext.Parent;

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
        eofImpl.Parent = programContext;
 
        programContext.children = new List<IParseTree>() { stmtWithBuiltInAnimCallContext, eofImpl };
        
        return programContext;
    }
    
    //Generates our wanted parse tree created from the program: "move(myRect1, 100, 100);"
    public ALFAParser.ProgramContext MockProgramTreeWithMove()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StmtContext stmtWithBuiltInAnimCallContext = new ALFAParser.StmtContext(programContext, _myInvokingState++);
        
        //Assigning child and parent to VarDclContext node
        ALFAParser.BuiltInAnimCallContext builtInAnimCallContext =
            new ALFAParser.BuiltInAnimCallContext(stmtWithBuiltInAnimCallContext, _myInvokingState);
        stmtWithBuiltInAnimCallContext.AddChild(builtInAnimCallContext);
        
        ALFAParser.BuiltInAnimContext builtInAnimContextWait =
            new ALFAParser.BuiltInAnimContext(builtInAnimCallContext, _myInvokingState);
        builtInAnimCallContext.AddChild(builtInAnimContextWait);
        
        TerminalNodeImpl moveImpl = new TerminalNodeImpl(new CommonToken(11, "move"));
        builtInAnimContextWait.AddChild(moveImpl);
        moveImpl.Parent = builtInAnimContextWait;
        
        TerminalNodeImpl leftParenImpl = new TerminalNodeImpl(new CommonToken(11, "("));
        TerminalNodeImpl rightParenImpl = new TerminalNodeImpl(new CommonToken(11, ")"));
        builtInAnimCallContext.AddChild(leftParenImpl);
        leftParenImpl.Parent = builtInAnimCallContext;


        ALFAParser.ActualParamsContext argsContext = new ALFAParser.ActualParamsContext(builtInAnimCallContext, _myInvokingState);
        ALFAParser.ExprContext argContext1 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.IdContext idContext1 = new ALFAParser.IdContext(argContext1);
        argContext1.AddChild(idContext1);
        TerminalNodeImpl myRectImpl = new TerminalNodeImpl(new CommonToken(12, "myRect1"));
        idContext1.AddChild(myRectImpl);
        myRectImpl.Parent = idContext1;

        argsContext.AddChild(idContext1);
        TerminalNodeImpl comma1 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma1);
        comma1.Parent = argsContext;
        
        ALFAParser.ExprContext argContext2 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext2 = new ALFAParser.NumContext(argContext2);
        TerminalNodeImpl numImpl2 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext2.AddChild(numImpl2);
        numImpl2.Parent = numContext2;

        argsContext.AddChild(numContext2);
        TerminalNodeImpl comma2 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma2);
        comma1.Parent = argsContext;
        
        ALFAParser.ExprContext argContext3 = new ALFAParser.ExprContext(argsContext, _myInvokingState);
        ALFAParser.NumContext numContext3 = new ALFAParser.NumContext(argContext3);
        TerminalNodeImpl numImpl3 = new TerminalNodeImpl(new CommonToken(12, "100"));
        numContext3.AddChild(numImpl3);
        numImpl3.Parent = numContext3;

        argsContext.AddChild(numContext3);


        builtInAnimCallContext.AddChild(argsContext);
        builtInAnimCallContext.AddChild(rightParenImpl);
        rightParenImpl.Parent = builtInAnimCallContext;
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        builtInAnimCallContext.AddChild(semiColonImpl);
        semiColonImpl.Parent = builtInAnimCallContext.Parent;

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
        eofImpl.Parent = programContext;
 
        programContext.children = new List<IParseTree>() { stmtWithBuiltInAnimCallContext, eofImpl };
        
        return programContext;
    }

}