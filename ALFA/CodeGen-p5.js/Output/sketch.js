let startTime = 0

class Rectangle {
    constructor(x, y, width, height) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    render() {
        return rect(this.x, this.y, this.width, this.height);
    }
}

class Animation {
    constructor() {
        this.done = false;
    }
}

class MoveAnimation extends Animation {
    constructor(shape, targetX, duration, type) {
        super();
        this.shape = shape;
        this.targetX = targetX;
        this.type = type;
        this.duration = duration;
        this.progress = 0;
        this.updated = false;
    }

    update() {
        if (this.updated) return;
        this.startX = this.shape.x;
        this.endX = this.targetX + this.startX;
        this.updated = true;
    }

    play() {
        this.update();

        const elapsedTime = (millis() - startTime)
        if (this.progress < 1) {
            this.progress = elapsedTime / this.duration;
            // formula for lerp
            this.shape.x = this.startX + (this.endX - this.startX) * this.progress
        }
        else {
            startTime = millis();
            this.done = true;
        }
    }
}

class WaitAnimation extends Animation {
    constructor(duration) {
        super();
        this.duration = duration;
    }

    play() {
        if (this.done) return;

        if (millis() - startTime >= this.duration) {
            startTime = millis();
            this.done = true;
        }
    }
}

class SeqAnimation extends Animation {
    constructor(animations) {
        super();
        this.animations = animations;
        this.state = 0;
    }

    play() {
        if (this.done) return;

        const elapsedTime = (millis() - startTime)
        const currAnimation = this.animations[this.state];

        if (currAnimation.done == false) {
            currAnimation.play();
        }
        else {
            startTime = millis();
            this.state++;

            if (this.animations.length == this.state) {this.done = true}
        }
    }
}

class ParalAnimation extends Animation {
    constructor(animations) {
        super();
        this.animations = animations;
    }

    play() {
        if (this.done) return;
        if (this.animations.every(a => a.done)) {this.done = true;}

        for (const animation of this.animations) {
            animation.play();
        }
    }
}

const x1 = 0
const y1 = 0
const x2 = 100
const y2 = 140
const length = 100
const width = 200
const animDuration = 1000
const delay = 300
const myrect1 = new Rectangle(x1,y1,width,length);

const myrect2 = new Rectangle(x2,y2,length,length);

const anim_0 = new MoveAnimation(myrect1,200,animDuration);
const anim_1 = new WaitAnimation(delay);
const anim_2 = new MoveAnimation(myrect2,300,animDuration);
const anim_3 = new MoveAnimation(myrect1,-200,animDuration);
const anim_4 = new WaitAnimation(delay);
const anim_5 = new MoveAnimation(myrect2,-200,animDuration);
const myrect3 = new Rectangle(200,150,20,20);

const anim_6 = new MoveAnimation(myrect3,200,animDuration);
const seqAnim = new SeqAnimation([anim_0,anim_1,anim_2,anim_3,anim_4,anim_5,anim_6]);

function setup() {
	createCanvas(600, 600)
	startTime = millis()
}

function draw() {
	background(255)
	myrect1.render();
	myrect2.render();
	myrect3.render();
	seqAnim.play();
}