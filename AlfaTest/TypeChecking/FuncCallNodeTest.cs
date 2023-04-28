using System.Collections;
using System.Diagnostics.Contracts;
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
    //Test for the createrect.
    var symbolTable1 = new SymbolTable();

    var formalParams1 = FormalParameters.FormalParams["createRect"].FormalParams;
    var buildInNode1 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, formalParams1, 50, 25);
    var args1 = new List<Node>()
    {
      new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22),
      new NumNode(0, 21, 22)
    };

    var symbolTable2 = new SymbolTable();
    var formalParams2 = FormalParameters.FormalParams["createRect"].FormalParams;
    var buildInNode2 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, formalParams2, 30, 10);
    var args2 = new List<Node>()
    {
      new NumNode(1, 31, 32), new NumNode(1, 31, 32), new NumNode(1, 31, 32)
    };
    var idNode = new IdNode("myrect1", 10, 15);
    var symbolTable3 = new SymbolTable();
    var symbol = new Symbol("myrect1", idNode, ALFATypes.TypeEnum.rect, 10, 5);
    var formalParams3 = FormalParameters.FormalParams["createRect"].FormalParams;
    symbolTable3.EnterSymbol(symbol);
    var buildInNode3 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, formalParams3, 20, 15);
    var args3 = new List<Node>()
    {
      idNode, new NumNode(1, 31, 32), new NumNode(1, 31, 32), new NumNode(1, 31, 32)
    };

    FuncCallNode funcCallNode1 = new FuncCallNode(buildInNode1, args1, 50, 25);
    FuncCallNode funcCallNode2 = new FuncCallNode(buildInNode2, args2, 30, 10);
    FuncCallNode funcCallNode3 = new FuncCallNode(buildInNode3, args3, 30, 15);
    yield return new object[]
      { funcCallNode1, new InvalidNumberOfArgumentsException("Invalid number of arguments"), symbolTable1 };
    yield return new object[]
      { funcCallNode2, new InvalidNumberOfArgumentsException("Invalid number of arguments"), symbolTable2 };
    yield return new object[] { funcCallNode3, new ArgumentTypeException("Invalid type in arguments"), symbolTable3 };


  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

public class FuncCallNodeTestsIfValid : IEnumerable<object[]>
  {
    public IEnumerator<object[]> GetEnumerator()
    {
      //Check if move is valid.
      var symboltable4 = new SymbolTable();
      var formalparams4 = FormalParameters.FormalParams["move"].FormalParams;
      var buildInNode4 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.move, formalparams4, 15, 10);
      var args4 = new List<Node>()
      {
        new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42)
      };
      FuncCallNode funcCallNode4 = new FuncCallNode(buildInNode4, args4, 10, 3);
      yield return new object[]
      {
        funcCallNode4, symboltable4
      };
      
      
      //Check if Createrect is valid
      var symboltable5 = new SymbolTable();
      var formalparams5 = FormalParameters.FormalParams["createRect"].FormalParams;
      var buildInNode5 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, formalparams5, 15, 10);
      var args5 = new List<Node>()
      {
        new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42)
      };
      FuncCallNode funcCallNode5 = new FuncCallNode(buildInNode5, args5, 10, 3);
      yield return new object[]
      {
        funcCallNode5, symboltable5
      };
      
      
      //check if wait is valid
      var symboltable6 = new SymbolTable();
      var formalparams6 = FormalParameters.FormalParams["wait"].FormalParams;
      var buildInNode6 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.wait, formalparams6, 15, 10);
      var args6 = new List<Node>()
      {
        new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42)
      };
      FuncCallNode funcCallNode6 = new FuncCallNode(buildInNode6, args6, 10, 3);
      yield return new object[]
      {
        funcCallNode6, symboltable6
      };
      
      
    
    }



  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}