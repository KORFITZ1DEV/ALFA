grammar ALFA;

program : statement+ EOF;

statement: type varDcl ';' | funcCall ';';

varDcl: ID '=' funcCall
| ID '=' NUM;

funcCall: builtIns '(' args ')';

builtIns: 'createSquare' | 'move' | 'wait';

args: arg (',' arg)*;

arg: NUM | ID;

type: 'int' | 'square' ;

ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]* ;
WS: [ \t\r\n]+ -> skip;