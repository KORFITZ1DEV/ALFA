grammar ALFA3;

prog: stmt+ EOF;

stmt
    : varDcl
    | assignStmt
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    | animDcl
    | animCall
    ;
    
varDcl: type assignStmt;

assignStmt: <assoc=right> ID '=' (builtInCreateShapeCall | expr) ';';

ifStmt: 'if' '(' expr ')' block ('else if' '(' expr ')' block)* ('else' block)?;

loopStmt: 'loop' '(' 'int' ID 'from' (NUM | ID) '..' (NUM | ID) ')' block;

paralStmt: 'paral' paralBlock;

animDcl: 'animation' ID '(' formalParams ')' block; 

expr
    : '(' expr ')'                                  #Parens
    | '!' expr                                      #Not
    | '-' expr                                      #Neg    
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

block: '{' blockStmt* '}';
blockStmt
    : varDcl
    | assignStmt
    | builtInAnimCall
    | ifStmt
    | loopStmt
    | paralStmt
    | animCall
    ; //Declaration of custom animations are NOT allowed in blocks.

paralBlock: '{' paralBlockStmt* '}';
paralBlockStmt
    : builtInParalAnimCall
    | animCall
    ;

builtInAnim: 'move' | 'wait';
builtInAnimCall: builtInAnim '(' actualParams ')' ';';

builtInCreateShape: 'createRect';
builtInCreateShapeCall: builtInCreateShape '(' actualParams ')';

builtInParalAnim: 'move';
builtInParalAnimCall: builtInParalAnim '(' actualParams ')' ';';

animCall: ID '(' actualParams ')' ';';

formalParams: (type ID (',' type ID)*)?;                                             
actualParams: (expr (',' expr)*)?;

COMMENT: '#' ~[\r\n]* -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
type: 'int' | 'bool' | 'rect';
bool: 'true' | 'false';

WS: [ \t\r\n]+ -> skip;