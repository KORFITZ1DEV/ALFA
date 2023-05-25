//Additions: strings, colours and new shapes (lines, triangles, circles).
grammar ALFA4;

prog: stmt+ EOF;

stmt
    : varDcl
    | assignStmt
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    | animDcl           //Only allowed at the top level (NOT allowed in blocks).
    | animCall
    ;
    
varDcl: type assignStmt;

assignStmt: <assoc=right> ID '=' (builtInCreateShapeCall | expr) ';';

ifStmt: 'if' '(' expr ')' block ('else if' '(' expr ')' block)* ('else' block)?;

loopStmt: 'loop' '(' 'int' ID 'from' expr '..' expr ')' block;

paralStmt: 'paral' paralBlock;

animDcl: 'animation' ID '(' formalParams ')' block; 

expr
    : '(' expr ')'                                  #Parens
    | '!' expr                                      #Not
    | '-' expr                                      #UnaryMinus    
    | expr op=('*' | '/' | '%') expr                #MulDiv
    | expr op=('+' | '-') expr                      #AddSub
    | expr op=('<' | '>' | '<=' | '>=') expr        #Relational
    | expr op=('==' | '!=') expr                    #Equality
    | expr 'and' expr                               #And
    | expr 'or' expr                                #Or
    | ID                                            #Id
    | NUM                                           #Num
    | STRING                                        #String
    | bool                                          #Boolean
    | color                                         #Colour
    ;

block: '{' blockStmt* '}';
blockStmt
    : varDcl
    | assignStmt
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    | animCall
    ;

paralBlock: '{' paralBlockStmt* '}';
paralBlockStmt
    : builtInParalAnimCall
    | animCall
    ;

builtInAnim: 'move' | 'wait' | 'color';
builtInAnimCall: builtInAnim '(' actualParams ')' ';';

builtInCreateShape: 'createRect' | 'createCircle' | 'createTriangle' | 'createLine' | 'createText';
builtInCreateShapeCall: builtInCreateShape '(' actualParams ')';

builtInParalAnim: 'move' | 'color';
builtInParalAnimCall: builtInParalAnim '(' actualParams ')' ';';

animCall: ID '(' actualParams ')' ';';

formalParams: (type ID (',' type ID)*)?;                                             
actualParams: (expr (',' expr)*)?;

COMMENT: '#' ~[\r\n]* -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
STRING: '"' (~["\r\n\\] | '\\' [\\"])* '"';
type: 'int' | 'bool' | 'rect' | 'string' | 'circle' | 'triangle' | 'line' | 'color' | 'text';
bool: 'true' | 'false';
color: 'white' | 'black' | 'red' | 'green' | 'blue';

WS: [ \t\r\n]+ -> skip;