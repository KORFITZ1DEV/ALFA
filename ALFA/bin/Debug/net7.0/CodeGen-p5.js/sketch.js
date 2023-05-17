class Shape {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    move(offsetX, offsetY, duration) {
        let start = Date.now();

        let originalX = this.x;
        let originalY = this.y;

        let animate = (resolve) => { // Added `resolve` parameter
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

const x = 0
const y = 0
const length = 20
const animDuration = 4000
const delay = 2000
const myRect1 = new Rect(0,0,length,length);

async function main() {
	await myRect1.move(200, 0, animDuration);
	await wait(delay);
	await myRect1.move(-200, 0, animDuration);
	}

function setup() {
	createCanvas(1000, 1000)
	main();
}

function draw() {
	background(255)
	myRect1.draw();
}