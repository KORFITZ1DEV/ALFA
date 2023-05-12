grammar ALFA;

program : statement+ EOF;

statement: varDcl | builtInAnimCall;

varDcl: type ID '=' (builtInCreateShapeCall | NUM) ';';

builtInAnim: 'move' | 'wait';
builtInAnimCall: builtInAnim '(' args ')' ';';                 

builtInCreateShape: 'createRect';
builtInCreateShapeCall: builtInCreateShape '(' args ')';

args: arg (',' arg)*;

arg: NUM | ID;

type: 'int' | 'rect';

ID: [a-zA-Z_][a-zA-Z0-9_]*;
NUM: '0'| '-'?[1-9][0-9]*;
WS: [ \t\r\n]+ -> skip;