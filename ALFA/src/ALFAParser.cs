//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ALFA.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public partial class ALFAParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, ID=9, 
		INT=10, WS=11;
	public const int
		RULE_start = 0, RULE_program = 1, RULE_statement = 2, RULE_createStmt = 3, 
		RULE_moveStmt = 4, RULE_waitStmt = 5;
	public static readonly string[] ruleNames = {
		"start", "program", "statement", "createStmt", "moveStmt", "waitStmt"
	};

	private static readonly string[] _LiteralNames = {
		null, "'='", "'createSquare'", "'('", "','", "')'", "';'", "'move'", "'wait'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, "ID", "INT", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "ALFA.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static ALFAParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public ALFAParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public ALFAParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class StartContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ProgramContext program() {
			return GetRuleContext<ProgramContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(ALFAParser.Eof, 0); }
		public StartContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_start; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterStart(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitStart(this);
		}
	}

	[RuleVersion(0)]
	public StartContext start() {
		StartContext _localctx = new StartContext(Context, State);
		EnterRule(_localctx, 0, RULE_start);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 12;
			program();
			State = 13;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ProgramContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public StatementContext[] statement() {
			return GetRuleContexts<StatementContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public StatementContext statement(int i) {
			return GetRuleContext<StatementContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterProgram(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitProgram(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 2, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 16;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 15;
				statement();
				}
				}
				State = 18;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & 896L) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StatementContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public CreateStmtContext createStmt() {
			return GetRuleContext<CreateStmtContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public MoveStmtContext moveStmt() {
			return GetRuleContext<MoveStmtContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public WaitStmtContext waitStmt() {
			return GetRuleContext<WaitStmtContext>(0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statement; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterStatement(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitStatement(this);
		}
	}

	[RuleVersion(0)]
	public StatementContext statement() {
		StatementContext _localctx = new StatementContext(Context, State);
		EnterRule(_localctx, 4, RULE_statement);
		try {
			State = 23;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case ID:
				EnterOuterAlt(_localctx, 1);
				{
				State = 20;
				createStmt();
				}
				break;
			case T__6:
				EnterOuterAlt(_localctx, 2);
				{
				State = 21;
				moveStmt();
				}
				break;
			case T__7:
				EnterOuterAlt(_localctx, 3);
				{
				State = 22;
				waitStmt();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CreateStmtContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(ALFAParser.ID, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] INT() { return GetTokens(ALFAParser.INT); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT(int i) {
			return GetToken(ALFAParser.INT, i);
		}
		public CreateStmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_createStmt; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterCreateStmt(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitCreateStmt(this);
		}
	}

	[RuleVersion(0)]
	public CreateStmtContext createStmt() {
		CreateStmtContext _localctx = new CreateStmtContext(Context, State);
		EnterRule(_localctx, 6, RULE_createStmt);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 25;
			Match(ID);
			State = 26;
			Match(T__0);
			State = 27;
			Match(T__1);
			State = 28;
			Match(T__2);
			State = 29;
			Match(INT);
			State = 30;
			Match(T__3);
			State = 31;
			Match(INT);
			State = 32;
			Match(T__3);
			State = 33;
			Match(INT);
			State = 34;
			Match(T__3);
			State = 35;
			Match(INT);
			State = 36;
			Match(T__4);
			State = 37;
			Match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class MoveStmtContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(ALFAParser.ID, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] INT() { return GetTokens(ALFAParser.INT); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT(int i) {
			return GetToken(ALFAParser.INT, i);
		}
		public MoveStmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_moveStmt; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterMoveStmt(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitMoveStmt(this);
		}
	}

	[RuleVersion(0)]
	public MoveStmtContext moveStmt() {
		MoveStmtContext _localctx = new MoveStmtContext(Context, State);
		EnterRule(_localctx, 8, RULE_moveStmt);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 39;
			Match(T__6);
			State = 40;
			Match(T__2);
			State = 41;
			Match(ID);
			State = 42;
			Match(T__3);
			State = 43;
			Match(INT);
			State = 44;
			Match(T__3);
			State = 45;
			Match(INT);
			State = 46;
			Match(T__4);
			State = 47;
			Match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class WaitStmtContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT() { return GetToken(ALFAParser.INT, 0); }
		public WaitStmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_waitStmt; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.EnterWaitStmt(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IALFAListener typedListener = listener as IALFAListener;
			if (typedListener != null) typedListener.ExitWaitStmt(this);
		}
	}

	[RuleVersion(0)]
	public WaitStmtContext waitStmt() {
		WaitStmtContext _localctx = new WaitStmtContext(Context, State);
		EnterRule(_localctx, 10, RULE_waitStmt);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 49;
			Match(T__7);
			State = 50;
			Match(T__2);
			State = 51;
			Match(INT);
			State = 52;
			Match(T__4);
			State = 53;
			Match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,11,56,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,1,0,1,0,1,0,
		1,1,4,1,17,8,1,11,1,12,1,18,1,2,1,2,1,2,3,2,24,8,2,1,3,1,3,1,3,1,3,1,3,
		1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,
		4,1,4,1,5,1,5,1,5,1,5,1,5,1,5,1,5,0,0,6,0,2,4,6,8,10,0,0,52,0,12,1,0,0,
		0,2,16,1,0,0,0,4,23,1,0,0,0,6,25,1,0,0,0,8,39,1,0,0,0,10,49,1,0,0,0,12,
		13,3,2,1,0,13,14,5,0,0,1,14,1,1,0,0,0,15,17,3,4,2,0,16,15,1,0,0,0,17,18,
		1,0,0,0,18,16,1,0,0,0,18,19,1,0,0,0,19,3,1,0,0,0,20,24,3,6,3,0,21,24,3,
		8,4,0,22,24,3,10,5,0,23,20,1,0,0,0,23,21,1,0,0,0,23,22,1,0,0,0,24,5,1,
		0,0,0,25,26,5,9,0,0,26,27,5,1,0,0,27,28,5,2,0,0,28,29,5,3,0,0,29,30,5,
		10,0,0,30,31,5,4,0,0,31,32,5,10,0,0,32,33,5,4,0,0,33,34,5,10,0,0,34,35,
		5,4,0,0,35,36,5,10,0,0,36,37,5,5,0,0,37,38,5,6,0,0,38,7,1,0,0,0,39,40,
		5,7,0,0,40,41,5,3,0,0,41,42,5,9,0,0,42,43,5,4,0,0,43,44,5,10,0,0,44,45,
		5,4,0,0,45,46,5,10,0,0,46,47,5,5,0,0,47,48,5,6,0,0,48,9,1,0,0,0,49,50,
		5,8,0,0,50,51,5,3,0,0,51,52,5,10,0,0,52,53,5,5,0,0,53,54,5,6,0,0,54,11,
		1,0,0,0,2,18,23
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
