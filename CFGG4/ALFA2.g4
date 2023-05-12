//Additions: comments, boolean data type, expressions, if-statement, loop, parallel animations. 
grammar ALFA2;

prog: stmt+ EOF;

stmt
    : varDcl ';'
    | assignStmt ';'
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    ;

varDcl: type assignStmt;

assignStmt: <assoc=right> ID '=' (builtInCreateShapeCall | expr);

ifStmt: 'if' '(' expr ')' block ('else if' '(' expr ')' block)* ('else' block)?;

loopStmt: 'loop' '(' 'int' ID 'from' expr '..' expr ')' block;

paralStmt: 'paral' paralBlock;

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
    | bool                                          #Boolean
    ;

block: '{' stmt* '}';
paralBlock: '{' (builtInParalAnimCall)* '}';

builtInAnim: 'move' | 'wait';
builtInAnimCall: builtInAnim '(' actualParams ')' ';';                                  

builtInCreateShape: 'createRect';
builtInCreateShapeCall: builtInCreateShape '(' actualParams ')';

builtInParalAnim: 'move';
builtInParalAnimCall: builtInParalAnim '(' actualParams ')' ';';

actualParams: (expr (',' expr)*)?;

COMMENT: '#' ~[\r\n]* -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
type: 'int' | 'bool' | 'rect';
bool: 'true' | 'false';
WS: [ \t\r\n]+ -> skip;