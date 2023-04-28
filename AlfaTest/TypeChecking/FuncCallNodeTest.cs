using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.TypeChecking;

public class FuncCallNodeTest
{
  private TypeCheckVisitor _sut;

  [Theory]
  [ClassData(typeof(FuncCallNodeTestData))]
  public void FunCallNodeThrowsException(Node funcCallNode, Exception expectedException, SymbolTable symbolTable)
  {
    _sut = new TypeCheckVisitor(symbolTable);

    try
    {
      _sut.Visit(funcCallNode);
    }
    catch (Exception actualException)
    {
      Assert.Equal(expectedException.GetType(), actualException.GetType());
    }
  }
}

public class FuncCallNodeTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    string Prog = "rect test2 = createRect(0, 0, 0 length, length);";
    var symbolTable1 = new SymbolTable();
    var formalParams1 = new List<ALFATypes.TypeEnum>()
    {
      ALFATypes.TypeEnum.rect
    };
    var buildInNode1 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, formalParams1, 50, 25);
    var args1 = new List<Node>()
    {
      new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22) 
    };

    FuncCallNode funcCallNode1 = new FuncCallNode(buildInNode1, args1, 50, 25);
    yield return new object[]{funcCallNode1, new InvalidNumberOfArgumentsException("Invalid number of arguments"), symbolTable1};
}

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}