grammar ALFA;

prog: stmt* playStmt EOF;

stmt: type varDcl ';' | funcCall ';' | animDcl | loopStmt;

varDcl: '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')?
        | ID '=' (createFuncCall | expr) ;

createFuncCall: createFunc '(' args ')'; 

funcCall: ID '(' args ')' 
        | builtInFuncCall;
        
args: (arg (',' arg)*)?;

builtInFuncCall: builtInFunc '(' args ')' 
        | SEQFUNC '(' args ')';

animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' block;

param: type ID;

arg: COLOR | expr;

arrayElem: ID | '-'?NUM; 

expr: term (op expr)*;

term: '-'?NUM | ID ('[' NUM ']')?;

blockStmt: varDcl ';' | ifStmt | paralStmt | loopStmt | builtInFuncCall ';' | funcCall ';';

ifStmt: 'if' '(' condition ')' block ('else if' block)* ('else' block)?;

condition: ('!')? arg ( boolOp ('!')? arg )*;

block: '{' blockStmt* '}';

paralStmt: 'paral' '{' (paralBlockStmt)* '}';

paralBlockStmt: builtInFuncCall ';' | funcCall ';';

loopStmt: 'loop' '(' 'int' ID 'from' '-'?NUM '..' '-'?NUM ')' '{' loopBlockStmt* '}';

loopBlockStmt:  varDcl ';' | loopIfStmt | paralStmt | loopStmt | builtInFuncCall ';' | funcCall ';';

loopIfStmt: 'if' '(' condition ')' loopBlock ('else if' '(' condition ')' loopBlock)* ('else' loopBlock)?;

loopBlock: '{' (blockStmt | 'break;' | 'continue;')* '}';

playStmt: 'play' '{' playBlockStmt* '}';

playBlockStmt: paralStmt | loopStmt | funcCall ';' ;

boolOp: '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';
op: '+' | '-' | '*' | '/' | '%' | boolOp;
type: 'int' | 'bool' | 'canvas' | 'square' | 'circle' | 'shape';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| [1-9][0-9]* ;
builtInFunc: 'add' | 'color' | 'print' | 'moveTo' | 'move';
SEQFUNC: 'resetCanvas' | 'wait' ;
createFunc: 'createSquare' | 'createCircle' | 'createCanvas';
COLOR: 'white' | 'black' | 'red' | 'green' | 'blue';
WS: [ \t\r\n]+ -> skip;