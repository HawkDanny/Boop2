//player 1 is on the left
int p1North = 7;
int p1South = 8;
int p1East = 10;
int p1West = 9;

//player 2 is on the right
int p2North = 2;
int p2South = 3;
int p2East = 5;
int p2West = 4;

//The button
int button = 6;

#define SERIAL_USB

void setup() {
  pinMode(p2North, INPUT);
  pinMode(p2South, INPUT);
  pinMode(p2East, INPUT);
  pinMode(p2West, INPUT);
  pinMode(p1North, INPUT);
  pinMode(p1South, INPUT);
  pinMode(p1East, INPUT);
  pinMode(p1West, INPUT);
  pinMode(button, INPUT);

  #ifdef SERIAL_USB
    Serial.begin(250000); // You can choose any baudrate, just need to also change it in Unity.
    while (!Serial); // wait for Leonardo enumeration, others continue immediately
  #endif
}

void loop() {
  String data = String(digitalRead(p1North)) + "," + String(digitalRead(p1East)) + "," + String(digitalRead(p1South)) + "," + String(digitalRead(p1West)) + "," + String(digitalRead(p2North)) + "," + String(digitalRead(p2East)) + "," + String(digitalRead(p2South)) + "," + String(digitalRead(p2West)) + "," + String(digitalRead(button));
  sendData(data);
  delay(5);
}

void sendData(String data){
  #ifdef SERIAL_USB
    Serial.println(data); // need a end-line because wrmlh.csharp use readLine method to receive data
  #endif
}

