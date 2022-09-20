#include <Arduino.h>
#include "Helpers/Enums.cpp"
#include "Helpers/SerialMessage.cpp"


const byte ACK_OK = 10;
const byte VAL_NULL = 255;

class Comm
{
  SerialMessage message;
  bool reading = false;
  
 public:
  bool is_connected=false;
  bool waiting = false;
  byte r_mode = ModeEnum::idle;

  int header = ACK;
  
  void send(SerialMessage msg)
  {
    Serial.write(msg.Header);
    Serial.write(msg.Value);
  }

  void send(int header, int value)
  {
    Serial.write(header);
    Serial.write(value);
  }

  void read()
  {
    if(Serial.available() > 0)
    {
      if(!reading)
      {
        message.Header = (SerialHeaderEnum)Serial.read();
        reading = true;
      }else
      {
        reading = false;
        message.Value = Serial.read();
      }
    }
  }

  void execute_cmd()
  {
    if(!reading)
    {
      switch(message.Header)
      {
        case PING:
          send(SerialHeaderEnum::ACK, ACK_OK);
        break;
        case SerialHeaderEnum::ACK:
          waiting = false;
        break;
        case SerialHeaderEnum::HELLO:
            is_connected = true;
            send(SerialHeaderEnum::MODE, r_mode);
        break;
        case SerialHeaderEnum::MODE:
          r_mode = (ModeEnum)message.Value;
          send(SerialHeaderEnum::MODE, r_mode);
        break;
      }

      message.Header = SerialHeaderEnum::NONE;
    }
  }

  void changeMode(ModeEnum mode)
  {
    r_mode = mode;
    send(SerialHeaderEnum::MODE, r_mode);
  }
  
};