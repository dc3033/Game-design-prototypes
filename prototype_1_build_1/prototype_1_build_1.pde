
// game design prototype 2
// 2d "platformer" with gravity control mechanics
// features variable difficulty, a time limit, and randomized objectives
// only uses circles!
// daniel chan
// 2/4/18

import java.util.Random;

float timer = 0;
int score = 0;
int difficulty = 1;
int endstate = 0;
int TC = 30;
int NT = 12;
float dt = 0.4;
float damp = 0.96;

//gravity vectors
PVector g = new PVector(0, 3);  
PVector ug = new PVector(0, -3);
PVector lg = new PVector(-3, 0);
PVector rg = new PVector(3, 0);

//randomness array
int rand[] = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};

//timer vectors
PVector t[] = new PVector[TC];

//difficulty vectors
PVector d[] = new PVector[10];

//trigger vectors
PVector p[] = new PVector[NT];
int status[] = new int[NT];

//player vectors
PVector pp = new PVector(300, 200);
PVector pv = new PVector(0, 0);
PVector pa = new PVector(0, 0);

//gravity booleans
boolean gravity = false;
boolean upgravity = false;
boolean leftgravity = false;
boolean rightgravity = false;


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
  
  // draw difficulty
  for (int i = 0; i < (difficulty + 1); i++){
    fill(155, 79, 185);
    stroke(0, 0, 0, 0);
    ellipse(d[i].x, d[i].y, 20, 20);
  }
  
  // draw timer
  for (int i = 0; i < (30 - (long)timer); i++){
    fill(242, 223, 73);
    stroke(0, 0, 0, 0);
    ellipse(t[i].x, t[i].y, 20, 20);
  }
  
  // draw triggers
  for (int i = 0; i < NT; i++){
    if (status[i] == 0){
      fill(180, 50, 50);
      stroke(255, 255, 255, 255);
      ellipse(p[i].x, p[i].y, 50, 50);
    }
    else if (status[i] == 1){
      fill(70, 120, 230);
      stroke(255, 255, 255, 255);
      ellipse(p[i].x, p[i].y, 50, 50);
    }
    else if (status[i] == 2){
      fill(32, 193, 38);
      stroke(255, 255, 255, 255);
      ellipse(p[i].x, p[i].y, 50, 50);
    }
    else {
      fill(0);
      stroke(255, 255, 255, 255);
      ellipse(p[i].x, p[i].y, 50, 50);
    }
  }

  // draw player
  fill(255);
  stroke(0, 0, 0, 0);  
  ellipse(pp.x, pp.y, 30, 30);

  // advance stuff  
  if (score == 12 || score == 6 + (difficulty * 3)){
    fill(100, 200, 0);
    stroke(0, 0, 0, 0);
    ellipse(300, 200, 200, 200);
    endstate = 1;
  }
  else if (timer < 30){
  move(); //<>//
  timer += 0.02;
  }
  else {
    fill(255, 0, 0);
    stroke(0, 0, 0, 0);
    ellipse(300, 200, 200, 200);
    endstate = 2;
  }
}

void move() {
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

  // player
  // pv+1 = (pv + pa dt) * damp
  PVector adt = pa.get();
  adt.mult(dt);
  pv.add(adt);
  pv.mult(damp);

  // pp+1 = pp + pv dt
  PVector vdt = pv.get();
  vdt.mult(dt);
  pp.add(vdt);

  // wall collisions
  wall();
  
  // player & pickup collisions
  trigger();
}
 
void wall() { 
  // wall collisions
 
  //player collision
  if (pp.x > width - 15) {
    pv.x = -pv.x;
    pp.x = width - 15;
  }
  if (pp.x < 15) {
    pv.x = -pv.x;
    pp.x = 15;
  }
  // bottom/floor
  if (pp.y > height - 15) {
    pv.y = -pv.y;
    pp.y = height - 15;
  }
  //top/ceiling
  if (pp.y < 15) {
    pv.y = -pv.y;
    pp.y = 15;
  }
}

void trigger() {
  // player collisions with triggers
  
  for (int i = 0; i < NT; i++){
    if (status[i] == 1){
      if (dist(p[i].x, p[i].y, pp.x, pp.y) <= 40){
        status[i] = 2;
        score += 1;
        if (i < 11) {status[i+1] = 1;}
      }
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

}

private static void shuffleArray(int[] array)
  // method that shuffles an integer array, copied from https://stackoverflow.com/questions/1519736/random-shuffling-of-an-array
{
    int index, temp;
    Random random = new Random();
    for (int i = array.length - 1; i > 0; i--)
    {
        index = random.nextInt(i + 1);
        temp = array[index];
        array[index] = array[i];
        array[i] = temp;
    }
}

void setCoords(PVector p[]){
  // sets coordinates for triggers in a 4x3 grid
  for (int i = 0; i < NT; i++){
    int pos = i+1;
    int column = pos % 4;
    int row = i/4 + 1;
    if (column == 0){
      p[rand[i]] = new PVector(width/8 * 7, (height/3 * row) - (height/6));
    }
    else {
      p[rand[i]] = new PVector((width/4 * column) - (width/8), (height/3 * row) - (height/6));
    }
  }
}

void setTimer(PVector t[]){
  // sets coordinates for circles that represent the timer
  for (int i = 0; i < TC; i++){
    t[i] = new PVector(i*20 + 10, height - 10);
  }
}

void setDifficulty(PVector d[]){
  // sets coordinates for circles that represent difficulty
  for (int i = 0; i < 10; i++){
    d[i] = new PVector(i*20 + 10, 10);
  }
}

void reroll() {
  shuffleArray(rand);
  setCoords(p);
  setTimer(t);
  setDifficulty(d);
  for (int i = 0; i < NT; i++){
    status[i] = 0;
  }
  status[0] = 1;
  if (endstate == 1) {difficulty += 1;}
  else if (endstate == 2 && difficulty > 0) {difficulty -= 1;}
  int timeMod = (difficulty - 2)*3;
  timer = 0;
  if (timeMod > 0){timer += timeMod;}
  score = 0;
  gravity = upgravity = leftgravity = rightgravity = false;
  pp = new PVector(300, 200);
  pv = new PVector(0, 0);
  pa = new PVector(0, 0);
}