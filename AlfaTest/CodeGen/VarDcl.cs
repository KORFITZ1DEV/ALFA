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
    
        BuiltInAnimCallNode buildInAnimCallNodeCreateRect = new BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum.createRect,
            FormalParameters.FormalParams["createRect"].FormalParams, 15, 10);
        List<Node> numNodesCreateRect = new List<Node>()
        {
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20),
            new NumNode(100, 60, 20)
        };
        FuncCallNode funcCallNodeCreateRect = new FuncCallNode(buildInAnimCallNodeCreateRect, numNodesCreateRect, 10, 25);
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