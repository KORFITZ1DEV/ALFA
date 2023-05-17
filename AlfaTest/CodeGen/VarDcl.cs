using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using ALFA.Visitors;

namespace AlfaTest.CodeGen;

public class VarDclTest
{
    private CodeGenVisitor _sut;

    
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
}

public class VarDclCodeGenTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        List<Node> numNodesCreateRect = new List<Node>()
        {
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20)
        };
        BuiltInCreateShapeCallNode buildInAnimCallNodeCreateRect = new BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum.createRect, numNodesCreateRect, 15, 10);
        VarDclNode varDclNodeNumChild = new VarDclNode(ALFATypes.TypeEnum.rect, "rect1" ,buildInAnimCallNodeCreateRect, 25, 20);
        SymbolTable symbolTableVarDclNode = new SymbolTable();
        yield return new object[]
        {
            varDclNodeNumChild, symbolTableVarDclNode, "const rect1 = new Rect(100,100,100,100);\n", "\trect1.draw();\n"
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}