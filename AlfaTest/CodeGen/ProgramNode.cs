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
        string expectedVarOutput, string expectedSetupOutput, string expectedDrawOutput, string expectedOutput)
    {
        _sut = new CodeGenVisitor(symbolTable, "../TestOutput");
        _sut._path = "../../../../ALFA/bin/Debug/net7.0/CodeGen-p5.js/";

        _sut.Visit(node);
    
        Assert.Equal(expectedVarOutput, _sut._varOutput);
        Assert.Equal(expectedDrawOutput, _sut._drawOutput);
        Assert.Equal(expectedSetupOutput, _sut._setupOutput);
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
    string baseVarOutput = File.ReadAllText(_stdlibPath) + "\n\nconst seqAnim = new SeqAnimation([anim_0]);\n";

    string baseDrawOutput = "function draw() {\n\tbackground(255)\n\tseqAnim.play();\n}";
    string baseSetupOutput = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tstartTime = millis()\n}\n\n";
    
    ProgramNode programNodeTestStdLib = new ProgramNode(new List<Node>());
    SymbolTable symbolTableProgramNode = new SymbolTable();

    string output = baseVarOutput + baseSetupOutput + baseDrawOutput;
    
    yield return new object[]
    {
      programNodeTestStdLib, symbolTableProgramNode, baseVarOutput, baseSetupOutput, baseDrawOutput, output
    };
    
    //Tests the output of a program with a VarDclNode Statement with a NumNode child
    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.@int, "num1" ,new NumNode(300, 13,2), 25, 20);
    string varOutputWithVarDcl = File.ReadAllText(_stdlibPath);
    varOutputWithVarDcl += "\n\nconst num1 = 300";
    varOutputWithVarDcl += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    string drawOutputWithVarDcl = "function draw() {\n\tbackground(255)\n\tseqAnim.play();\n}";
    string setupOutputWithVarDcl = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tstartTime = millis()\n}\n\n";
    List<Node> programNodeWithVarDclStatements = new List<Node>()
    {
      varDclNodeNumChild
    };
    
    ProgramNode programNodeWithVarDcl = new ProgramNode(programNodeWithVarDclStatements);
    SymbolTable symbolTableProgramNodeWithVarDcl = new SymbolTable();

    string outputWithVarDcl = varOutputWithVarDcl + setupOutputWithVarDcl + drawOutputWithVarDcl ;
    
    yield return new object[]
    {
      programNodeWithVarDcl, symbolTableProgramNodeWithVarDcl, varOutputWithVarDcl, setupOutputWithVarDcl, drawOutputWithVarDcl, outputWithVarDcl
    };

    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    BuiltInCreateShapeCallNode buildInAnimCallNodeCreateRect = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect,numNodesCreateRect, 15, 10);

    
    SymbolTable symbolTableProgramRect = new SymbolTable();
    VarDclNode varDclNodeRect = new VarDclNode(ALFATypes.TypeEnum.rect, "Rect1" , buildInAnimCallNodeCreateRect, 25, 20);
    string varOutputRect = File.ReadAllText(_stdlibPath);
    varOutputRect += "\n\nconst Rect1 = new Rectangle(100,100,100,100);\n";
    varOutputRect += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    string drawOutputRect = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRect = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tstartTime = millis()\n}\n\n";
    string outputRect = varOutputRect + setupOutputRect + drawOutputRect ;
    List<Node> programNodeWithRectStatements = new List<Node>()
    {
      varDclNodeRect
    };
    ProgramNode programNodeRect = new ProgramNode(programNodeWithRectStatements);

    yield return new object[]
    {
      programNodeRect, symbolTableProgramRect, varOutputRect, setupOutputRect, drawOutputRect, outputRect
    };

    IdNode idNode1 = new IdNode("Rect1", 20, 10);
    List<Node> numNodesMove = new List<Node>()
    {
      idNode1,
      new NumNode(200, 60, 20),
      new NumNode(4000, 60, 20),
    };
       
    BuiltInAnimCallNode buildInAnimCallNodeMove = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMove, 20, 15);


    string varOutputRectAndMove = File.ReadAllText(_stdlibPath);
    varOutputRectAndMove += "\n\nconst Rect1 = new Rectangle(100,100,100,100);";
    varOutputRectAndMove += "\n\nconst anim_0 = new MoveAnimation(Rect1,200,4000);";
    varOutputRectAndMove += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    
    string drawOutputRectAndMove = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRectAndMove = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tstartTime = millis()\n}\n\n";
    List<Node> programNodeWithRectStatementsAndMove = new List<Node>()
    {
      varDclNodeRect, buildInAnimCallNodeMove
    };
    
    

    ProgramNode programNodeRectAndMove = new ProgramNode(programNodeWithRectStatementsAndMove);
    SymbolTable symbolTableProgramNodeRectAndMove = new SymbolTable();

    string outputRectAndMove = varOutputRectAndMove + setupOutputRectAndMove + drawOutputRectAndMove;
    
    yield return new object[]
    {
      programNodeRectAndMove, symbolTableProgramNodeRectAndMove, varOutputRectAndMove, setupOutputRectAndMove, drawOutputRectAndMove, outputRectAndMove
    };
    
    IdNode idNode1TwoFunc = new IdNode("Rect1", 20, 10);
    List<Node> numNodesMoveTwoFunc = new List<Node>()
    {
      idNode1TwoFunc,
      new NumNode(200, 60, 20),
      new NumNode(4000, 60, 20)
    };
    
    List<Node> numNodesWaitTwoFunc = new List<Node>()
    {
      new NumNode(300, 60, 20)
    };
    
    BuiltInAnimCallNode buildInAnimCallNodeMoveTwoFunc = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMoveTwoFunc, 20, 15);
    BuiltInAnimCallNode builtInAnimCallNodeWait = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.wait, numNodesWaitTwoFunc, 25, 10);

    string varOutputRectAndMoveTwoFunc = File.ReadAllText(_stdlibPath);
    varOutputRectAndMoveTwoFunc += "\n\nconst Rect1 = new Rectangle(100,100,100,100);";
    varOutputRectAndMoveTwoFunc += "\n\nconst anim_0 = new MoveAnimation(Rect1,200,4000);";
    varOutputRectAndMoveTwoFunc += "\nconst anim_1 = new WaitAnimation(300);";
    varOutputRectAndMoveTwoFunc += "\nconst seqAnim = new SeqAnimation([anim_0,anim_1]);\n";

    string drawOutputRectAndMoveTwoFunc = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRectAndMoveTwoFunc = "\nfunction setup() {\n\tcreateCanvas(1000, 1000)\n\tstartTime = millis()\n}\n\n";
    List<Node> programNodeWithRectStatementsAndMoveTwoFunc = new List<Node>()
    {
      varDclNodeRect, buildInAnimCallNodeMoveTwoFunc, builtInAnimCallNodeWait
    };

    ProgramNode programNodeRectAndMoveTwoFunc = new ProgramNode(programNodeWithRectStatementsAndMoveTwoFunc);
    SymbolTable symbolTableProgramNodeRectAndMoveTwoFunc = new SymbolTable();

    string outputRectAndMoveTwoFunc = varOutputRectAndMoveTwoFunc + setupOutputRectAndMoveTwoFunc + drawOutputRectAndMoveTwoFunc;

    yield return new object[]
    {
      programNodeRectAndMoveTwoFunc, symbolTableProgramNodeRectAndMoveTwoFunc, varOutputRectAndMoveTwoFunc, setupOutputRectAndMoveTwoFunc, drawOutputRectAndMoveTwoFunc, outputRectAndMoveTwoFunc
    };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

}