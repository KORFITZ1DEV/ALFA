﻿using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using System.Diagnostics;

MyParseMethod();

void MyParseMethod()
{
      String input = File.ReadAllText("../../../srcprograms/mvptypes.alfa");
      ICharStream stream = CharStreams.fromString(input);
      ITokenSource lexer = new ALFALexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      ALFAParser parser = new ALFAParser(tokens);
      parser.BuildParseTree = true;
      IParseTree tree = parser.program();
      
      SymbolTable symbolTable = new();
      BuildASTVisitor visitor = new BuildASTVisitor(symbolTable);
      Node ast = visitor.Visit(tree);
      TypeCheckVisitor typeCheckVisitor = new TypeCheckVisitor(symbolTable);
      typeCheckVisitor.Visit(ast);
      //ASTPrintVisitor astPrintVisitor = new ASTPrintVisitor();
      //astPrintVisitor.Visit(ast);
      CodeGenVisitor codeGenVisitor = new CodeGenVisitor(symbolTable, "../../../Output/sketch.js");
      codeGenVisitor.Visit(ast);
}

