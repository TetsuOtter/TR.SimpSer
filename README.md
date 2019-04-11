# Simple Serial Writer
It is a Simple Serial communication PI. It sends only one element using pre-specified COM Port and BaudRate.

## About this
依頼を受けて作った、とても簡単なプラグインです。練習のため、C#版ATSPIとして開発しています。

できることは、あらかじめ指定したCOMポートとBaudRateを用いて、指定した情報をSerial.printlnすることだけです。  
BIDSよりも単純で高速ですが、一部の方にとっては使いづらいかもしれません。

## How to Use
非常に単純で、機器を接続し、デバイスマネージャー等でCOMポートを確認したのちに、そのポート番号と使用するBaudRate、送信する情報をファイル名に含めるだけです。

ファイル名の書式は以下の通りです。  
~~~
TR.SimpSer.[DataName].[PortNum].[BaudRate].dll
~~~

### DataName
#### 注意事項
- 大文字小文字を区別するので、注意してください。
- [DataName]には、DataName列の4文字を記載してください。
- 送信形式は、送信される文字列のフォーマットを表します。
-- 例えば、intであれば"100"などと、整数がstringで送られます。
-- floatやdoubleであれば"100.0"などと、小数がstringで送られてくると思います。
-- boolであれば"0"もしくは"1"がstringで送信されると思います。
-- byteであれば、その構造体の生データが**byteで**送信されます。stringではないので、注意してください。
---byte形式のDataNameを利用する場合は、ソースを読んで実装を理解してください。テストできてないので、動作保証もありませんし、サポート範囲外です。
- 誤ったDataNameを指定すると、エラーを吐くと思います。

#### DataName一覧表
|DataName|名称|送信形式|備考|
|----|----|----|----|
|SPDf|現在速度|float|単位は"km/h"|
|STTb|列車状態|byte|サポート範囲外|

### PortNum
PortNumは、このプラグインが開くポート番号です。数字のみを設定してください。  
256以上とか数字以外を入れるとエラーを吐きます。たぶん。

### BaudRate
[BaudRate]では、ひとつ前で指定したCOMポートで使用するBaudRateを指定します。必ず指定してください。デフォルトなんてものはありません。

## License
このプラグインは、CC0のテンプレートを使用し、MITライセンスの下でソースコートを公開しております。  
MITライセンスに従い、利用してください。

## Contact
何か連絡がある場合は、[@Tetsu_Otter@Twitter.com](https://twitter.com/Tetsu_Otter)までお願いいたします。  
issueやPRは大歓迎です。

## Template
今回は私が提供しているC#版ATSPIテンプレートを用いて開発を行いました。こちらのテンプレートの説明は以下の通りです。

[@ED67900_5@Twitter.com](https://twitter.com/ED67900_5)氏の発見した方法をもとに、自分用にC++版のテンプレートの内容を移植したものになります。

参考 : https://kagasu.hatenablog.com/entry/2017/12/31/220239

C++版と同様にCC0で利用できますが、このテンプレートはDllExportを使用しているため、それに関してはMIT Licenseが適用されます。誤ってLICENSEをどこにも含まないままで使用しないように注意してください。

packagesフォルダは、gitignoreにてリポジトリに含まないよう設定しています。各自で参考リンクを参考にして、DllExportライブラリを導入してください。

