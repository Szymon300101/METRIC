#include <Arduino.h>
#include "communication.cpp"
#include "sensor.cpp"
#include "Helpers/Blinker.cpp"
#include "Helpers/Button.cpp"


#define MAX_LOOP_TIME 100

Sensor tsl;
Comm comm;
Blinker LED(4);
Button button(5);

byte state = StateEnum::s_idle;

int loop_time;
long loop_start;

void setup() {
  Serial.begin(115200);
  button.begin();
  LED.begin();
  tsl.begin();
  delay(500);
  comm.send(SerialHeaderEnum::HELLO,100);
}

void loop() {
  loop_start=millis();

  comm.read();
  comm.execute_cmd();

  LED.blink();

  switch(state)
  {
    case StateEnum::s_idle :
      LED.set(200,2000);

      if(comm.r_mode == ModeEnum::read) state = StateEnum::s_set_read;
      if(comm.r_mode == ModeEnum::scan) state = StateEnum::s_scan;
    break;
    
    case StateEnum::s_set_read:
      state = StateEnum::s_read;
    break;
    
    case StateEnum::s_read:
      LED.set(500,500);
      
      tsl.read();
      comm.send(SerialHeaderEnum::READING,tsl.get_center());

      if(comm.r_mode == ModeEnum::idle) state = StateEnum::s_idle;
      if(comm.r_mode == ModeEnum::scan) state = StateEnum::s_scan;
    break;
    
    case StateEnum::s_scan:
      LED.set(1000,400);
      if(!comm.waiting)
      {
        comm.send(SerialHeaderEnum::SCAN,255);
        for(int i=0;i<NPIXELS;i++)
        {
          byte value = tsl.pixel[i];
          if(value == 255)
          {
            value = 254;
          }
          comm.send(SerialHeaderEnum::SCAN,value);
          delay(2);
        }
        comm.send(SerialHeaderEnum::PING, VAL_NULL);
        comm.waiting = true;
      }

      if(comm.r_mode == ModeEnum::read) state = StateEnum::s_set_read;
      if(comm.r_mode == ModeEnum::idle) state = StateEnum::s_idle;
    break;
  }

  loop_time=millis()-loop_start;
  delay(max(0, MAX_LOOP_TIME-loop_time));

}