let startTime = 0
let state = 0
let progress = 0
let x0 = 0

function setup() {
	createCanvas(600, 600)
	startTime = millis()
}

function draw() {
	background(255)
	rect(x0,0,20,20)

	if (state == 0) {
		if (x0 < 200) {progress = (millis() - startTime) / 4000;x0 = 0 + (200 - 0) * progress }
		else { startTime = millis(); state = 1;}
	}

	if (state == 1) {
		if (millis() - startTime >= 2000) { startTime = millis(); state = 2 }
	}

	if (state == 2) {
		if (x0 > 1) {progress = (millis() - startTime) / 4000;x0 = 200 + (-200 + 1) * progress }
		else { startTime = millis(); state = 3;}
	}
}