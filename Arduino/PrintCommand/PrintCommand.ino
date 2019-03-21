/* Example: 'Print Command'  
 * Copyright (c) 2019 Mateusz Pusty
 *
 * Part of Arduino-Unity. 
 * https://github.com/Sygan/Arduino-Unity 
 * 
 * This code is available under: MIT License.
 * Full licence available at: https://github.com/Sygan/Arduino-Unity/blob/master/LICENSE
 */
 
#include <SoftwareSerial.h>

String message; 

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  if (Serial.available() > 0) 
  {
    message = Serial.readString();

    Serial.print("Received message: ");
    Serial.println(message);
  }
}
