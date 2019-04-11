/*  Simple Serial Writer Child Side Sample Program
    LCD Writer by Tetsu Otter
    These code are under the MIT License.  Please refer to the LICENSE file.
    If you want to know how to use, please read the README.md file.

    Comment : 単に受け取った情報をlcd.printするだけ。Leonardo推奨。実際は更新確認を入れた方がいい。
*/
#include <LiquidCrystal.h>
#define BaudRateNum 115200
LiquidCrystal lcd(8, 9, 4, 5, 6, 7);


void SetUpDoing() {
  lcd.setCursor(0, 0);
  lcd.print("Now Loading");
  Serial.begin(BaudRateNum);
  int cnt = 0;
  while (!Serial) {
    lcd.setCursor(11, 0);
    switch (cnt) {
      case 0:
        lcd.print(".");
        break;
      case 1:
        lcd.print("..");
        break;
      case 2:
        lcd.print("...");
        break;
      case 3:
        lcd.print("....");
        break;
      case 4:
        cnt = -1;
        lcd.print("    ");
        break;
    }
    cnt++;
    delay(200);
  }
  //lcd.clear();
}

void setup() {
  lcd.begin(16, 2);
  SetUpDoing();
}

void loop() {
  if (!Serial) SetUpDoing();
  if (Serial.available() > 0) {
    lcd.setCursor(0, 1);
    lcd.print("                ");
    lcd.setCursor(0, 1);
    while (Serial.available()) {
      lcd.write(Serial.read());
    }

  }
  delay(20);
}
