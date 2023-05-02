//Additions: built-in utilities and play statement.
grammar ALFA4;

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
    : <assoc=right> type ID '=' elem ';'                                #VarDecl
    | <assoc=right> type '[]' ID ('=' '{' elem (',' elem)* '}')? ';'    #ArrayDecl
    ;

assignStmt
    : <assoc=right> ID '=' elem ';'                                     #Assign
    | <assoc=right>  ID '[' NUM ']' '=' elem                            #ArrayAssign
    ;

elem: (builtInCreateShapeCall | expr);

ifStmt: 'if' '(' expr ')' block ('else if' '(' expr ')' block)* ('else' block)?;

loopStmt: 'loop' '(' 'int' ID 'from' (NUM | ID) '..' (NUM | ID) ')' block;

paralStmt: 'paral' paralBlock;

animDcl: 'animation' ID '(' formalParams ')' block; 

expr
    : '(' expr ')'                                                      #Parens
    | '!' expr                                                          #Not
    | '-' expr                                                          #Neg    
    | expr op=('*' | '/' | '%') expr                                    #MulDiv
    | expr op=('+' | '-') expr                                          #AddSub
    | expr op=('<' | '>' | '<=' | '>=') expr                            #Relational
    | expr op=('==' | '!=') expr                                        #Equality
    | expr 'and' expr                                                   #And
    | expr 'or' expr                                                    #Or
    | ID                                                                #Id
    | NUM                                                               #Num
    | ID '[' NUM ']'                                                    #ArrayAccess
    | bool                                                              #Boolean
    | color                                                             #Colour
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

builtInAnim: 'move' | 'moveTo' | 'wait' | 'color';
builtInAnimCall: builtInAnim '(' actualParams ')' ';';

builtInCreateShape: 'createRect' | 'createCircle' | 'createTriangle' | 'createLine' | 'createCanvas';
builtInCreateShapeCall: builtInCreateShape '(' actualParams ')';

builtInUtil: 'add' | 'remove' | 'print' | 'show' | 'hide' | 'resetCanvas';
builtInUtilCall: builtInUtil '(' actualParams ')' ';';

builtInParalAnim: 'move' | 'moveTo' | 'color';
builtInParalAnimCall: builtInParalAnim '(' actualParams ')' ';';

formalParams: (type ID (',' type ID)*)?;                                             
actualParams: (expr (',' expr)*)?;

COMMENT: '#' ~[\r\n]* -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
type: 'int' | 'bool' | 'color' | 'rect' | 'circle' | 'triangle' | 'line' | 'shape' | 'canvas';
bool: 'true' | 'false';
color: 'white' | 'black' | 'red' | 'green' | 'blue';

WS: [ \t\r\n]+ -> skip; 