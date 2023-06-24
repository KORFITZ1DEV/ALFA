using System.Collections;
using System.Diagnostics.Contracts;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.TypeChecking;

public class AnimCallNodeTest
{
  private TypeCheckVisitor _sut;

  [Theory]
  [ClassData(typeof(AnimCallNodeTestData))]
  public void AnimCallNodeThrowsException(Node AnimCallNode, Exception expectedException, SymbolTable symbolTable)
  {
    _sut = new TypeCheckVisitor(symbolTable);

    try
    {
      _sut.Visit(AnimCallNode);
    }
    catch (Exception actualException)
    {
      Assert.Equal(expectedException.GetType(), actualException.GetType());
    }
  }
  
  [Theory]
  [ClassData(typeof(AnimCallNodeTestsIfValidNumberOfParameters))]
  public void AnimCallNodeValidNumberOfParameters(Node AnimCallNode, SymbolTable symbolTable)
  {
    _sut = new TypeCheckVisitor(symbolTable);
    _sut.Visit(AnimCallNode);
    Assert.True(true);
  }
}


public class AnimCallNodeTestData : IEnumerable<object[]>
{
  public IEnumerator<object[]> GetEnumerator()
  {
    //Test for the createrect.
    var symbolTable1 = new SymbolTable();

    var args1 = new List<Node>()
    {
      new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22),
      new NumNode(0, 21, 22)
    };
    var buildInNode1 = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, args1, 50, 25);
    
    yield return new object[]
      { buildInNode1, new InvalidNumberOfArgumentsException("Invalid number of arguments"), symbolTable1 };

    var idNode = new IdNode("myrect1", 10, 15);
    var symbolTable3 = new SymbolTable();
    var symbol = new Symbol("myrect1", idNode, ALFATypes.TypeEnum.rect, 10, 5);
    symbolTable3.EnterSymbol(symbol);

    var args2 = new List<Node>()
    {
      idNode, new NumNode(1, 31, 32), new NumNode(1, 31, 32), new NumNode(1, 31, 32)
    };
    var buildInNode2 = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, args2, 20, 15);


    yield return new object[]
      { buildInNode2, new ArgumentTypeException("Invalid number of arguments"), symbolTable3 };

    var args3 = new List<Node>()
    {
      new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22)
    };
    var buildInNode3 = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, args3, 50, 25);
    
    yield return new object[]
      { buildInNode3, new InvalidNumberOfArgumentsException("Invalid number of arguments"), symbolTable3 };
    
    var args4 = new List<Node>()
    {
      new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22)
    };
    var buildInNode4 = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, args4, 50, 25);
    
    
    var symbolTable4 = new SymbolTable();
    yield return new object[]
      { buildInNode4, new ArgumentTypeException("Trying to move something that is not a rect"), symbolTable4 };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}

public class AnimCallNodeTestsIfValidNumberOfParameters : IEnumerable<object[]>
  {
    public IEnumerator<object[]> GetEnumerator()
    {
      //Check if move is valid.
      var symboltable4 = new SymbolTable();
      
      var args4 = new List<Node>()
      {
        new IdNode("myrect1", 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42)
      };
      var buildInNode4 = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.move, args4 , 15, 10);

      yield return new object[]
      {
        buildInNode4, symboltable4
      };
      
      
      //Check if Createrect is valid
      var symboltable5 = new SymbolTable();
      var args5 = new List<Node>()
      {
        new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42), new NumNode(2, 41, 42)
      };
      var buildInNode5 = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, args5, 15, 10);

      yield return new object[]
      {
        buildInNode5, symboltable5
      };
      
      
      //check if wait is valid
      var symboltable6 = new SymbolTable();
      var args6 = new List<Node>()
      {
        new NumNode(2, 41, 42)
      };
      var buildInNode6 = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.wait, args6, 15, 10);

      yield return new object[]
      {
        buildInNode6, symboltable6
      };
      
      
    
    }



  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}