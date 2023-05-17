using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.CodeGen;


public class BuiltInAnimCallNodeTest
{
  private CodeGenVisitor _sut;

  [Theory]
  [ClassData(typeof(BuiltInAnimCallNodeMoveTestData))]
  public void BuiltInAnimCallNodeWritesCorrectlyToMainOutput(Node node, SymbolTable symbolTable, string expectedMainOutput)
  {
    _sut = new CodeGenVisitor(symbolTable, "../TestOutput");

    _sut.Visit(node);
    
    Assert.Equal(expectedMainOutput, _sut._mainOutput);
  }
  
}

public class BuiltInAnimCallNodeMoveTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
      
      var idNode = new IdNode("myrect1", 10, 15);

      List<Node> numNodesMove = new List<Node>()
      {
        idNode,
        new NumNode(200, 60, 20),
        new NumNode(4000, 60, 20),
      };
      
      BuiltInAnimCallNode buildInAnimCallNodeMove = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, numNodesMove, 20, 15);
      SymbolTable symbolTableMove = new SymbolTable();
      symbolTableMove.EnterSymbol(new Symbol("myrect1", idNode, ALFATypes.TypeEnum.rect, 25, 30));
      yield return new object[]
      {
        buildInAnimCallNodeMove, symbolTableMove, "await myrect1.move(200, 0, 4000);\n\t"
      };
      
      List<Node> numNodesWait = new List<Node>()
      {
        new NumNode(100, 60, 20),
      };
      
      BuiltInAnimCallNode buildInAnimCallNodeWait = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.wait, numNodesWait, 15, 10);
      SymbolTable symbolTableWait = new SymbolTable();
      yield return new object[]
      {
        buildInAnimCallNodeWait, symbolTableWait, "await wait(100);\n\t"
      };

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
} 