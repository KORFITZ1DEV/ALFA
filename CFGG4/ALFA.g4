grammar ALFA;

prog: stmt* playStmt EOF;

stmt: (type varDcl | funcCall) ';' | animDcl | loopStmt;

varDcl: <assoc=right> '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')?
        | <assoc=right> ID '=' (createFuncCall | expr) ;

createFuncCall: createFunc '(' args ')'; 

funcCall: ID '(' args ')' 
        | builtInFuncCall;
        
args: (arg (',' arg)*)?;

builtInFuncCall:  (seqFunc | builtInFunc) '(' args ')';

animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' block;

param: type ID;

arg: color | expr;

arrayElem: ID | '-'?NUM; 

expr: '(' expr ')'
    | unaryOp expr                          //Unary negative or negation maybe tvinge parentes
    | ID ('[' NUM ']')? rhsExpr             //Id or array expression
    | NUM rhsExpr
    | bool rhsExpr;

rhsExpr: (( multiOp | op | boolOp) expr)*;
unaryOp: '!' | '-';
multiOp: '*' | '/' | '%'; 
op: '+' | '-';
boolOp: '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';

blockStmt: (varDcl | builtInFuncCall | funcCall) ';' | ifStmt | paralStmt | loopStmt ;

ifStmt: 'if' '(' condition ')' block ('else if' block)* ('else' block)?;

condition: ('!')? arg ( boolOp ('!')? arg )*;

block: '{' blockStmt* '}';

paralStmt: 'paral' '{' (paralBlockStmt)* '}';

paralBlockStmt: (builtInFuncCall | funcCall) ';';

loopStmt: 'loop' '(' 'int' ID 'from' '-'?NUM '..' '-'?NUM ')' '{' loopBlockStmt* '}';

loopBlockStmt:  varDcl ';' | loopIfStmt | paralStmt | loopStmt | builtInFuncCall ';' | funcCall ';';

loopIfStmt: 'if' '(' condition ')' loopBlock ('else if' '(' condition ')' loopBlock)* ('else' loopBlock)?;

loopBlock: '{' (blockStmt | 'break;' | 'continue;')* '}';

playStmt: 'play' '{' playBlockStmt* '}';

playBlockStmt: paralStmt | loopStmt | funcCall ';' ;


type: 'int' | 'bool' | 'canvas' | 'square' | 'circle' | 'shape';
bool: 'true' | 'false';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| [1-9][0-9]* ;
builtInFunc: 'add' | 'color' | 'print' | 'moveTo' | 'move';
seqFunc: 'resetCanvas' | 'wait' ;
createFunc: 'createSquare' | 'createCircle' | 'createCanvas';
color: 'white' | 'black' | 'red' | 'green' | 'blue';
WS: [ \t\r\n]+ -> skip;