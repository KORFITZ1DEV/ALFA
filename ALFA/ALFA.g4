grammar ALFA;

prog: stmt* playStmt EOF;

stmt: TYPE varDcl ';' | funcCall ';' | animDcl | loopStmt;

varDcl: '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')?
        | ID '=' (createFuncCall | expr) ;

createFuncCall: CREATEFUNC '(' args ')'; 

funcCall: ID '(' args ')' 
        | builtInFuncCall;
        
args: (arg (',' arg)*)?;

builtInFuncCall: BUILTINFUNC '(' args ')' 
        | SEQFUNC '(' args ')';

animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' block;

param: TYPE ID;

arg: COLOR | expr;

arrayElem: ID | NUM; 

expr: term (OP expr)*;

term: NUM | ID ('['POSNUM']')?;

blockStmt: varDcl ';' | ifStmt | paralStmt | loopStmt;

ifStmt: 'if' '(' condition ')' block ('else if' block)* ('else' block)?;

condition: ('!')? arg ( BOOLOP ('!')? arg )*;

block: '{' blockStmt* '}';

paralStmt: 'paral' '{' BUILTINFUNC* '}';

loopStmt: 'loop' '(' 'int' ID 'from' NUM '..' NUM ')' block;

playStmt: 'play' '{' playBlockStmt* '}';

playBlockStmt: paralStmt | loopStmt | funcCall ';' ;

BOOLOP: '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';
OP: '+' | '-' | '*' | '/' | '%' | BOOLOP;
TYPE: 'int' | 'bool' | 'canvas' | 'square' | 'circle' | 'shape';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '-'[1-9][0-9]* | POSNUM;
POSNUM: '0'|[1-9][0-9]*;
BUILTINFUNC: 'add' | 'color' | 'print' | 'moveTo' | 'move';
SEQFUNC: 'resetCanvas' | 'wait' ;
CREATEFUNC: 'createSquare' | 'createCircle' | 'createCanvas';
COLOR: 'white' | 'black' | 'red' | 'green' | 'blue';
WS: [ \t\r\n]+ -> skip;