#include <Arduino.h>

                     // Sensor interface: 
#define AOpin  A0     // Analog output
#define SIpin  3     // Start Integration
#define CLKpin 2     // Clock
 
#define NPIXELS 128  // No. of pixels in array


class Sensor
{
 public:
  byte pixel[NPIXELS]; // Field for measured values <0-255>
  byte max_pixel=0;

  void begin()
  {
    pinMode(SIpin, OUTPUT);
    pinMode(CLKpin, OUTPUT);
 
    digitalWrite(SIpin, LOW);   // IDLE state
    digitalWrite(CLKpin, LOW);  // IDLE state
  }

  void read()
  {
    
     digitalWrite (CLKpin, LOW);
     digitalWrite (SIpin, HIGH);
     digitalWrite (CLKpin, HIGH);
     digitalWrite (SIpin, LOW);
   
     delayMicroseconds (1);            
   
     for (int i = 0; i < NPIXELS; i++) {
       digitalWrite (CLKpin, LOW);
       //delayMicroseconds (1);
       digitalWrite (CLKpin, HIGH);
     }
   
       delayMicroseconds (1);  /* Integration time in microseconds */
     
       digitalWrite (CLKpin, LOW);
       digitalWrite (SIpin, HIGH);
       digitalWrite (CLKpin, HIGH);
       digitalWrite (SIpin, LOW);
     
       delayMicroseconds (1);            
     
      /* and now read the real image */
       max_pixel = 0;
       for (int i = 0; i < NPIXELS; i++) {
         pixel[i] = (analogRead(AOpin)/4); // 8-bit is enough
         if(pixel[i]>max_pixel) max_pixel = pixel[i];
         
         digitalWrite (CLKpin, LOW);
         delayMicroseconds (1);
         digitalWrite (CLKpin, HIGH);
       }
  }

  byte get_center()
  {
    byte left_pointer;
    byte right_pointer;

    for(left_pointer=0;left_pointer<NPIXELS;left_pointer++)
    {
      if(pixel[left_pointer]>max_pixel-5)
        break;
    }
    
    for(right_pointer=NPIXELS-1;right_pointer>=0;right_pointer--)
    {
      if(pixel[right_pointer]>max_pixel-5)
        break;
    }

    return (right_pointer+left_pointer)/2;
  }

  
};