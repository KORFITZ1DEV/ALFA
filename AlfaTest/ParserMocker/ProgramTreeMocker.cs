using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AlfaTest;

public class ProgramTreeMocker
{
    private int _myInvokingState = 0;
    
    public ProgramTreeMocker()
    {
        _myInvokingState = 0;
    }

    public ALFAParser.ProgramContext MockProgramTree()
    {
        ALFAParser.ProgramContext programContext = new ALFAParser.ProgramContext(null, _myInvokingState++);
        ALFAParser.StatementContext stmtWithVarDclNode = new ALFAParser.StatementContext(programContext, _myInvokingState++);
        ALFAParser.TypeContext terminalIntNode = new ALFAParser.TypeContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        TerminalNodeImpl semiColonImpl = new TerminalNodeImpl(new CommonToken(1, ";"));
        
        TerminalNodeImpl terminalNodeImplInt = new TerminalNodeImpl(new CommonToken(9, "int"));

        //Assigning child and parent to TypeContext node
        terminalIntNode.AddChild(terminalNodeImplInt);
        terminalNodeImplInt.Parent = terminalIntNode;
        
        //Assigning child and parent to StatementContext node
        stmtWithVarDclNode.AddChild(terminalIntNode);
        stmtWithVarDclNode.AddChild(varDclNode);
        stmtWithVarDclNode.AddChild(semiColonImpl);
        semiColonImpl.Parent = stmtWithVarDclNode;
        
        //Assigning child and parent to VarDclContext node
        TerminalNodeImpl identifierImpl = new TerminalNodeImpl(new CommonToken(11, "x1"));
        TerminalNodeImpl equalSignImpl = new TerminalNodeImpl(new CommonToken(2, "="));
        TerminalNodeImpl rightSideAssignmentImpl = new TerminalNodeImpl(new CommonToken(12, "0"));
        
        varDclNode.AddChild(identifierImpl);
        identifierImpl.Parent = varDclNode;
        
        varDclNode.AddChild(equalSignImpl);
        equalSignImpl.Parent = varDclNode;
        
        varDclNode.AddChild(rightSideAssignmentImpl);
        rightSideAssignmentImpl.Parent = varDclNode;

        programContext.children = new List<IParseTree>() { stmtWithVarDclNode };
        
        return programContext;
    }

    public List<ALFAParser.StatementContext> MockParseTree(string input)
    {
        //should tokenize input to create a tree. 
        
        return new List<ALFAParser.StatementContext>();
    }

}