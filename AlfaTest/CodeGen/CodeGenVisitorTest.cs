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
 
    List<Node> numNodesMove = new List<Node>()
    {
      idNode1,
      new NumNode(200, 60, 20),
      new NumNode(4000, 60, 20),
    };

    
    BuiltInAnimCallNode buildInAnimCallNodeMove = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMove, 20, 15);
    SymbolTable symbolTableMove = new SymbolTable();
    yield return new object[]
    {
      buildInAnimCallNodeMove, symbolTableMove, "const anim_0 = new MoveAnimation(Rect1,200,4000);\n"
    };
    
    List<Node> numNodesWait = new List<Node>()
    {
      new NumNode(100, 60, 20),
    };
    
    BuiltInAnimCallNode buildInAnimCallNodeWait = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.wait, numNodesWait, 15, 10);
    SymbolTable symbolTableWait = new SymbolTable();
    yield return new object[]
    {
      buildInAnimCallNodeWait, symbolTableWait, "const anim_0 = new WaitAnimation(100);\n"
    };

    
    List<Node> numNodesCreateRect = new List<Node>()
    {
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20),
      new NumNode(100, 60, 20)
    };
    BuiltInCreateShapeCallNode buildInAnimCallNodeCreateRect = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, numNodesCreateRect, 15, 10);
    SymbolTable symbolTableCreateRect = new SymbolTable();
    yield return new object[]
    {
      buildInAnimCallNodeCreateRect, symbolTableCreateRect, "new Rect(100,100,100,100);\n"
    };


    SymbolTable symbolTableFuncCallWait = new SymbolTable();
    yield return new object[]
    {
      buildInAnimCallNodeWait, symbolTableFuncCallWait, "const anim_0 = new WaitAnimation(100);\n"
    };
    
    SymbolTable symbolTableFunccallMove = new SymbolTable();
    yield return new object[]
    {
      buildInAnimCallNodeMove, symbolTableFunccallMove, "const anim_0 = new MoveAnimation(Rect1,200,4000);\n"
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




