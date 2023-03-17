using Antlr4.Runtime.Tree;

namespace ALFA;

static class PrintVisitor
{
    public static void PrintTree(IParseTree tree)
    {
        PrintTreeRecursive(tree , "",true);
    }
    static void PrintTreeRecursive(IParseTree tree, String indent, bool last)
    {
        Console.WriteLine(indent + "+- " + tree.GetText());
        indent += last ? "   " : "|  ";

        for (int i = 0; i < tree.ChildCount; i++)
        {
            PrintTreeRecursive(tree.GetChild(i), indent, i == tree.ChildCount - 1);
        }
    }
}
