using System.Collections;
using System.Diagnostics.Contracts;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;
using AlfaTest.TypeChecking;

namespace AlfaTest.CodeGen;

public class CodeGenTest
{
  private CodeGenVisitor _sut;

  
  [Theory]
  [ClassData(typeof(CodeGenVisitorTestData))]
  public void WritesCorrectlyToVarOutput(Node node, SymbolTable symbolTable, string expectedOutput)
  {
    _sut = new CodeGenVisitor(symbolTable, "../TestOutput");

    _sut.Visit(node);
    
    Assert.Equal(expectedOutput, _sut._varOutput);
  }

  [Theory]
  [ClassData(typeof(VarDclCodeGenTestData))]
  public void VarDclNodeWithCreateRectChildWritesToDrawOutput(Node node, SymbolTable symbolTable,
    string expectedVarOutput, string expectedDrawOutput)
  {
    _sut = new CodeGenVisitor(symbolTable, "../TestOutput");

    _sut.Visit(node);
    
    Assert.Equal(expectedVarOutput, _sut._varOutput);
    Assert.Equal(expectedDrawOutput, _sut._drawOutput);

  }
  
  [Theory]
  [ClassData(typeof(ProgramNodeCodeGenTestData))]
  public void ProgramNodeWritesToTheDifferentOutputs(Node node, SymbolTable symbolTable,
    string expectedVarOutput, string expectedSetupOutput, string expectedDrawOutput, string expectedOutput)
  {
    _sut = new CodeGenVisitor(symbolTable, "../TestOutput");
    _sut._path = "../../../../ALFA/CodeGen-p5.js";

    _sut.Visit(node);
    
    Assert.Equal(expectedVarOutput, _sut._varOutput);
    Assert.Equal(expectedDrawOutput, _sut._drawOutput);
    Assert.Equal(expectedSetupOutput, _sut._setupOutput);
    Assert.Equal(expectedOutput, _sut._output);



  }
}


public class CodeGenVisitorTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    NumNode numNode1 = new NumNode(200, 20, 20);
    SymbolTable symbolTable1 = new SymbolTable();
    yield return new object[]
    {
      numNode1, symbolTable1, "200"
    };
    
    IdNode idNode1 = new IdNode("Rect1", 20, 10);
    SymbolTable symbolTable2 = new SymbolTable();
    yield return new object[]
    {
      idNode1, symbolTable2, "Rect1"
    };
    
    BuiltInsNode buildInsNodeMove = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.move,
      FormalParameters.FormalParams["move"].FormalParams, 20, 15);
    SymbolTable symbolTableMove = new SymbolTable();
    yield return new object[]
    {
      buildInsNodeMove, symbolTableMove, "const anim_0 = new MoveAnimation("
    };
    
    BuiltInsNode buildInsNodeWait = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.wait,
      FormalParameters.FormalParams["wait"].FormalParams, 15, 10);
    SymbolTable symbolTableWait = new SymbolTable();
    yield return new object[]
    {
      buildInsNodeWait, symbolTableWait, "const anim_0 = new WaitAnimation("
    };

    BuiltInsNode buildInsNodeCreateRect = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect,
      FormalParameters.FormalParams["createRect"].FormalParams, 15, 10);
    SymbolTable symbolTableCreateRect = new SymbolTable();
    yield return new object[]
    {
      buildInsNodeCreateRect, symbolTableCreateRect, "new Rectangle("
    };
    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    
    FuncCallNode funcCallNodeCreateRect = new FuncCallNode(buildInsNodeCreateRect, numNodesCreateRect, 10, 25);
    SymbolTable symbolTableFuncCallCreateRect = new SymbolTable();
    yield return new object[]
    {
      funcCallNodeCreateRect, symbolTableFuncCallCreateRect, "new Rectangle(100,100,100,100);\n"
    };
    
    List<Node> numNodesWait = new List<Node>()
    {
      new NumNode(100, 60, 20),
    };
    FuncCallNode funcCallNodeWait = new FuncCallNode(buildInsNodeWait, numNodesWait, 10, 25);
    SymbolTable symbolTableFuncCallWait = new SymbolTable();
    yield return new object[]
    {
      funcCallNodeWait, symbolTableFuncCallWait, "const anim_0 = new WaitAnimation(100);\n"
    };
    
    List<Node> numNodesMove = new List<Node>()
    {
      idNode1,
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
    };
    FuncCallNode funcCallNodeMove = new FuncCallNode(buildInsNodeMove, numNodesMove, 10, 25);
    SymbolTable symbolTableFunccallMove = new SymbolTable();
    yield return new object[]
    {
      funcCallNodeMove, symbolTableFunccallMove, "const anim_0 = new MoveAnimation(Rect1,100,100);\n"
    };

    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.@int, "num1" ,numNode1, 25, 20);
    SymbolTable symbolTableVarDclNode = new SymbolTable();
    yield return new object[]
    {
      varDclNodeNumChild, symbolTableVarDclNode, "const num1 = 200\n"
    };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

public class VarDclCodeGenTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    
    BuiltInsNode buildInsNodeCreateRect = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect,
      FormalParameters.FormalParams["createRect"].FormalParams, 15, 10);
    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    FuncCallNode funcCallNodeCreateRect = new FuncCallNode(buildInsNodeCreateRect, numNodesCreateRect, 10, 25);
    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.rect, "rect1" ,funcCallNodeCreateRect, 25, 20);
    SymbolTable symbolTableVarDclNode = new SymbolTable();
    yield return new object[]
    {
      varDclNodeNumChild, symbolTableVarDclNode, "const rect1 = new Rectangle(100,100,100,100);\n\n", "\trect1.render();\n"
    };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

public class ProgramNodeCodeGenTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    //Tests the output of a program with no statements
    string path = "../../ALFA/CodeGen-p5.js/stdlib.js";
    string baseVarOutput = File.ReadAllText("../../../../ALFA/CodeGen-p5.js/stdlib.js") + "\n\nconst seqAnim = new SeqAnimation([anim_0]);\n";

    string baseDrawOutput = "function draw() {\n\tbackground(255)\n\tseqAnim.play();\n}";
    string baseSetupOutput = "\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n";
    
    ProgramNode programNodeTestStdLib = new ProgramNode(new List<Node>());
    SymbolTable symbolTableProgramNode = new SymbolTable();

    string output = baseVarOutput + baseSetupOutput + baseDrawOutput;
    
    yield return new object[]
    {
      programNodeTestStdLib, symbolTableProgramNode, baseVarOutput, baseSetupOutput, baseDrawOutput, output
    };
    
    //Tests the output of a program with a VarDclNode Statement with a NumNode child
    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.@int, "num1" ,new NumNode(300, 13,2), 25, 20);
    string varOutputWithVarDcl = File.ReadAllText("../../../../ALFA/CodeGen-p5.js/stdlib.js");
    varOutputWithVarDcl += "\n\nconst num1 = 300";
    varOutputWithVarDcl += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    string drawOutputWithVarDcl = "function draw() {\n\tbackground(255)\n\tseqAnim.play();\n}";
    string setupOutputWithVarDcl = "\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n";
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

    BuiltInsNode buildInsNodeCreateRect = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect,
    FormalParameters.FormalParams["createRect"].FormalParams, 15, 10);
    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    
    FuncCallNode funcCallNodeCreateRect = new FuncCallNode(buildInsNodeCreateRect, numNodesCreateRect, 10, 25);
    SymbolTable symbolTableProgramRect = new SymbolTable();
    VarDclNode varDclNodeRect = new VarDclNode(ALFATypes.TypeEnum.rect, "Rect1" , funcCallNodeCreateRect, 25, 20);
    string varOutputRect = File.ReadAllText("../../../../ALFA/CodeGen-p5.js/stdlib.js");
    varOutputRect += "\n\nconst Rect1 = new Rectangle(100,100,100,100);\n";
    varOutputRect += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    string drawOutputRect = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRect = "\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n";
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

       
    BuiltInsNode buildInsNodeMove = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.move,
      FormalParameters.FormalParams["move"].FormalParams, 20, 15);
    IdNode idNode1 = new IdNode("Rect1", 20, 10);

    List<Node> numNodesMove = new List<Node>()
    {
      idNode1,
      new NumNode(200, 60, 20),
      new NumNode(4000, 60, 20),
    };
    FuncCallNode funcCallNodeMove = new FuncCallNode(buildInsNodeMove, numNodesMove, 10, 25);
    string varOutputRectAndMove = File.ReadAllText("../../../../ALFA/CodeGen-p5.js/stdlib.js");
    varOutputRectAndMove += "\n\nconst Rect1 = new Rectangle(100,100,100,100);";
    varOutputRectAndMove += "\n\nconst anim_0 = new MoveAnimation(Rect1,200,4000);";
    varOutputRectAndMove += "\nconst seqAnim = new SeqAnimation([anim_0]);\n";
    
    string drawOutputRectAndMove = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRectAndMove = "\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n";
    List<Node> programNodeWithRectStatementsAndMove = new List<Node>()
    {
      varDclNodeRect, funcCallNodeMove
    };
    
    

    ProgramNode programNodeRectAndMove = new ProgramNode(programNodeWithRectStatementsAndMove);
    SymbolTable symbolTableProgramNodeRectAndMove = new SymbolTable();

    string outputRectAndMove = varOutputRectAndMove + setupOutputRectAndMove + drawOutputRectAndMove;
    
    yield return new object[]
    {
      programNodeRectAndMove, symbolTableProgramNodeRectAndMove, varOutputRectAndMove, setupOutputRectAndMove, drawOutputRectAndMove, outputRectAndMove
    };
    
    
    BuiltInsNode buildInsNodeMoveTwoFunc = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.move,
      FormalParameters.FormalParams["move"].FormalParams, 20, 15);
    BuiltInsNode builtInsNodeWait = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.wait,
      FormalParameters.FormalParams["wait"].FormalParams, 25, 10);
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
    FuncCallNode funcCallNodeMoveTwoFunc = new FuncCallNode(buildInsNodeMoveTwoFunc, numNodesMoveTwoFunc, 10, 25);
    FuncCallNode funcCallNodeWaitTwoFunc = new FuncCallNode(builtInsNodeWait, numNodesWaitTwoFunc, 10, 20);
    string varOutputRectAndMoveTwoFunc = File.ReadAllText("../../../../ALFA/CodeGen-p5.js/stdlib.js");
    varOutputRectAndMoveTwoFunc += "\n\nconst Rect1 = new Rectangle(100,100,100,100);";
    varOutputRectAndMoveTwoFunc += "\n\nconst anim_0 = new MoveAnimation(Rect1,200,4000);";
    varOutputRectAndMoveTwoFunc += "\nconst anim_1 = new WaitAnimation(300);";
    varOutputRectAndMoveTwoFunc += "\nconst seqAnim = new SeqAnimation([anim_0,anim_1]);\n";

    string drawOutputRectAndMoveTwoFunc = "function draw() {\n\tbackground(255)\n\tRect1.render();\n\tseqAnim.play();\n}";
    string setupOutputRectAndMoveTwoFunc = "\nfunction setup() {\n\tcreateCanvas(600, 600)\n\tstartTime = millis()\n}\n\n";
    List<Node> programNodeWithRectStatementsAndMoveTwoFunc = new List<Node>()
    {
      varDclNodeRect, funcCallNodeMoveTwoFunc, funcCallNodeWaitTwoFunc
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
