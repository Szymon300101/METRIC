#include <Arduino.h>

void setup() {
  Serial.begin(115200);
}

void loop() {
  Serial.println(millis());
  delay(1000);
}