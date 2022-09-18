#include <Arduino.h>

class Button
{
    int button_pin;

    long press_start;

    public:
        Button(int pin)
        {
            this->button_pin = pin;
        }

        void begin()
        {
            pinMode(button_pin,INPUT_PULLUP);
        }

        bool isPressed()
        {
            if(digitalRead(button_pin))
                return false;
            else
            {
                press_start=millis();
                delay(10);
                while(!digitalRead(button_pin)) delay(2);
                delay(10);
                if(millis()-press_start>2000)
                {
                    //akcja przy długim przytrzymaniu
                    return false;
                }
                return true;
            }
        }
};