const shapesToDraw = [];

class Shape {
    constructor(x, y) {
        this.x = x;
        this.y = y;
        shapesToDraw.push(this);
    }

    move(offsetX, offsetY, duration) {
        let start = Date.now();

        let originalX = this.x;
        let originalY = this.y;

        let animate = (resolve) => {
            let now = Date.now();
            let progress = (now - start) / duration;

            if (progress >= 1) {
                this.x = originalX + offsetX;
                this.y = originalY + offsetY;
                resolve(); // Resolving the Promise here
                return;
            }

            let dx = progress * offsetX;
            let dy = progress * offsetY;

            this.x = originalX + dx;
            this.y = originalY + dy;

            requestAnimationFrame(() => animate(resolve)); // Passing `resolve` to the next frame
        }

        return new Promise((resolve, reject) => {
            animate(resolve); // Passing `resolve` to the `animate` function
        });
    }
}

class Rect extends Shape {
    constructor(x, y, w, h) {
        super(x, y);
        this.w = w;
        this.h = h;
    }

    draw() {
        rect(this.x, this.y, this.w, this.h);
    }
}

function wait(duration) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, duration)
    });
}

async function moveParal(animations) {
    const promises = animations.map(animation => animation());
    return Promise.all(promises);
}


async function main() {
	let var_len=40
	let var_xPos=1000 / 2 - var_len / 2
	let var_myRect1=new Rect(var_xPos,0,var_len,var_len)
	let var_myRect2=new Rect(var_xPos,0,var_len,var_len)
	let var_myRect3=new Rect(var_xPos,0,var_len,var_len)
	let var_myRect4=new Rect(var_xPos,0,var_len,var_len)
	let var_myRect5=new Rect(var_xPos,0,var_len,var_len)
	let var_myRect6=new Rect(var_xPos,0,var_len,var_len)
	let var_offset1=300
	let var_offset2=400
	let var_offset3=500
	let var_offset4=600
	let var_offset5=700
	let var_offset6=800 + 300 - 200
var_offset6=var_offset5 - var_offset3
	let var_duration=300

	for (let var_i=0
; var_i < 4; var_i++){
		
		await moveParal([			
			() => var_myRect1.move(0,var_offset1 + 200,var_duration),
			() => var_myRect2.move(0,var_offset2 + var_offset1,var_duration),
			() => var_myRect3.move(0,var_offset3 - var_offset2 + var_offset1,var_duration),
			() => var_myRect4.move(0,var_offset4,var_duration),
			() => var_myRect5.move(0,var_offset5,var_duration),
			() => var_myRect6.move(0,var_offset6,var_duration),		
		]);


		await wait(1000);
var_offset1= -var_offset1
var_offset2= -var_offset2
var_offset3= -var_offset3
var_offset4= -var_offset4
var_offset5= -var_offset5
var_offset6= -var_offset6
	}
}

function setup() {
	createCanvas(1000, 1000)
	main();
}

function draw() {
	background(255)

	for (const shape of shapesToDraw) {
		shape.draw()
	}
}