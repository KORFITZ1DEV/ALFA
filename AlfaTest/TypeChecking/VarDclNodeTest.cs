using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.TypeChecking;

public class VarDclNodeTest
{
    private TypeCheckVisitor _sut;
    
    
    [Theory]
    [ClassData(typeof(VarDclNodeTestData))]
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

public class VarDclNodeTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var symbolTable1 = new SymbolTable();
        var buildInNode1 = new BuiltInsNode(ALFATypes.BuiltInTypeEnum.createRect, FormalParameters.FormalParams["createRect"].FormalParams, 50, 25);
        var args1 = new List<Node>()
        {
            new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22) 
        };

        var varDclValueNode1 = new FuncCallNode(buildInNode1, args1, 50, 25);
        var varDclNode1 = new VarDclNode(ALFATypes.TypeEnum.@int, "test1", varDclValueNode1, 25, 25);

        var symbolTable2 = new SymbolTable();
        var varDclValueNode2 = new NumNode(25, 20, 10);
        var varDclNode2 = new VarDclNode(ALFATypes.TypeEnum.rect, "test2", varDclValueNode2, 25, 30);
        yield return new object[]{varDclNode1, new TypeException($"Invalid type {varDclNode1.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {varDclNode1.Line}:{varDclNode1.Col}"), symbolTable1};
        yield return new object[]{varDclNode2, new TypeException($"Invalid type {varDclNode2.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {varDclNode2.Line}:{varDclNode2.Col}"), symbolTable2};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}