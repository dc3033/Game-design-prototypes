// prototype 3
// 2d shooter with random enemy generation
// self-downgrade and enemy upgrade progression test
// daniel chan
// 2/12/18

int level = 1;

boolean notdead = true;
float timer = 10.0;

int totalshields = 3;
int shields = totalshields;
float pspmod = 6;

PVector plpos = new PVector(375, 375);
PVector plvel = new PVector(0, 0);

Enemy enemyVec[] = new Enemy[16];
int enemylimit = 8;
int enemycount = 8;

float spmod = 3;
int hpmod = 0;


void setup(){
  size(800, 800, P2D);
  smooth();
  fill(0);
  stroke(255, 255, 255, 255);
  background(99, 180, 245);
  strokeWeight(1);
  for (int i = 0; i < enemycount; i++){
    PVector spawn = getSpawnCoords();
    enemyVec[i] = new Enemy(spawn.x, spawn.y);
  }
}

PVector getSpawnCoords(){
  PVector spawn = new PVector(0, 0);
  int side = round(random(1,4));
  if (side == 1){
    spawn.x = random(-50, 850);
    spawn.y = -50;
  }
  else if (side == 2){
    spawn.x = 850;
    spawn.y = random(-50, 850);
  }
  else if (side == 3){
    spawn.x = random(-50, 850);
    spawn.y = 850;
  }
  else if (side == 4){
    spawn.x = -50;
    spawn.y = random(-50, 850);
  }
  return spawn;
}

class Enemy{
  int health;
  float speed;
  PVector pos = new PVector(0, 0);
  PVector vel = new PVector(0, 0);
  
  Enemy(float x, float y){
    health = 1 + hpmod;
    speed = 1 + spmod;
    pos.x = x;
    pos.y = y;
    vel.x = (plpos.x - pos.x) + speed;
    vel.y = (plpos.y - pos.y) + speed;
  }
  
  void eDraw(){
    fill(255, 0, 0);
    stroke(0, 0, 0, 0);
    ellipse(pos.x, pos.y, 100, 100);
  }
  
  void eMove(){
    pos.x += vel.x / 100;
    pos.y += vel.y / 100;
  }
  
  boolean checkAlive(){
    if (health <= 0) return false;
    else return true;
  }
}

void draw(){
  
  //clear framebuffer
  background(99, 180, 245);
  
  //draw level count
  textSize(30);
  fill(255);
  text(level, 10, 30);
  
  //draw timer
  textSize(30);
  fill(255);
  text(timer, 335, 30);
  
  //draw player
  fill(100, 255, 135);
  stroke(0, 0, 0, 0);
  rect(plpos.x, plpos.y, 50, 50);
  
  //draw shield count
  textSize(25);
  fill(0);
  text(shields, plpos.x + 17, plpos.y + 33);
  
  //draw enemies
  for (int i = 0; i < enemycount; i++){
    enemyVec[i].eDraw();
  }
  
  if (notdead && timer > 0){
  update();
  
  collision();
  }
  else if (notdead && timer < 0){
    textSize(60);
    fill(0);
    text("Click to advance", 150, 400);
  }
  else if (!notdead){
    textSize(60);
    fill(0);
    text("Click to start over", 150, 400);
  }
}

void update(){
  //move player
  plpos.add(plvel);
  
  //move enemies, respawn enemies if they leave play area
  for (int i = 0; i < enemycount; i++){
    enemyVec[i].eMove();
    if (enemyVec[i].pos.x > 850 || enemyVec[i].pos.x < -50 || enemyVec[i].pos.y > 850 || enemyVec[i].pos.y < -50 || enemyVec[i].health <= 0){
      PVector spawn = getSpawnCoords();
      enemyVec[i] = new Enemy(spawn.x, spawn.y);
    }
  }
  
  //add additional enemies
  if (enemylimit > enemycount){
    for (int i = enemycount; i < enemylimit; i++){
      PVector spawn = getSpawnCoords();
      enemyVec[i] = new Enemy(spawn.x, spawn.y);
    }
  }
  
  //reduce time
  timer -= 0.02;
}

void collision(){
  //player collision with walls
  if (plpos.x > width - 50) {
    plvel.x = 0;
    plpos.x = width - 50;
  }
  if (plpos.x < 0) {
    plvel.x = 0;
    plpos.x = 0;
  }
  // bottom/floor
  if (plpos.y > height - 50) {
    plvel.y = 0;
    plpos.y = height - 50;
  }
  //top/ceiling
  if (plpos.y < 0) {
    plvel.y = 0;
    plpos.y = 0;
  }
  
  //player collision with enemy
  for (int i = 0; i < enemycount; i++){
    if (dist(enemyVec[i].pos.x, enemyVec[i].pos.y, plpos.x + 25, plpos.y + 25) <= 75){
      if (shields > 0){
        shields -= 1;
        PVector spawn = getSpawnCoords();
        enemyVec[i] = new Enemy(spawn.x, spawn.y);
      }
      else notdead = false;
    }
  }
}

void mousePressed(){
  if (notdead && timer <= 0){
    nextlevel();
  }
  else if (!notdead){
    reset();
  }
}

void keyPressed() {
  if (keyCode == DOWN) {
    plvel.y = 1 + pspmod;
  } 
  if (keyCode == UP) {
    plvel.y = -1 - pspmod;
  } 
  if (keyCode == LEFT) {
    plvel.x = -1 - pspmod;
  } 
  else if (keyCode == RIGHT) {
    plvel.x = 1 + pspmod;
  } 
}

void keyReleased(){
  if (keyCode == DOWN && plvel.y > 0) {
    plvel.y = 0;
  } 
  if (keyCode == UP && plvel.y < 0) {
    plvel.y = 0;
  } 
  if (keyCode == LEFT && plvel.x < 0) {
    plvel.x = 0;
  } 
  if (keyCode == RIGHT && plvel.x > 0) {
    plvel.x = 0;
  } 
}

void reset(){
  notdead = true;
  
  level = 1;
  totalshields = 3;
  pspmod = 6;
  spmod = 3;
  enemylimit = 8;
  enemycount = enemylimit;
  
  shields = totalshields;
  timer = 10.0;
  
  plpos = new PVector(375, 375);
  plvel = new PVector(0, 0);
  
  for (int i = 0; i < enemylimit; i++){
      PVector spawn = getSpawnCoords();
      enemyVec[i] = new Enemy(spawn.x, spawn.y);
  }
}

void nextlevel(){
  //choose difficulty modifier
  int diffmod = round(random(1,4));
  if (diffmod == 1){
    if (pspmod > 3) pspmod -= 1.5;
    else if (totalshields > 0) totalshields -= 1;
    else if (spmod < 6) spmod += 1.5;
    else if (enemylimit < 16) enemylimit += 2;
  }
  else if (diffmod == 2){
    if (totalshields > 0) totalshields -= 1;
    else if (pspmod > 3) pspmod -= 1.5;
    else if (spmod < 6) spmod += 1.5;
    else if (enemylimit < 16) enemylimit += 2;
  }
    else if (diffmod == 3){
    if (enemylimit < 16) enemylimit += 2;
    else if (pspmod > 3) pspmod -= 1.5;
    else if (spmod < 6) spmod += 1.5;
    else if (totalshields > 0) totalshields -= 1;
  }
    else if (diffmod == 4){
    if (spmod < 6) spmod += 1.5;     
    else if (pspmod > 3) pspmod -= 1.5;
    else if (totalshields > 0) totalshields -= 1;
    else if (enemylimit < 16) enemylimit += 2;
  }
  
  level += 1;
  enemycount = enemylimit;
  shields = totalshields;
  timer = 10.0;
  
  plpos = new PVector(375, 375);
  plvel = new PVector(0, 0);
  
  for (int i = 0; i < enemylimit; i++){
      PVector spawn = getSpawnCoords();
      enemyVec[i] = new Enemy(spawn.x, spawn.y);
  }
}