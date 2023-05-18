using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AlfaTest;

public class ProgramTreeMocker
{
    private int _myInvokingState = 0;

    //Generates our wanted parse tree created from the program: "int i = 2;
    public ALFAParser.ProgramContext MockProgramTreeWithIntVarDcl()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StatementContext stmtWithVarDclNode = new ALFAParser.StatementContext(programContext, _myInvokingState++);
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.TypeContext terminalIntNode = new ALFAParser.TypeContext(varDclNode, _myInvokingState++);
        
        TerminalNodeImpl terminalNodeImplInt = new TerminalNodeImpl(new CommonToken(9, "int"));
 
        terminalIntNode.AddChild(terminalNodeImplInt);
        terminalNodeImplInt.Parent = terminalIntNode;
        
        stmtWithVarDclNode.AddChild(varDclNode);
        
        TerminalNodeImpl identifierImpl = new TerminalNodeImpl(new CommonToken(11, "i"));
        TerminalNodeImpl equalSignImpl = new TerminalNodeImpl(new CommonToken(2, "="));
        TerminalNodeImpl rightSideAssignmentImpl = new TerminalNodeImpl(new CommonToken(12, "2"));
        varDclNode.AddChild(terminalIntNode);
        varDclNode.AddChild(identifierImpl);
        identifierImpl.Parent = varDclNode;
        varDclNode.AddChild(equalSignImpl);
        equalSignImpl.Parent = varDclNode;
        
        varDclNode.AddChild(rightSideAssignmentImpl);
        rightSideAssignmentImpl.Parent = varDclNode;
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        varDclNode.AddChild(semiColonImpl);
        semiColonImpl.Parent = varDclNode.Parent;

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
        eofImpl.Parent = programContext;
 
        programContext.children = new List<IParseTree>() { stmtWithVarDclNode, eofImpl };
        
        return programContext;
    }
    
    //Generates our wanted parse tree created from the program: "rect myRect1 = createRect(100, 100, 100, 100);
    public ALFAParser.ProgramContext MockProgramTreeWithRectVarDcl()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StatementContext stmtWithVarDclNode = new ALFAParser.StatementContext(programContext, _myInvokingState++);
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.TypeContext terminalRectNode = new ALFAParser.TypeContext(varDclNode, _myInvokingState++);
        
        TerminalNodeImpl terminalNodeRectImpl = new TerminalNodeImpl(new CommonToken(9, "rect"));
 
        //Assigning child and parent to TypeContext node
        terminalRectNode.AddChild(terminalNodeRectImpl);
        terminalNodeRectImpl.Parent = terminalRectNode;
        
        //Assigning child to StatementContext node
        stmtWithVarDclNode.AddChild(varDclNode);
        
        //Assigning child and parent to VarDclContext node
        TerminalNodeImpl identifierImpl = new TerminalNodeImpl(new CommonToken(11, "myRect1"));
        TerminalNodeImpl equalSignImpl = new TerminalNodeImpl(new CommonToken(2, "="));
        ALFAParser.BuiltInCreateShapeCallContext builtInCreateShapeCallContext =
            new ALFAParser.BuiltInCreateShapeCallContext(varDclNode, _myInvokingState);
        TerminalNodeImpl rightSideAssignmentImpl = new TerminalNodeImpl(new CommonToken(12, "2"));
        
        varDclNode.AddChild(terminalRectNode);

        varDclNode.AddChild(identifierImpl);
        identifierImpl.Parent = varDclNode;
        
        varDclNode.AddChild(equalSignImpl);
        equalSignImpl.Parent = varDclNode;

        varDclNode.AddChild(builtInCreateShapeCallContext);
        
        ALFAParser.BuiltInCreateShapeContext builtInCreateShapeContextCreateRect =
            new ALFAParser.BuiltInCreateShapeContext(builtInCreateShapeCallContext, _myInvokingState);
        TerminalNodeImpl createRectImpl = new TerminalNodeImpl(new CommonToken(11, "createRect"));
        builtInCreateShapeContextCreateRect.AddChild(createRectImpl);
        
        builtInCreateShapeCallContext.AddChild(builtInCreateShapeContextCreateRect);
        TerminalNodeImpl leftParenImpl = new TerminalNodeImpl(new CommonToken(11, "("));
        TerminalNodeImpl rightParenImpl = new TerminalNodeImpl(new CommonToken(11, ")"));
        builtInCreateShapeCallContext.AddChild(leftParenImpl);
        leftParenImpl.Parent = builtInCreateShapeCallContext;


        ALFAParser.ArgsContext argsContext = new ALFAParser.ArgsContext(builtInCreateShapeCallContext, _myInvokingState);
        ALFAParser.ArgContext argContext1 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl1 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext1.AddChild(numImpl1);
        numImpl1.Parent = argContext1;

        argsContext.AddChild(argContext1);
        TerminalNodeImpl comma1 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma1);
        comma1.Parent = argsContext;

        
        ALFAParser.ArgContext argContext2 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl2 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext2.AddChild(numImpl2);
        numImpl2.Parent = argContext2;
        
        argsContext.AddChild(argContext2);
        TerminalNodeImpl comma2 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma2);
        comma2.Parent = argsContext;
        
        ALFAParser.ArgContext argContext3 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl3 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext3.AddChild(numImpl3);
        numImpl3.Parent = argContext3;
        
        argsContext.AddChild(argContext3);
        TerminalNodeImpl comma3 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma3);
        comma3.Parent = argsContext;
        
        ALFAParser.ArgContext argContext4 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl4 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext4.AddChild(numImpl4);
        numImpl4.Parent = argContext4;
        
        argsContext.AddChild(argContext4);


        builtInCreateShapeCallContext.AddChild(argsContext);
        builtInCreateShapeCallContext.AddChild(rightParenImpl);
        rightParenImpl.Parent = builtInCreateShapeCallContext;

        
        
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        varDclNode.AddChild(semiColonImpl);
        semiColonImpl.Parent = varDclNode.Parent;

        
        TerminalNodeImpl eofImpl = new TerminalNodeImpl(new CommonToken(-1, "<EOF>"));
        eofImpl.Parent = programContext;
 
        programContext.children = new List<IParseTree>() { stmtWithVarDclNode, eofImpl };
        
        return programContext;
    }
    
    //Generates our wanted parse tree created from the program: "rect myRect1 = createRect(100, 100, 100, 100);
    public ALFAParser.ProgramContext MockProgramTreeWithWait()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StatementContext stmtWithBuiltInAnimCallContext = new ALFAParser.StatementContext(programContext, _myInvokingState++);
        
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


        ALFAParser.ArgsContext argsContext = new ALFAParser.ArgsContext(builtInAnimCallContext, _myInvokingState);
        ALFAParser.ArgContext argContext1 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl1 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext1.AddChild(numImpl1);
        numImpl1.Parent = argContext1;

        argsContext.AddChild(argContext1);

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
    
    //Generates our wanted parse tree created from the program: "move(myRect1, 100, 100);
    public ALFAParser.ProgramContext MockProgramTreeWithMove()
    {
        _myInvokingState = 0;
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StatementContext stmtWithBuiltInAnimCallContext = new ALFAParser.StatementContext(programContext, _myInvokingState++);
        
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


        ALFAParser.ArgsContext argsContext = new ALFAParser.ArgsContext(builtInAnimCallContext, _myInvokingState);
        ALFAParser.ArgContext argContext1 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl myRectImpl = new TerminalNodeImpl(new CommonToken(12, "myRect1"));
        argContext1.AddChild(myRectImpl);
        myRectImpl.Parent = argContext1;

        argsContext.AddChild(argContext1);
        TerminalNodeImpl comma1 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma1);
        comma1.Parent = argsContext;
        
        ALFAParser.ArgContext argContext2 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl2 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext2.AddChild(numImpl2);
        numImpl2.Parent = argContext2;

        argsContext.AddChild(argContext2);
        TerminalNodeImpl comma2 = new TerminalNodeImpl(new CommonToken(12, ","));
        argsContext.AddChild(comma2);
        comma1.Parent = argsContext;
        
        ALFAParser.ArgContext argContext3 = new ALFAParser.ArgContext(argsContext, _myInvokingState);
        TerminalNodeImpl numImpl3 = new TerminalNodeImpl(new CommonToken(12, "100"));
        argContext3.AddChild(numImpl3);
        numImpl3.Parent = argContext3;

        argsContext.AddChild(argContext3);


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