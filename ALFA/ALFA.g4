grammar ALFA;

program : statement+ EOF;

statement: type varDcl ';' | funcCall ';';

varDcl: ID '=' (funcCall | NUM);

funcCall: builtIns '(' args ')';

builtIns: 'createRect' | 'move' | 'wait';

args: arg (',' arg)*;

arg: NUM | ID;

type: 'int' | 'rect';

ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
WS: [ \t\r\n]+ -> skip;