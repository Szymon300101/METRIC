#include <Arduino.h>

class Blinker
{
    bool is_on = false;
    unsigned long last_blink_time;

    int led_pin;

    int on_time;
    int off_time;

    public:
        Blinker(int pin)
        {
            this->led_pin = pin;
        }

        void begin()
        {
            pinMode(led_pin,OUTPUT);
        }

        void set(int on_time, int off_time)
        {
            this->on_time = on_time;
            this->off_time = off_time;
        }

        void blink()
        {
            if(is_on)
            {
                if(millis() > last_blink_time + on_time)
                {
                    last_blink_time = millis();
                    is_on = false;
                    digitalWrite(led_pin,is_on);
                }
            }else
            {
                if(millis() > last_blink_time + off_time)
                {
                    last_blink_time = millis();
                    is_on = true;
                    digitalWrite(led_pin,is_on);
                }
            }
        }
};