using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.CodeGen;

public class ProgramNodeTest
{
    private CodeGenVisitor _sut;
    
    [Theory]
    [ClassData(typeof(ProgramNodeCodeGenTestData))]
    public void ProgramNodeWritesToTheDifferentOutputs(Node node, SymbolTable symbolTable,
        string expectedVarOutput, string expectedSetupOutput, string expectedDrawOutput, string expectedOutput, string expectedMainOutput)
    {
        _sut = new CodeGenVisitor(symbolTable, "../TestOutput");
        _sut._path = "../../../../ALFA/bin/Debug/net7.0/CodeGen-p5.js/";

        _sut.Visit(node);
    
        Assert.Equal(expectedVarOutput, _sut._varOutput);
        Assert.Equal(expectedDrawOutput, _sut._drawOutput);
        Assert.Equal(expectedSetupOutput, _sut._setupOutput);
        Assert.Equal(expectedMainOutput, _sut._mainOutput);
        Assert.Equal(expectedOutput, _sut._output);
    }
}


public class ProgramNodeCodeGenTestData : IEnumerable<object[]>
{
  private string _stdlibPath = "../../../../ALFA/bin/Debug/net7.0/CodeGen-p5.js/stdlib.js";
  public IEnumerator<object[]> GetEnumerator()
  {
    //Tests the output of a program with no statements
    string path = "../../ALFA/CodeGen-p5.js/Output/stdlib.js";
    string baseVarOutput = File.ReadAllText(_stdlibPath) + "\n\n";

    string baseDrawOutput = "function draw() {\n\tbackground(255)\n\n\tfor (const shape of shapesToDraw) {\n\t\tshape.draw()\n\t}\n}";
    string baseSetupOutput = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n";
    string baseMainOutput = "\nasync function main() {\n";
    string closeBaseMainOutput = "\r}\n";
    string mainOutputNothingAdded = baseMainOutput + closeBaseMainOutput;
    ProgramNode programNodeTestStdLib = new ProgramNode(new List<Node>());
    SymbolTable symbolTableProgramNode = new SymbolTable();

    string output = baseVarOutput + mainOutputNothingAdded + baseSetupOutput + baseDrawOutput;
    
    yield return new object[]
    {
      programNodeTestStdLib, symbolTableProgramNode, baseVarOutput, baseSetupOutput, baseDrawOutput, output, mainOutputNothingAdded
    };
    
    
    //Tests the output of a program with a VarDclNode Statement with a NumNode child
    AssignStmtNode assStmtNode = new AssignStmtNode("num1", new NumNode(300, 13, 2), 25, 20);
    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.@int ,assStmtNode, 25, 20);
    string varOutputWithVarDcl = File.ReadAllText(_stdlibPath);
    varOutputWithVarDcl += "\n\n";
    string setupOutputWithVarDcl = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n";
    List<Node> programNodeWithVarDclStatements = new List<Node>()
    {
      varDclNodeNumChild
    };
    
    ProgramNode programNodeWithVarDcl = new ProgramNode(programNodeWithVarDclStatements);
    SymbolTable symbolTableProgramNodeWithVarDcl = new SymbolTable();

    string outputWithVarDcl = varOutputWithVarDcl + mainOutputNothingAdded + setupOutputWithVarDcl + baseDrawOutput ;
    
    yield return new object[]
    {
      programNodeWithVarDcl, symbolTableProgramNodeWithVarDcl, varOutputWithVarDcl, setupOutputWithVarDcl, baseDrawOutput, outputWithVarDcl, mainOutputNothingAdded
    };

    
    //Tests VarDclNode where the child is a BuiltInCreateShapeCallNode
    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    BuiltInCreateShapeCallNode buildInAnimCallNodeCreateRect = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect,numNodesCreateRect, 15, 10);

    
    SymbolTable symbolTableProgramRect = new SymbolTable();
    AssignStmtNode assStmtNodeRect = new AssignStmtNode("Rect1", buildInAnimCallNodeCreateRect, 25, 20);
    VarDclNode varDclNodeRect = new VarDclNode(ALFATypes.TypeEnum.rect, assStmtNodeRect, 25, 20);
    string varOutputRect = File.ReadAllText(_stdlibPath);
    varOutputRect += "\n\n";
    string setupOutputRect = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n";
    string outputRect = varOutputRect + mainOutputNothingAdded + setupOutputRect + baseDrawOutput ;
    List<Node> programNodeWithRectStatements = new List<Node>()
    {
      varDclNodeRect
    };
    ProgramNode programNodeRect = new ProgramNode(programNodeWithRectStatements);

    yield return new object[]
    {
      programNodeRect, symbolTableProgramRect, varOutputRect, setupOutputRect, baseDrawOutput, outputRect, mainOutputNothingAdded
    };

    IdNode rect = new IdNode("Rect1", 30, 30);

    List<Node> numNodesMove = new List<Node>()
    {
      rect,
      new NumNode(200, 60, 20),
      new NumNode(0, 60, 20),
      new NumNode(4000, 60, 20),
    };
    BuiltInAnimCallNode buildInAnimCallNodeMove = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMove, 20, 15);


    string varOutputRectAndMove = File.ReadAllText(_stdlibPath);
    varOutputRectAndMove += "\n\n";
    
    string setupOutputRectAndMove = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n";
    List<Node> programNodeWithRectStatementsAndMove = new List<Node>()
    {
      varDclNodeRect, buildInAnimCallNodeMove
    };
    
    

    ProgramNode programNodeRectAndMove = new ProgramNode(programNodeWithRectStatementsAndMove);
    SymbolTable symbolTableProgramNodeRectAndMove = new SymbolTable();
    string mainOutputRectAndMove = baseMainOutput + "await Rect1.move(200, 0, 4000);\n\t\r}\n";
    string outputRectAndMove = varOutputRectAndMove + mainOutputRectAndMove + setupOutputRectAndMove + baseDrawOutput;
    
    yield return new object[]
    {
      programNodeRectAndMove, symbolTableProgramNodeRectAndMove, varOutputRectAndMove, setupOutputRectAndMove, baseDrawOutput, outputRectAndMove, mainOutputRectAndMove
    };
    
    /* Tests that a program with a move and a wait call works as expected */
    IdNode idNode1TwoFunc = new IdNode("Rect1", 20, 10);
    List<Node> numNodesMoveTwoFunc = new List<Node>()
    {
      idNode1TwoFunc,
      new NumNode(200, 60, 20),
      new NumNode(0, 60, 20),
      new NumNode(4000, 60, 20)
    };
    
    List<Node> numNodesWaitTwoFunc = new List<Node>()
    {
      new NumNode(300, 60, 20)
    };
    
    BuiltInAnimCallNode buildInAnimCallNodeMoveTwoFunc = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMoveTwoFunc, 20, 15);
    BuiltInAnimCallNode builtInAnimCallNodeWait = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.wait, numNodesWaitTwoFunc, 25, 10);
    
    List<Node> programNodeWithRectStatementsAndMoveTwoFunc = new List<Node>()
    {
      varDclNodeRect, buildInAnimCallNodeMoveTwoFunc, builtInAnimCallNodeWait
    };

    ProgramNode programNodeRectAndMoveTwoFunc = new ProgramNode(programNodeWithRectStatementsAndMoveTwoFunc);
    SymbolTable symbolTableProgramNodeRectAndMoveTwoFunc = new SymbolTable();

    
    string varOutputRectAndMoveTwoFunc = File.ReadAllText(_stdlibPath);
    varOutputRectAndMoveTwoFunc += "\n\nconst Rect1 = new Rect(100,100,100,100);\n";
    string setupOutputRectAndMoveTwoFunc = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tmain();\n}\n\n";
    string mainOutputRectMoveAndWait = baseMainOutput + "await Rect1.move(200, 0, 4000);\n\tawait wait(300);\n\t\r}\n";
    string outputRectAndMoveTwoFunc = varOutputRectAndMoveTwoFunc + mainOutputRectMoveAndWait + setupOutputRectAndMoveTwoFunc + baseDrawOutput;
    yield return new object[]
    {
      programNodeRectAndMoveTwoFunc, symbolTableProgramNodeRectAndMoveTwoFunc, varOutputRectAndMoveTwoFunc, 
      setupOutputRectAndMoveTwoFunc, baseDrawOutput, outputRectAndMoveTwoFunc, mainOutputRectMoveAndWait
    };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

}