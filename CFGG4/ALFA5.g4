//Additions: arrays, new data types, constants, built-in utilities and play statement.
grammar ALFA5;

prog: stmt+ play EOF;

stmt
    : varDcl
    | assignStmt
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    | animDcl           //Only allowed at the top level (NOT allowed in blocks).
    | animCall
    | builtInUtilCall
    ;
    
varDcl
    : <assoc=right> 'const'? type ID '=' elem ';'                                #VarDecl
    | <assoc=right> 'const'? type '[]' ID ('=' '{' elem (',' elem)* '}')? ';'    #ArrayDecl
    ;

assignStmt
    : <assoc=right> ID '=' elem ';'                 #Assign
    | <assoc=right>  ID '[' NUM ']' '=' elem        #ArrayAssign
    ;

elem: (builtInCreateShapeCall | expr);

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
    | ID '[' NUM ']'                                #ArrayAccess
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
    | builtInUtilCall
    ;

paralBlock: '{' paralBlockStmt* '}';
paralBlockStmt
    : builtInParalAnimCall
    | animCall
    ;

play: 'play' '{' blockStmt* '}';

animCall: ID '(' actualParams ')' ';';

builtInAnim: 'move' | 'wait' | 'color';
builtInAnimCall: builtInAnim '(' actualParams ')' ';';

builtInCreateShape: 'createRect' | 'createCircle' | 'createTriangle' | 'createLine' | 'createCanvas' | 'createImage' | 'createText';
builtInCreateShapeCall: builtInCreateShape '(' actualParams ')';

builtInUtil: 'add' | 'remove' | 'print' | 'resetCanvas' | 'changeDimensions' | 'import';
builtInUtilCall: builtInUtil '(' actualParams ')' ';';

builtInParalAnim: 'move' | 'color';
builtInParalAnimCall: builtInParalAnim '(' actualParams ')' ';';

formalParams: (type ID (',' type ID)*)?;                                             
actualParams: (expr (',' expr)*)?;

COMMENT: '#' ~[\r\n]* -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
STRING: '"' (~["\r\n\\] | '\\' [\\"])* '"';
type: 'int' | 'bool' | 'color' | 'rect' | 'circle' | 'triangle' | 'line' | 'shape' | 'canvas' | 'img' | 'text';
bool: 'true' | 'false';
color: 'white' | 'black' | 'red' | 'green' | 'blue';

WS: [ \t\r\n]+ -> skip; 