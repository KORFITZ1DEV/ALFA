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
    public void VarDclNodeThrowsTypeExceptionWhenAssignedWrongType(Node varDclNode, Exception expectedException, SymbolTable symbolTable)
    {
        _sut = new TypeCheckVisitor(symbolTable);

        try
        {
            _sut.Visit(varDclNode);
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
        var args1 = new List<Node>()
        {
            new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22), new NumNode(0, 21, 22) 
        };
        var buildInNode1 = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, args1, 50, 25);

        AssignStmtNode assStmt1 = new AssignStmtNode("test1", buildInNode1, 25, 25);
        var varDclNode1 = new VarDclNode(ALFATypes.TypeEnum.@int, assStmt1, 25, 25);

        var symbolTable2 = new SymbolTable();
        var varDclValueNode2 = new NumNode(25, 20, 10);
        AssignStmtNode assStmt2 = new AssignStmtNode("test2", varDclValueNode2, 25, 25);
        var varDclNode2 = new VarDclNode(ALFATypes.TypeEnum.rect, assStmt2, 25, 30);
        
        yield return new object[]{varDclNode1, new TypeException($"Invalid type {varDclNode1.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {varDclNode1.Line}:{varDclNode1.Col}"), symbolTable1};
        yield return new object[]{varDclNode2, new TypeException($"Invalid type {varDclNode2.Type.ToString()}, expected type {ALFATypes.TypeEnum.@int} on line {varDclNode2.Line}:{varDclNode2.Col}"), symbolTable2};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}