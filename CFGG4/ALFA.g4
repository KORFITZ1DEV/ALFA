grammar ALFA;

prog: stmt* playStmt EOF;

stmt: (type varDcl | funcCall) ';' | animDcl | loopStmt;    //statement can either be vardcl, funcCall, animDcl, loopStmt

varDcl:   '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')? //array declaration 
        | <assoc=right> ID '=' (createFuncCall | expr) ;    //Right associative varaible declaration

createFuncCall: createFunc '(' args ')';                    //builtin function call that returns square/canvas/circle

funcCall: ID '(' args ')'                                   //user defined animation call 
        | builtInFuncCall;                                  
        
args: (arg (',' arg)*)?;                                    //arguments passed to a function

builtInFuncCall:  (seqFunc | builtInFunc) '(' args ')';     

animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' block; //'add' | 'color' | 'print' | 'moveTo' | 'move'; 'resetCanvas' | 'wait' ;

param: type ID;                                             

arg: color | expr;                                          

arrayElem: ID | '-'?NUM;                    //Id or (-)num should probably be type instead.

expr: '(' expr ')'                          //parenthesis
    | unaryOp expr                          //Unary negative or negation maybe tvinge parentes
    | ID ('[' NUM ']')? rhsExpr             //Id or array expression
    | NUM rhsExpr                           //pos or negative integer via unaryOp expr
    | bool rhsExpr;                         //boolean

rhsExpr: (( multiOp | op | boolOp) expr)*;  // *,/,%, +,- or bool operations. unaryOp can only be placed at lhs
unaryOp: '!' | '-';
multiOp: '*' | '/' | '%'; 
op: '+' | '-';
boolOp: '==' | '!=' | '<' | '>' | '<=' | '>=' | '&&' | '||';

blockStmt: (varDcl | builtInFuncCall | funcCall) ';' | ifStmt | paralStmt | loopStmt ;

ifStmt: 'if' '(' condition ')' block ('else if' block)* ('else' block)?;

condition: ('!')? arg ( boolOp ('!')? arg )*;

block: '{' blockStmt* '}';                     //(varDcl | builtInFuncCall | funcCall) ';' | ifStmt | paralStmt | loopStmt

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