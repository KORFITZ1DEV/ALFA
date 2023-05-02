grammar OldVersionWorks;
//https://stackoverflow.com/questions/26471876/how-to-tell-the-precedence-of-operators-in-a-context-free-grammar
//page 144 sebesta

prog: stmt* playStmt EOF;

stmt: (type varDcl | funcCall) ';' | animDcl | loopStmt;    //statement can either be vardcl, funcCall, animDcl, loopStmt

varDcl:   '[]' ID ('=' '{' arrayElem (',' arrayElem)* '}')? //array declaration 
        | <assoc=right> ID '=' (createFuncCall | expr) ;    //Right associative variable assignment

createFuncCall: createFunc '(' args ')';                    //built-in function call that returns rect/canvas/circle

funcCall: ID '(' args ')'                                   //user defined animation call 
        | builtInFuncCall;                                  

args: (arg (',' arg)*)?;                                    //arguments passed to a function

builtInFuncCall:  (seqFunc | builtInFunc) '(' args ')';    

animDcl: 'animation' ID '(' (param (' ,' param)*)? ')' block; 

param: type ID;                                             

arg: color | expr;                                          

arrayElem: ID | '-'?NUM;                                    //Id or (-)num should probably be type instead.

expr: orExpr;
orExpr: orExpr 'or' andExpr | andExpr;                              //priority (7)
andExpr: andExpr 'and' equalityExpr | equalityExpr;                 //priority (6)
equalityExpr: equalityExpr equalityOp boolExpr | boolExpr;          //priority (5)
boolExpr: boolExpr boolOp addExpr | addExpr ;                       //priority (4)
addExpr: addExpr addSubOp multExpr | multExpr;                      //priority (3)
multExpr: multExpr multiOp terminalExpr | unaryOp? terminalExpr;    //priority (2)
terminalExpr: NUM                                                   //priority (1)
            | ID ('[' NUM ']')?                                     //priority (1)
            | '(' expr ')'                                          //priority (1)
            | bool ;                                                //priority (1)

blockStmt: (varDcl | builtInFuncCall | funcCall) ';' | ifStmt | paralStmt | loopStmt ;

ifStmt: 'if' '(' condition ')' block ('else if' '(' condition ')' block)* ('else' block)?;

condition: ('not')? arg ( boolOp ('not')? arg )*;

block: '{' blockStmt* '}';

paralStmt: 'paral' '{' (paralBlockStmt)* '}';

paralBlockStmt: (builtInFuncCall | funcCall) ';';

loopStmt: 'loop' '(' 'int' ID 'from' '-'?NUM '..' '-'?NUM ')' '{' blockStmt* '}';

playStmt: 'play' '{' playBlockStmt* '}';

playBlockStmt: paralStmt | loopStmt | funcCall ';' ;

type: 'int' | 'bool' | 'canvas' | 'rect' | 'circle' | 'shape';
bool: 'true' | 'false';
unaryOp: '-'| 'not';
multiOp: '*' | '/' | '%'; 
addSubOp: '+' | '-';
boolOp: '<' | '>' | '<=' | '>=';
equalityOp: '==' | '!=';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| [1-9][0-9]* ;
builtInFunc: 'add' | 'color' | 'print' | 'moveTo' | 'move';
seqFunc: 'resetCanvas' | 'wait' ;
createFunc: 'createRect' | 'createCircle' | 'createCanvas';
color: 'white' | 'black' | 'red' | 'green' | 'blue';
WS: [ \t\r\n]+ -> skip;