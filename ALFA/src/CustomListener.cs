using Antlr4.Runtime.Misc;
using System.Diagnostics;

public class CustomListener : ALFABaseListener
{
    public override void EnterCreateStmt([NotNull] ALFAParser.CreateStmtContext context)
    {
        Console.WriteLine("Creat statement: " + context.GetText());
    }

    public override void EnterMoveStmt([NotNull] ALFAParser.MoveStmtContext context)
    {
        Console.WriteLine("Move statement" + context.GetText());
    }

    public override void EnterWaitStmt([NotNull] ALFAParser.WaitStmtContext context)
    {
        Console.WriteLine("wait statement" + context.GetText()); 
    }
    
}