grammar ALFA;

program : statement+ EOF;

statement: varDcl ';' | funcCall';';

varDcl: ID '=' funcCall;

funcCall: builtIns '(' args ')';

builtIns: 'createSquare' | 'move' | 'wait';

args: arg (',' arg)*;

arg: INT | ID;

ID: [a-zA-Z]+;
INT: '0' |('-'?([1-9][0-9]*));
WS: [ \t\r\n]+ -> skip;