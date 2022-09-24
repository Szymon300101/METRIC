#include <Arduino.h>
#include "communication.cpp"
#include "sensor.cpp"
#include "sd.cpp"
#include "Helpers/Blinker.cpp"
#include "Helpers/Button.cpp"

#define LOOP_TIME 100 //desired loop time [ms]

#define LED_PIN 4
#define BUTTON_PIN 5

Sensor tsl;
Comm comm;
Card sd;
Blinker LED(LED_PIN);
Button button(BUTTON_PIN);

byte state = StateEnum::s_idle;

byte new_reading;

int loop_time;
long last_loop_end;

void setup() {
  //Serial.begin(9600);
  Serial.begin(115200);
  button.begin();
  LED.begin();
  tsl.begin();
  sd.begin();
  delay(500);
  comm.send(SerialHeaderEnum::HELLO,VALUE_NULL);
}

void do_reading()
{
  tsl.read();
  new_reading = tsl.get_center();
  comm.send(SerialHeaderEnum::READING,new_reading);
  sd.formatAndPrint(new_reading);
}

void do_scan()
{
  if(!comm.waiting)
  {
    tsl.read();
    comm.send(SerialHeaderEnum::SCAN,255);
    for(int i=0;i<NPIXELS;i++)
    {
      new_reading = tsl.pixel[i];
      if(new_reading == 255)
      {
        new_reading = 254;
      }
      comm.send(SerialHeaderEnum::SCAN,new_reading);
      delay(2);
    }
    comm.send(SerialHeaderEnum::PING, VALUE_NULL);
    comm.waiting = true;
  }
}

void loop() {

  comm.read();
  comm.execute_cmd();

  LED.blink();

  switch(state)
  {
    case StateEnum::s_idle :
      LED.set(200,2000);
      //
      if(button.is_pressed()) comm.change_mode(ModeEnum::read);
      if(comm.r_mode == ModeEnum::read && comm.is_connected) state = StateEnum::s_read;
      if(comm.r_mode == ModeEnum::read && !comm.is_connected) state = StateEnum::s_open_sd;
      if(comm.r_mode == ModeEnum::scan) state = StateEnum::s_scan;
    break;
    
    case StateEnum::s_open_sd:
      sd.open();
      //
      state = StateEnum::s_read;
    break;
    
    case StateEnum::s_read:
      LED.set(500,500);
      //
      do_reading();
      //
      if(button.is_pressed()) comm.change_mode(ModeEnum::idle);
      if(comm.r_mode == ModeEnum::idle && comm.is_connected) state = StateEnum::s_idle;
      if(comm.r_mode == ModeEnum::idle && !comm.is_connected) state = StateEnum::s_close_sd;
      if(comm.r_mode == ModeEnum::scan) state = StateEnum::s_scan;
    break;
    
    case StateEnum::s_close_sd:
      sd.close();
      //
      state = StateEnum::s_idle;
    break;
    
    case StateEnum::s_scan:
      LED.set(1000,400);
      //
      do_scan();
      //
      if(comm.r_mode == ModeEnum::read) state = StateEnum::s_read;
      if(comm.r_mode == ModeEnum::idle) state = StateEnum::s_idle;
    break;
  }

  loop_time=millis()-last_loop_end;
  delay(max(0, LOOP_TIME-loop_time));
  last_loop_end = millis();
}