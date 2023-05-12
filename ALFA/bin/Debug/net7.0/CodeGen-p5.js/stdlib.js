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