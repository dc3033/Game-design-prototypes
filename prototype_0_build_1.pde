
// game design prototype 0
// 2d pickup game with gravity control mechanics
// daniel chan (some code borrowed from demo by andy nealen)
// 1/28/18

int NP = 100;
float dt = 0.4;
float damp = 0.96;
float timealter = 1.0;

//gravity vectors
PVector g = new PVector(0, 9.81);  
PVector ug = new PVector(0, -9.81);
PVector lg = new PVector(-9.81, 0);
PVector rg = new PVector(9.81, 0);

//pickup vector arrays
PVector[] p = new PVector[NP];
PVector[] v = new PVector[NP];

//pickup boolean array
boolean[] dead = new boolean[NP];

//player vectors
PVector pp = new PVector(300, 200);
PVector pv = new PVector(0, 0);
PVector pa = new PVector(0, 0);

//gravity booleans
boolean gravity = false;
boolean upgravity = false;
boolean leftgravity = false;
boolean rightgravity = false;

//time modifier boolean
boolean halftime = false;

void setup() {
  size(600, 400, P2D);
  smooth();
  fill(0);
  stroke(255, 255, 255, 255);
  background(192, 64, 0);
  strokeWeight(1);
  reroll();
} 

void draw() {

  // clear framebuffer
  background(192, 64, 0);

  // draw points
  fill(0);
  stroke(255, 255, 255, 255);

  for (int i = 0; i < NP; i++) {
    if (!dead[i]){
    ellipse(p[i].x, p[i].y, 10, 10);
    }
  }    

  // draw player
  fill(255);
  stroke(0, 0, 0, 0);  
  ellipse(pp.x, pp.y, 30, 30);

  // advance stuff  
  move();
}

void move() {
  // check if time is being slowed
  if (halftime) {timealter = 0.5;}
  else {timealter = 1;}
  
  // accumulate accelerations from gravity
  // gravity only applies to player, not pickups
  pa.set(0,0,0);
  
  if (gravity) {
    PVector accel = g.get();
    pa.add(accel);
  }
  
  if (upgravity) {
    PVector accel = ug.get();
    pa.add(accel);
  }
  
  if (leftgravity) {
    PVector accel = lg.get();
    pa.add(accel);
  }
    
  if (rightgravity) {
    PVector accel = rg.get();
    pa.add(accel);
  }

  // euler integration/timestepping
  
  // pickups
  for (int i = 0; i < NP; i++) {

    // p_i+1 = p_i + v_i dt
    PVector vdt = v[i].get();
    vdt.mult(timealter);
    vdt.mult(dt);
    p[i].add(vdt);
  }

  // player
  // pv+1 = (pv + pa dt) * damp
  PVector adt = pa.get();
  adt.mult(dt);
  pv.add(adt);
  pv.mult(damp);

  // pp+1 = pp + pv dt
  PVector vdt = pv.get();
  vdt.mult(timealter);
  vdt.mult(dt);
  pp.add(vdt);

  // wall collisions
  wall();
  
  // player & pickup collisions
  trigger();
}
 
void wall() { 
  // wall collisions
  
  //pickup collision
  for (int i = 0; i < NP; i++) {
    if (p[i].x > width) {
      v[i].x = -v[i].x;
      p[i].x = width;
    }
    if (p[i].x < 0) {
      v[i].x = -v[i].x;
      p[i].x = 0;
    }
    // bottom/floor
    if (p[i].y > height) {
      v[i].y = -v[i].y;
      p[i].y = height;
    }
    //top/ceiling
    if (p[i].y > height || p[i].y < 0) {
      v[i].y = -v[i].y;
      p[i].y = 0;
    }
  }    
  
  //player collision
  if (pp.x > width) {
    pv.x = -pv.x;
    pp.x = width;
  }
  if (pp.x < 0) {
    pv.x = -pv.x;
    pp.x = 0;
  }
  // bottom/floor
  if (pp.y > height) {
    pv.y = -pv.y;
    pp.y = height;
  }
  //top/ceiling
  if (pp.y > height || pp.y < 0) {
    pv.y = -pv.y;
    pp.y = 0;
  }
}

void trigger() {
  // player collisions with pickups
  
  for (int i = 0; i < NP; i++){
    if (dist(p[i].x, p[i].y, pp.x, pp.y) <= 20 && !dead[i]){
      dead[i] = true;
    }
  }
}

void mousePressed() {
  background(192, 64, 0);
  reroll();
}

void keyPressed() {
  if (keyCode == DOWN) {
    gravity = !gravity;
  } 
  if (keyCode == UP) {
    upgravity = !upgravity;
  } 
  if (keyCode == LEFT) {
    leftgravity = !leftgravity;
  } 
  if (keyCode == RIGHT) {
    rightgravity = !rightgravity;
  } 
  if (keyCode == SHIFT) {
    halftime = true;  
  }
}

void keyReleased() {
  if (keyCode == SHIFT) {
    halftime = false;
  }
}

void reroll() {
  for (int i = 0; i < NP; i++) {
    p[i] = new PVector(random(20, width -20), random(20, height-20));
    v[i] = new PVector(random(-8, 8),random(-8, 8));
    dead[i] = false;
  }  
  pp = new PVector(300, 200);
  pv = new PVector(0, 0);
  pa = new PVector(0, 0);
}