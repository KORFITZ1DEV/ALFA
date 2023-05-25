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
  public void WritesCorrectlyToMainOutput(Node node, SymbolTable symbolTable, string expectedOutput)
  {
    _sut = new CodeGenVisitor(symbolTable, "../../../../ALFA/bin/Debug/net7.0/CodeGen-p5.js/");

    _sut.Visit(node);

    Assert.Equal(expectedOutput, _sut._mainOutput);
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
      idNode1, symbolTable2, "var_Rect1"
    };

    AssignStmtNode assnmt = new AssignStmtNode(idNode1.Identifier, numNode1, 25, 20);
 
    
    VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.@int, assnmt, 25, 20);
    SymbolTable symbolTableVarDclNode = new SymbolTable();
    yield return new object[]
    {
      varDclNodeNumChild, symbolTableVarDclNode, "\tlet var_Rect1=200\n"
    };
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}




