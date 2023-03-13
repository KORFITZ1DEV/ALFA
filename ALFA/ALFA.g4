grammar ALFA;

start : program EOF;

program: statement+;

statement: createStmt | moveStmt | waitStmt;

createStmt: ID '=' 'createSquare' '(' INT ',' INT ',' INT ',' INT ')' ';';

moveStmt: 'move' '(' ID ',' INT ',' INT ')' ';';

waitStmt: 'wait' '(' INT ')' ';';

ID: [a-zA-Z]+;
INT: '0' |('-'?([1-9][0-9]*));
WS: [ \t\r\n]+ -> skip;