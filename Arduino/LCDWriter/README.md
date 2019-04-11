# LCDWriter
One of Sample Proglams for Simple Serial Writer.

## About this
Simple Serial Writerを使ってArduinoと通信する際に、どのようにプログラムを組むと通信できるかがわからない方向けに、サンプルプログラムを配布しています。  
このLCDWriterもその1つで、LCDシールドに情報を表示するだけのプログラムです。遊びとして、Now loadingのドットが動くようにしました。

## How to Use
LCDWriterをArduino IDEで開き、ボードに書き込んでください。ボードはLeonardoを推奨します。Leonardo以外だとNow loadingのドットは動きません。

今回はBaudRateを115200に設定していますが、BaudRateNumを変更することによりこれを自由に変更することができます。  
プラグイン側で情報名, COMポート番号, BaudRateを指定し、BVEに読み込ませると、自動で通信を開始し、LCDに情報を表示してくれます。

## Attemtion
- 今回はそこまで凝った作りにしていないので、情報が来るたびにLCDが書き換わります。いくら何でもこれは過剰なので、適宜履歴を参照して同一であれば捨てるコードを追加するといいと思います。
- MITライセンスの下で自由に加工・再配布等行うことができます。

## License
These code are under the MIT License.  
Please refer to the LICENSE file.