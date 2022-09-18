#include <Arduino.h>

class Blinker
{
    bool blinkOn = false;
    unsigned long lastBlinkTime;

    int ledPin;

    int on_time;
    int off_time;

    public:
        Blinker(int pin)
        {
            this->ledPin = pin;
        }

        void begin()
        {
            pinMode(ledPin,OUTPUT);
        }

        void set(int on_time, int off_time)
        {
            this->on_time = on_time;
            this->off_time = off_time;
        }

        void blink()
        {
            if(blinkOn)
            {
                if(millis() > lastBlinkTime + on_time)
                {
                    lastBlinkTime = millis();
                    blinkOn = false;
                    digitalWrite(ledPin,blinkOn);
                }
            }else
            {
                if(millis() > lastBlinkTime + off_time)
                {
                    lastBlinkTime = millis();
                    blinkOn = true;
                    digitalWrite(ledPin,blinkOn);
                }
            }
        }
};