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
	let var_pos=200
	let var_myRect1=new Rect(var_pos,var_pos,10,10)
	let var_myRect2=new Rect(var_pos + 100,var_pos,10,10)
	let var_myRect3=new Rect(var_pos + 300,var_pos,10,10)
	let var_myRect4=new Rect(var_pos + 400,var_pos,10,10)
	let var_duration1=500
	let var_pushLength=10
	let var_offSet=50
	let var_fDuration=100
	let var_loopduration1=500

	for (let var_i=1
; var_i <= 10; var_i++){
		
		await moveParal([			
			() => var_myRect1.move(20,20,var_duration1),
			() => var_myRect2.move(20,20,var_duration1),
			() => var_myRect3.move(20,20,var_duration1),
			() => var_myRect4.move(20,20,var_duration1),		
		]);


		for (let var_j=1
; var_j <= 2; var_j++){
			
			await moveParal([				
				() => var_myRect1.move(var_offSet,0,var_loopduration1),
				() => var_myRect2.move(var_offSet,0,var_loopduration1),
				() => var_myRect3.move(var_offSet,0,var_loopduration1),
				() => var_myRect4.move(var_offSet,0,var_loopduration1),			
			]);


			await moveParal([				
				() => var_myRect1.move(0,var_offSet,var_loopduration1),
				() => var_myRect2.move(0,var_offSet,var_loopduration1),
				() => var_myRect3.move(0,var_offSet,var_loopduration1),
				() => var_myRect4.move(0,var_offSet,var_loopduration1),			
			]);


			await moveParal([				
				() => var_myRect1.move( -var_offSet,0,var_loopduration1),
				() => var_myRect2.move( -var_offSet,0,var_loopduration1),
				() => var_myRect3.move( -var_offSet,0,var_loopduration1),
				() => var_myRect4.move( -var_offSet,0,var_loopduration1),			
			]);


			await moveParal([				
				() => var_myRect1.move(0, -var_offSet,var_loopduration1),
				() => var_myRect2.move(0, -var_offSet,var_loopduration1),
				() => var_myRect3.move(0, -var_offSet,var_loopduration1),
				() => var_myRect4.move(0, -var_offSet,var_loopduration1),			
			]);

		}
	}

	await var_myRect1.move(10 *  (-20),10 *  (-20),var_fDuration);

	await var_myRect2.move(10 *  (-20),10 *  (-20),var_fDuration);

	await var_myRect3.move(10 *  (-20),10 *  (-20),var_fDuration);

	await var_myRect4.move(10 *  (-20),10 *  (-20),var_fDuration);
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