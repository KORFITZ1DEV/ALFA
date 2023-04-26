grammar ALFAnew;
// NOT DONE YET - look at ALFA2.g4 instead

prog: stmt* playStmt EOF;

stmt
    : varDcl ';'
    | assignStmt ';'
    | funcCall ';' 
    | animDcl
//    | ifStmt
//    | loopStmt
    ;

varDcl
    : <assoc=right> type ID '=' (createFuncCall | expr)
    | <assoc=right> type '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')?
    ;
    
assignStmt
    : <assoc=right> ID '=' (createFuncCall | expr)
    | <assoc=right>  ID '[' NUM ']' '=' arrayElem
    ;

funcCall
    : ID '(' actualParams ')'   // call of user-defined animation
    | builtInFuncCall;                                  

animDcl: 'animation' ID '(' formalParams ')' animBlock; 

expr
    : 'not' expr
    | '(' expr ')'
    | '-' expr
    | expr op=('*' | '/' | '%') expr            
    | expr op=('+' | '-') expr
    | expr op=('<' | '>' | '<=' | '>=') expr
    | expr op=('==' | '!=') expr
    | expr 'and' expr
    | expr 'or' expr
    | ID ('[' NUM ']')?
    | ID
    | NUM
    | bool
    | color
    ;

//blockStmt: (varDcl | builtInFuncCall | funcCall) ';' | ifStmt | paralStmt | loopStmt ;

ifStmt: 'if' '(' expr ')' block ('else if' '(' expr ')' block)* ('else' block)?;

// Blocks
block: '{' blockStmt* '}';
animBlock: '{' animBlockStmt* '}';
paralBlock: '{' paralBlockStmt* '}';

//Special statements
blockStmt
    : varDcl ';'
    | assignStmt ';'
    | funcCall ';' 
    | ifStmt
    | loopStmt
    ;

animBlockStmt
    : varDcl ';'
    | assignStmt ';'
    | funcCall ';' 
    | ifStmt
    | loopStmt
    | paralStmt // In animation block, paralStmt is allowed
    ;

paralStmt: 'paral' '{' (paralBlockStmt)* '}';

paralBlockStmt: (builtInFuncCall | funcCall) ';';

loopStmt: 'loop' '(' 'int' ID 'from' '-'?NUM '..' '-'?NUM ')' '{' blockStmt* '}';

playStmt: 'play' '{' playBlockStmt* '}';

playBlockStmt: paralStmt | loopStmt | funcCall ';' ;


ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| [1-9][0-9]*;
type: 'int' | 'bool' | 'canvas' | 'rect' | 'circle' | 'shape';
bool: 'true' | 'false';

builtInFunc: 'add' | 'color' | 'print' | 'moveTo' | 'move' ;
seqFunc: 'resetCanvas' | 'wait' ;
createFunc: 'createRect' | 'createCircle' | 'createCanvas';
color: 'white' | 'black' | 'red' | 'green' | 'blue';

arrayElem: ID | '-'?NUM | color | bool;
createFuncCall: createFunc '(' actualParams ')';
builtInFuncCall:  (seqFunc | builtInFunc) '(' actualParams ')';  
formalParams: (type ID (' ,' type ID)*)?;                                             
actualParams: (expr (',' expr)*)?;

WS: [ \t\r\n]+ -> skip;