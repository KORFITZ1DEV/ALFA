//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ALFA/ALFA.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ALFAParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public interface IALFAListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] ALFAParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] ALFAParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmt([NotNull] ALFAParser.StmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmt([NotNull] ALFAParser.StmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.varDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarDcl([NotNull] ALFAParser.VarDclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.varDcl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarDcl([NotNull] ALFAParser.VarDclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.assignStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignStmt([NotNull] ALFAParser.AssignStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.assignStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignStmt([NotNull] ALFAParser.AssignStmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStmt([NotNull] ALFAParser.IfStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.ifStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStmt([NotNull] ALFAParser.IfStmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.loopStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoopStmt([NotNull] ALFAParser.LoopStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.loopStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoopStmt([NotNull] ALFAParser.LoopStmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.paralStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParalStmt([NotNull] ALFAParser.ParalStmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.paralStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParalStmt([NotNull] ALFAParser.ParalStmtContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNot([NotNull] ALFAParser.NotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNot([NotNull] ALFAParser.NotContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] ALFAParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] ALFAParser.OrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MulDiv</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulDiv([NotNull] ALFAParser.MulDivContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MulDiv</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulDiv([NotNull] ALFAParser.MulDivContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddSub([NotNull] ALFAParser.AddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddSub([NotNull] ALFAParser.AddSubContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParens([NotNull] ALFAParser.ParensContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Parens</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParens([NotNull] ALFAParser.ParensContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] ALFAParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] ALFAParser.AndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Num</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNum([NotNull] ALFAParser.NumContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Num</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNum([NotNull] ALFAParser.NumContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Relational</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelational([NotNull] ALFAParser.RelationalContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Relational</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelational([NotNull] ALFAParser.RelationalContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>UnaryMinus</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryMinus([NotNull] ALFAParser.UnaryMinusContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>UnaryMinus</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryMinus([NotNull] ALFAParser.UnaryMinusContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Id</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] ALFAParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Id</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] ALFAParser.IdContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquality([NotNull] ALFAParser.EqualityContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquality([NotNull] ALFAParser.EqualityContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Boolean</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoolean([NotNull] ALFAParser.BooleanContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Boolean</c>
	/// labeled alternative in <see cref="ALFAParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoolean([NotNull] ALFAParser.BooleanContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] ALFAParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] ALFAParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.paralBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParalBlock([NotNull] ALFAParser.ParalBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.paralBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParalBlock([NotNull] ALFAParser.ParalBlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInAnim"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInAnim([NotNull] ALFAParser.BuiltInAnimContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInAnim"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInAnim([NotNull] ALFAParser.BuiltInAnimContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInAnimCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInAnimCall([NotNull] ALFAParser.BuiltInAnimCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInAnimCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInAnimCall([NotNull] ALFAParser.BuiltInAnimCallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInCreateShape"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInCreateShape([NotNull] ALFAParser.BuiltInCreateShapeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInCreateShape"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInCreateShape([NotNull] ALFAParser.BuiltInCreateShapeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInCreateShapeCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInCreateShapeCall([NotNull] ALFAParser.BuiltInCreateShapeCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInCreateShapeCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInCreateShapeCall([NotNull] ALFAParser.BuiltInCreateShapeCallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInParalAnim"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInParalAnim([NotNull] ALFAParser.BuiltInParalAnimContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInParalAnim"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInParalAnim([NotNull] ALFAParser.BuiltInParalAnimContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.builtInParalAnimCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBuiltInParalAnimCall([NotNull] ALFAParser.BuiltInParalAnimCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.builtInParalAnimCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBuiltInParalAnimCall([NotNull] ALFAParser.BuiltInParalAnimCallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.actualParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActualParams([NotNull] ALFAParser.ActualParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.actualParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActualParams([NotNull] ALFAParser.ActualParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] ALFAParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] ALFAParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ALFAParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBool([NotNull] ALFAParser.BoolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ALFAParser.bool"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBool([NotNull] ALFAParser.BoolContext context);
}
