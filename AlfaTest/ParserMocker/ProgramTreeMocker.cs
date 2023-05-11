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
        ALFAParser.VarDclContext varDclNode = new ALFAParser.VarDclContext(stmtWithVarDclNode, _myInvokingState++);
        ALFAParser.TypeContext terminalIntNode = new ALFAParser.TypeContext(varDclNode, _myInvokingState++);
        
        TerminalNodeImpl terminalNodeImplInt = new TerminalNodeImpl(new CommonToken(9, "int"));
 
        //Assigning child and parent to TypeContext node
        terminalIntNode.AddChild(terminalNodeImplInt);
        terminalNodeImplInt.Parent = terminalIntNode;
        
        //Assigning child to StatementContext node
        stmtWithVarDclNode.AddChild(varDclNode);
        
        //Assigning child and parent to VarDclContext node
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

}