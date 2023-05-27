# ALFA
A domain specific language for animations

# Types
## int
Represents an integer accordingly to the JavaScript definition.

## rect
Represents a rectangle.

## bool
Represents truthy or falsy values.

# Built-in functions
## move(rect x, int offsetX, int offsetY, int duration)
Moves a rectangle from its current position to a new position based on offsetX, and offsetY over a duration in miliseconds.

| Parameters | Description |
| ----------- | ----------- |
| rect x | A declared variable of type rect |
| int offsetX | A declared variable of type int |
| int offsetY | A declared variable of type int |
| int duration | A declared variable of type int |

## wait(int duration)
Repeats the last played frame over a duration in miliseconds.

| Parameters | Description |
| ----------- | ----------- |
| int duration | A declared variable of type int |


## createRect(int xPosition, int yPosition, int width, int length)
Creates a rectangle at (x, y) on the canvas with a given width and length.

| Parameters | Description |
| ----------- | ----------- |
| int xPosition | A declared variable of type int |
| int yPosition | A declared variable of type int |
| int width | A declared variable of type int |
| int length | A declared variable of type int |

# Keywords
## paral 

## loop

## if-elseif-else
