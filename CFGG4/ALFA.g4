grammar ALFA;

start : program EOF;

program: stmt+;

stmt: TYPE varDcl ';' | funcCall ';' | animDcl | loopStmt | playStmt;
funcCall: (ID '(' (arg (',' arg)*)? ')') 
        | builtInFuncCall;
builtInFuncCall: BUILTINFUNC '(' (arg (',' arg)*)? ')';
animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' '{' blockStmt* '}';
param: TYPE ID;
arg: COLOR | expr;
playStmt: 'play' '{' blockStmt* '}';
varDcl: '[]' ID '=' '{' arrayElem (',' arrayElem)* '}'
        | ID '=' (builtInFuncCall | expr) ;
arrayElem: ID; //in the future maybe also NUM
expr: term (op expr)?;
term: NUM | ID ('['POSNUM']')?;
blockStmt: ifStmt | ifElseStmt | paralStmt | loopStmt | playStmt;
ifStmt: 'if' '(' condition ')' '{' blockStmt* '}' 'else' '{' blockStmt* '}';
ifElseStmt: 'if' '(' condition ')' '{' blockStmt* '}' ('else if' '{' blockStmt* '}')* 'else' '{' blockStmt* '}';
condition: arg ( boolOp arg )*;
paralStmt: 'paral' '{' funcCall* '}';
loopStmt: 'loop' '(' 'int' ID 'from' NUM '..' NUM ')' '{' blockStmt* '}';


boolOp: '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';
op: '+' | '-' | '*' | '/' | '%' | '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';
TYPE: 'int' | 'canvas' | 'square' | 'circle' | 'shape';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'|'-'?[1-9][0-9]*;
POSNUM: '0'|[1-9][0-9]*;
BUILTINFUNC: 'add' | 'color' | 'print' | 'createSquare' | 'createCircle' | 'createCanvas' | 'move' | 'moveTo' | 'wait' | 'resetCanvas'; 
COLOR: 'white' | 'black' | 'red' | 'green' | 'blue';
WS: [ \t\r\n]+ -> skip;