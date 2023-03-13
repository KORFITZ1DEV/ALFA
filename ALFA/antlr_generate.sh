ANTPATH="src/antlr"
DESTPATH="src"

cd ALFA
antlr4 -o $ANTPATH -Dlanguage=CSharp ALFA.g4

case "$OSTYPE" in
  msys*) move $ANTPATH/*.cs $DESTPATH ;;
  darwin*)  mv $ANTPATH/*.cs $DESTPATH ;;
  *)        echo "Unknown: $OSTYPE" ;;
esac



